using Microsoft.AspNetCore.Mvc;
using Registration.Application.DTOs;
using Registration.Application.Interfaces;

namespace RegistrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AdminController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: api/Admin/stats
        [HttpGet("stats")]
        public async Task<ActionResult<AdminDashboardStatsDto>> GetDashboardStats()
        {
            var stats = await _authService.GetAdminStatsAsync();
            return Ok(stats);
        }

        // GET: api/Admin/applications
        [HttpGet("applications")]
        public async Task<ActionResult<List<ApplicantSummaryDto>>> GetAllApplications(
            [FromQuery] string? search,
            [FromQuery] string? school,
            [FromQuery] string? status,
            [FromQuery] string? className)
        {
            var list = await _authService.GetAllApplicationsAsync(search, school, status, className);
            return Ok(list);
        }

        // GET: api/Admin/applications/5
        [HttpGet("applications/{id}")]
        public async Task<ActionResult<Registration.Domain.Entities.Registration>> GetApplicationDetails(int id)
        {
            var reg = await _authService.GetApplicationDetailsAsync(id);
            if (reg == null)
            {
                return NotFound(new { Message = "Application not found." });
            }
            return Ok(reg);
        }

        // PATCH: api/Admin/applications/5/status
        [HttpPatch("applications/{id}/status")]
        public async Task<ActionResult> UpdateApplicationStatus(int id, [FromBody] UpdateApplicationStatusDto dto)
        {
            var success = await _authService.UpdateApplicationStatusAsync(id, dto);
            if (!success)
            {
                return NotFound(new { Message = "Application not found to update." });
            }
            return Ok(new { Message = "Application status updated successfully." });
        }

        // GET: api/Admin/users
        [HttpGet("users")]
        public async Task<ActionResult<List<UserAccountDto>>> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }

        // PATCH: api/Admin/users/5/toggle
        [HttpPatch("users/{id}/toggle")]
        public async Task<ActionResult> ToggleUserStatus(int id)
        {
            var success = await _authService.ToggleUserStatusAsync(id);
            if (!success)
            {
                return NotFound(new { Message = "User account not found." });
            }
            return Ok(new { Message = "User account status toggled successfully." });
        }
    }
}
