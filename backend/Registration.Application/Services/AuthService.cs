using Registration.Application.Common;
using Registration.Application.DTOs;
using Registration.Application.Interfaces;
using Registration.Domain.Entities;

namespace Registration.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserAccountRepository _userRepository;

        public AuthService(IUserAccountRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Username/Registration No. and password are required."
                };
            }

            var cleanUser = request.Username.Trim().ToLower();

            // Search user by Username or Email or RegistrationNo
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(cleanUser);

            // If not found in UserAccounts, check if this is an existing Registration without an account yet
            if (user == null)
            {
                var reg = await _userRepository.FindRegistrationForAccountAsync(cleanUser);
                if (reg != null)
                {
                    // Auto-provision user account for this existing registration
                    user = await CreateClientAccountForRegistrationAsync(reg, "ISOman@2026");
                }
            }

            if (user == null)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Account not found with provided credentials."
                };
            }

            if (!user.IsActive)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "This account is inactive. Please contact the Indian Schools Admission Helpdesk."
                };
            }

            // Verify Password
            bool isPasswordValid = PasswordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt);
            
            // Allow fallback match if password matches default passport number or standard default
            if (!isPasswordValid && user.Registration?.Student != null)
            {
                var passport = user.Registration.Student.PassportNumber;
                if (!string.IsNullOrEmpty(passport) && request.Password.Equals(passport, StringComparison.OrdinalIgnoreCase))
                {
                    isPasswordValid = true;
                }
            }

            if (!isPasswordValid)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid password. Please check and try again."
                };
            }

            // Update Last Login
            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateUserAsync(user);

            // Generate token
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + "." + user.Id;

            return new LoginResponseDto
            {
                Success = true,
                Message = $"Welcome back, {user.FullName}!",
                Token = token,
                User = new UserAccountDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    RegistrationId = user.RegistrationId,
                    RegistrationNo = user.Registration?.RegistrationNo,
                    IsActive = user.IsActive,
                    LastLoginAt = user.LastLoginAt
                }
            };
        }

        public async Task<UserAccountDto?> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return null;

            return new UserAccountDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                RegistrationId = user.RegistrationId,
                RegistrationNo = user.Registration?.RegistrationNo,
                IsActive = user.IsActive,
                LastLoginAt = user.LastLoginAt
            };
        }

        public async Task<UserAccountDto?> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(username);
            if (user == null) return null;

            return new UserAccountDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                RegistrationId = user.RegistrationId,
                RegistrationNo = user.Registration?.RegistrationNo,
                IsActive = user.IsActive,
                LastLoginAt = user.LastLoginAt
            };
        }

        public async Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            if (!PasswordHasher.VerifyPassword(dto.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            {
                return false;
            }

            var newSalt = PasswordHasher.GenerateSalt();
            user.PasswordSalt = newSalt;
            user.PasswordHash = PasswordHasher.HashPassword(dto.NewPassword, newSalt);
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<UserAccount> CreateClientAccountForRegistrationAsync(Registration.Domain.Entities.Registration registration, string? initialPassword = null)
        {
            var student = registration.Student;
            var parent = registration.Parent;

            var rawPassword = !string.IsNullOrWhiteSpace(initialPassword) 
                ? initialPassword 
                : (!string.IsNullOrWhiteSpace(student?.PassportNumber) ? student.PassportNumber : "ISOman@2026");

            var salt = PasswordHasher.GenerateSalt();
            var hash = PasswordHasher.HashPassword(rawPassword, salt);

            var user = new UserAccount
            {
                Username = registration.RegistrationNo,
                Email = parent?.Email ?? "",
                PasswordHash = hash,
                PasswordSalt = salt,
                Role = "CLIENT",
                FullName = parent != null ? $"{parent.GivenName} {parent.Surname}".Trim() : "Parent / Applicant",
                PhoneNumber = parent?.MobileNo ?? "",
                RegistrationId = registration.Id,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            return await _userRepository.AddUserAsync(user);
        }

        public async Task<AdminDashboardStatsDto> GetAdminStatsAsync()
        {
            return await _userRepository.GetAdminStatsAsync();
        }

        public async Task<List<ApplicantSummaryDto>> GetAllApplicationsAsync(string? search = null, string? school = null, string? status = null, string? className = null)
        {
            return await _userRepository.GetAllApplicationsAsync(search, school, status, className);
        }

        public async Task<Registration.Domain.Entities.Registration?> GetApplicationDetailsAsync(int registrationId)
        {
            return await _userRepository.GetApplicationDetailsAsync(registrationId);
        }

        public async Task<bool> UpdateApplicationStatusAsync(int registrationId, UpdateApplicationStatusDto dto)
        {
            return await _userRepository.UpdateRegistrationStatusAsync(registrationId, dto.Status);
        }

        public async Task<List<UserAccountDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UserAccountDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                FullName = u.FullName,
                PhoneNumber = u.PhoneNumber,
                RegistrationId = u.RegistrationId,
                RegistrationNo = u.Registration?.RegistrationNo,
                IsActive = u.IsActive,
                LastLoginAt = u.LastLoginAt
            }).ToList();
        }

        public async Task<bool> ToggleUserStatusAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.IsActive = !user.IsActive;
            return await _userRepository.UpdateUserAsync(user);
        }
    }
}
