using Registration.Application.DTOs;
using Registration.Domain.Entities;

namespace Registration.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<UserAccountDto?> GetUserByIdAsync(int userId);
        Task<UserAccountDto?> GetUserByUsernameAsync(string username);
        Task<bool> ChangePasswordAsync(int userId, ChangePasswordDto dto);
        Task<UserAccount> CreateClientAccountForRegistrationAsync(Registration.Domain.Entities.Registration registration, string? initialPassword = null);
        
        // Admin Application & Reporting Services
        Task<AdminDashboardStatsDto> GetAdminStatsAsync();
        Task<List<ApplicantSummaryDto>> GetAllApplicationsAsync(string? search = null, string? school = null, string? status = null, string? className = null);
        Task<Registration.Domain.Entities.Registration?> GetApplicationDetailsAsync(int registrationId);
        Task<bool> UpdateApplicationStatusAsync(int registrationId, UpdateApplicationStatusDto dto);
        Task<List<UserAccountDto>> GetAllUsersAsync();
        Task<bool> ToggleUserStatusAsync(int userId);
    }
}
