using Microsoft.EntityFrameworkCore;
using Registration.Application.DTOs;
using Registration.Application.Interfaces;
using Registration.Domain.Entities;
using Registration.Infrastructure.Data;

namespace Registration.Infrastructure.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly AppDbContext _context;

        public UserAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserAccount?> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
        {
            var clean = usernameOrEmail.Trim().ToLower();
            return await _context.UserAccounts
                .Include(u => u.Registration)
                    .ThenInclude(r => r!.Student)
                .Include(u => u.Registration)
                    .ThenInclude(r => r!.Parent)
                .FirstOrDefaultAsync(u => !u.IsDeleted && (u.Username.ToLower() == clean || u.Email.ToLower() == clean));
        }

        public async Task<UserAccount?> GetUserByIdAsync(int id)
        {
            return await _context.UserAccounts
                .Include(u => u.Registration)
                .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<UserAccount> AddUserAsync(UserAccount user)
        {
            _context.UserAccounts.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(UserAccount user)
        {
            _context.UserAccounts.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<UserAccount>> GetAllUsersAsync()
        {
            return await _context.UserAccounts
                .Include(u => u.Registration)
                .Where(u => !u.IsDeleted)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<Registration.Domain.Entities.Registration?> FindRegistrationForAccountAsync(string cleanUser)
        {
            return await _context.Registrations
                .Include(r => r.Student)
                .Include(r => r.Parent)
                .FirstOrDefaultAsync(r => r.RegistrationNo.ToLower() == cleanUser || (r.Parent != null && r.Parent.Email.ToLower() == cleanUser));
        }

        public async Task<AdminDashboardStatsDto> GetAdminStatsAsync()
        {
            var totalApps = await _context.Registrations.CountAsync();
            var submitted = await _context.Registrations.CountAsync(r => r.Status == "SUBMITTED" || r.Status == "DRAFT");
            var underVer = await _context.Registrations.CountAsync(r => r.Status == "UNDER_VERIFICATION");
            var approved = await _context.Registrations.CountAsync(r => r.Status == "APPROVED");
            var allotted = await _context.Registrations.CountAsync(r => r.Status == "SEAT_ALLOTTED");
            var rejected = await _context.Registrations.CountAsync(r => r.Status == "REJECTED");

            var totalSchools = await _context.PortalSchools.CountAsync(s => !s.IsDeleted);
            var totalGuidelines = await _context.PortalGuidelines.CountAsync(g => !g.IsDeleted);
            var totalUsers = await _context.UserAccounts.CountAsync(u => !u.IsDeleted);

            var byClass = await _context.Students
                .Where(s => !string.IsNullOrEmpty(s.AdmissionClass))
                .GroupBy(s => s.AdmissionClass)
                .Select(g => new { Class = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Class, x => x.Count);

            var byPref = await _context.RegistrationSchoolPreferences
                .Where(p => p.PreferenceOrder == 1 && !string.IsNullOrEmpty(p.SchoolName))
                .GroupBy(p => p.SchoolName)
                .Select(g => new { School = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.School, x => x.Count);

            return new AdminDashboardStatsDto
            {
                TotalApplications = totalApps,
                SubmittedCount = submitted,
                UnderVerificationCount = underVer,
                ApprovedCount = approved,
                SeatAllottedCount = allotted,
                RejectedCount = rejected,
                TotalSchools = totalSchools,
                TotalGuidelines = totalGuidelines,
                TotalUsers = totalUsers,
                ApplicationsByClass = byClass,
                ApplicationsByFirstPreference = byPref
            };
        }

        public async Task<List<ApplicantSummaryDto>> GetAllApplicationsAsync(string? search = null, string? school = null, string? status = null, string? className = null)
        {
            var query = _context.Registrations
                .Include(r => r.Student)
                    .ThenInclude(s => s!.ExistingSiblings)
                .Include(r => r.Student)
                    .ThenInclude(s => s!.NewApplicantSiblings)
                .Include(r => r.Parent)
                .Include(r => r.Address)
                .Include(r => r.SchoolPreferences)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(r => r.Status.ToUpper() == status.Trim().ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(className))
            {
                query = query.Where(r => r.Student != null && r.Student.AdmissionClass.ToUpper() == className.Trim().ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(school))
            {
                var sTerm = school.Trim().ToLower();
                query = query.Where(r => r.SchoolPreferences.Any(p => p.SchoolName.ToLower().Contains(sTerm)));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim().ToLower();
                query = query.Where(r => 
                    r.RegistrationNo.ToLower().Contains(s) ||
                    (r.Student != null && (r.Student.GivenName.ToLower().Contains(s) || r.Student.Surname.ToLower().Contains(s) || r.Student.PassportNumber.ToLower().Contains(s))) ||
                    (r.Parent != null && (r.Parent.GivenName.ToLower().Contains(s) || r.Parent.Surname.ToLower().Contains(s) || r.Parent.MobileNo.Contains(s) || r.Parent.Email.ToLower().Contains(s)))
                );
            }

            var records = await query
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return records.Select(r => new ApplicantSummaryDto
            {
                Id = r.Id,
                RegistrationNo = r.RegistrationNo,
                Status = r.Status,
                CreatedAt = r.CreatedAt,
                SubmittedAt = r.SubmittedAt,
                StudentFullName = r.Student != null ? $"{r.Student.GivenName} {r.Student.Surname}".Trim() : "",
                PassportNumber = r.Student?.PassportNumber ?? "",
                AdmissionClass = r.Student?.AdmissionClass ?? "",
                Gender = r.Student?.Gender ?? "",
                DateOfBirth = r.Student?.DateOfBirth ?? DateTime.MinValue,
                ParentFullName = r.Parent != null ? $"{r.Parent.GivenName} {r.Parent.Surname}".Trim() : "",
                ParentRelationship = r.Parent?.Relationship ?? "",
                ParentMobileNo = r.Parent?.MobileNo ?? "",
                ParentEmail = r.Parent?.Email ?? "",
                ParentCivilId = r.Parent?.CivilId ?? "",
                FirstSchoolPreference = r.SchoolPreferences.OrderBy(p => p.PreferenceOrder).FirstOrDefault()?.SchoolName ?? "",
                SchoolPreferences = r.SchoolPreferences.OrderBy(p => p.PreferenceOrder).Select(p => p.SchoolName).ToList(),
                Locality = r.Address?.Locality ?? "",
                PostalCode = r.Address?.PostalCode ?? "",
                SiblingCount = (r.Student?.ExistingSiblings?.Count ?? 0) + (r.Student?.NewApplicantSiblings?.Count ?? 0)
            }).ToList();
        }

        public async Task<Registration.Domain.Entities.Registration?> GetApplicationDetailsAsync(int registrationId)
        {
            return await _context.Registrations
                .Include(r => r.Student)
                    .ThenInclude(s => s.ExistingSiblings)
                .Include(r => r.Student)
                    .ThenInclude(s => s.NewApplicantSiblings)
                .Include(r => r.Parent)
                .Include(r => r.Address)
                .Include(r => r.ApplicationDetail)
                .Include(r => r.SchoolPreferences)
                .FirstOrDefaultAsync(r => r.Id == registrationId);
        }

        public async Task<bool> UpdateRegistrationStatusAsync(int registrationId, string status)
        {
            var reg = await _context.Registrations.FirstOrDefaultAsync(r => r.Id == registrationId);
            if (reg == null) return false;

            reg.Status = status.ToUpper();
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
