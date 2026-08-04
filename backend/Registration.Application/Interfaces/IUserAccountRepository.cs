using Registration.Application.DTOs;
using Registration.Domain.Entities;

namespace Registration.Application.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<UserAccount?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
        Task<UserAccount?> GetUserByIdAsync(int id);
        Task<UserAccount> AddUserAsync(UserAccount user);
        Task<bool> UpdateUserAsync(UserAccount user);
        Task<List<UserAccount>> GetAllUsersAsync();
        Task<Registration.Domain.Entities.Registration?> FindRegistrationForAccountAsync(string cleanUser);
        Task<AdminDashboardStatsDto> GetAdminStatsAsync();
        Task<List<ApplicantSummaryDto>> GetAllApplicationsAsync(string? search = null, string? school = null, string? status = null, string? className = null);
        Task<Registration.Domain.Entities.Registration?> GetApplicationDetailsAsync(int registrationId);
        Task<bool> UpdateRegistrationStatusAsync(int registrationId, string status);
    }
}
