using Microsoft.AspNetCore.Mvc;
using Registration.Application.DTOs;
using Registration.Application.Interfaces;

namespace RegistrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authService.LoginAsync(request);
            if (!response.Success)
            {
                return Unauthorized(response);
            }
            return Ok(response);
        }

        // GET: api/Auth/me?userId=1
        [HttpGet("me")]
        public async Task<ActionResult<UserAccountDto>> GetCurrentProfile([FromQuery] int userId)
        {
            var user = await _authService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { Message = "User profile not found." });
            }
            return Ok(user);
        }

        // POST: api/Auth/change-password?userId=1
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword([FromQuery] int userId, [FromBody] ChangePasswordDto dto)
        {
            var success = await _authService.ChangePasswordAsync(userId, dto);
            if (!success)
            {
                return BadRequest(new { Message = "Failed to update password. Current password may be incorrect." });
            }
            return Ok(new { Message = "Password updated successfully." });
        }
    }
}
