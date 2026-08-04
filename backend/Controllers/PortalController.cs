using Microsoft.AspNetCore.Mvc;
using Registration.Application.DTOs;
using Registration.Application.Interfaces;

namespace RegistrationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortalController : ControllerBase
    {
        private readonly IPortalLinkService _portalService;

        public PortalController(IPortalLinkService portalService)
        {
            _portalService = portalService;
        }

        #region Landing Page Data
        /// <summary>
        /// Gets active links, schools, guidelines, and configs for the landing page
        /// </summary>
        [HttpGet("landing-data")]
        public async Task<ActionResult<LandingPageDataDto>> GetLandingPageData()
        {
            var data = await _portalService.GetLandingPageDataAsync();
            return Ok(data);
        }
        #endregion

        #region Portal Links Management
        /// <summary>
        /// Gets all portal links (supports optional includeDeleted)
        /// </summary>
        [HttpGet("links")]
        public async Task<ActionResult<IEnumerable<PortalLinkDto>>> GetAllLinks([FromQuery] bool includeDeleted = false)
        {
            var links = await _portalService.GetAllLinksAsync(includeDeleted);
            return Ok(links);
        }

        /// <summary>
        /// Gets a specific link by its ID
        /// </summary>
        [HttpGet("links/{id}")]
        public async Task<ActionResult<PortalLinkDto>> GetLinkById(int id)
        {
            var link = await _portalService.GetByIdAsync(id);
            if (link == null) return NotFound(new { message = $"Link with ID {id} not found." });
            return Ok(link);
        }

        /// <summary>
        /// Creates a new portal link
        /// </summary>
        [HttpPost("links")]
        public async Task<ActionResult<PortalLinkDto>> CreateLink([FromBody] CreatePortalLinkDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest(new { message = "Title is required." });

            if (string.IsNullOrWhiteSpace(dto.TargetUrl))
                return BadRequest(new { message = "TargetUrl is required." });

            var result = await _portalService.CreateLinkAsync(dto);
            return CreatedAtAction(nameof(GetLinkById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing portal link
        /// </summary>
        [HttpPut("links/{id}")]
        public async Task<ActionResult<PortalLinkDto>> UpdateLink(int id, [FromBody] UpdatePortalLinkDto dto)
        {
            var result = await _portalService.UpdateLinkAsync(id, dto);
            if (result == null) return NotFound(new { message = $"Link with ID {id} not found." });
            return Ok(result);
        }

        /// <summary>
        /// Toggles active/inactive status of a link
        /// </summary>
        [HttpPatch("links/{id}/status")]
        public async Task<ActionResult> ToggleLinkStatus(int id, [FromBody] ToggleStatusDto dto)
        {
            var success = await _portalService.ToggleStatusAsync(id, dto.IsActive);
            if (!success) return NotFound(new { message = $"Link with ID {id} not found." });
            return Ok(new { message = $"Link ID {id} active status updated to {dto.IsActive}." });
        }

        /// <summary>
        /// Soft deletes a portal link
        /// </summary>
        [HttpDelete("links/{id}")]
        public async Task<ActionResult> DeleteLink(int id)
        {
            var success = await _portalService.DeleteLinkAsync(id);
            if (!success) return NotFound(new { message = $"Link with ID {id} not found." });
            return Ok(new { message = $"Link ID {id} deleted successfully." });
        }
        #endregion

        #region Schools Matrix Management
        /// <summary>
        /// Gets all schools in the matrix (supports optional includeDeleted)
        /// </summary>
        [HttpGet("schools")]
        public async Task<ActionResult<IEnumerable<PortalSchoolDto>>> GetAllSchools([FromQuery] bool includeDeleted = false)
        {
            var schools = await _portalService.GetAllSchoolsAsync(includeDeleted);
            return Ok(schools);
        }

        /// <summary>
        /// Gets a school by its ID
        /// </summary>
        [HttpGet("schools/{id}")]
        public async Task<ActionResult<PortalSchoolDto>> GetSchoolById(int id)
        {
            var school = await _portalService.GetSchoolByIdAsync(id);
            if (school == null) return NotFound(new { message = $"School with ID {id} not found." });
            return Ok(school);
        }

        /// <summary>
        /// Creates a new school entry
        /// </summary>
        [HttpPost("schools")]
        public async Task<ActionResult<PortalSchoolDto>> CreateSchool([FromBody] CreateSchoolDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "School Name is required." });

            var result = await _portalService.CreateSchoolAsync(dto);
            return CreatedAtAction(nameof(GetSchoolById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing school entry
        /// </summary>
        [HttpPut("schools/{id}")]
        public async Task<ActionResult<PortalSchoolDto>> UpdateSchool(int id, [FromBody] UpdateSchoolDto dto)
        {
            var result = await _portalService.UpdateSchoolAsync(id, dto);
            if (result == null) return NotFound(new { message = $"School with ID {id} not found." });
            return Ok(result);
        }

        /// <summary>
        /// Toggles active/inactive status of a school
        /// </summary>
        [HttpPatch("schools/{id}/status")]
        public async Task<ActionResult> ToggleSchoolStatus(int id, [FromBody] ToggleStatusDto dto)
        {
            var success = await _portalService.ToggleSchoolStatusAsync(id, dto.IsActive);
            if (!success) return NotFound(new { message = $"School with ID {id} not found." });
            return Ok(new { message = $"School ID {id} active status updated to {dto.IsActive}." });
        }

        /// <summary>
        /// Soft deletes a school entry
        /// </summary>
        [HttpDelete("schools/{id}")]
        public async Task<ActionResult> DeleteSchool(int id)
        {
            var success = await _portalService.DeleteSchoolAsync(id);
            if (!success) return NotFound(new { message = $"School with ID {id} not found." });
            return Ok(new { message = $"School ID {id} deleted successfully." });
        }
        #endregion

        #region Guidelines Management
        /// <summary>
        /// Gets all guidelines (supports optional includeDeleted)
        /// </summary>
        [HttpGet("guidelines")]
        public async Task<ActionResult<IEnumerable<PortalGuidelineDto>>> GetAllGuidelines([FromQuery] bool includeDeleted = false)
        {
            var guidelines = await _portalService.GetAllGuidelinesAsync(includeDeleted);
            return Ok(guidelines);
        }

        /// <summary>
        /// Gets a guideline by its ID
        /// </summary>
        [HttpGet("guidelines/{id}")]
        public async Task<ActionResult<PortalGuidelineDto>> GetGuidelineById(int id)
        {
            var guideline = await _portalService.GetGuidelineByIdAsync(id);
            if (guideline == null) return NotFound(new { message = $"Guideline with ID {id} not found." });
            return Ok(guideline);
        }

        /// <summary>
        /// Creates a new guideline entry
        /// </summary>
        [HttpPost("guidelines")]
        public async Task<ActionResult<PortalGuidelineDto>> CreateGuideline([FromBody] CreateGuidelineDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Detail))
                return BadRequest(new { message = "Title and Detail are required." });

            var result = await _portalService.CreateGuidelineAsync(dto);
            return CreatedAtAction(nameof(GetGuidelineById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Updates an existing guideline
        /// </summary>
        [HttpPut("guidelines/{id}")]
        public async Task<ActionResult<PortalGuidelineDto>> UpdateGuideline(int id, [FromBody] UpdateGuidelineDto dto)
        {
            var result = await _portalService.UpdateGuidelineAsync(id, dto);
            if (result == null) return NotFound(new { message = $"Guideline with ID {id} not found." });
            return Ok(result);
        }

        /// <summary>
        /// Toggles active/inactive status of a guideline
        /// </summary>
        [HttpPatch("guidelines/{id}/status")]
        public async Task<ActionResult> ToggleGuidelineStatus(int id, [FromBody] ToggleStatusDto dto)
        {
            var success = await _portalService.ToggleGuidelineStatusAsync(id, dto.IsActive);
            if (!success) return NotFound(new { message = $"Guideline with ID {id} not found." });
            return Ok(new { message = $"Guideline ID {id} active status updated to {dto.IsActive}." });
        }

        /// <summary>
        /// Soft deletes a guideline entry
        /// </summary>
        [HttpDelete("guidelines/{id}")]
        public async Task<ActionResult> DeleteGuideline(int id)
        {
            var success = await _portalService.DeleteGuidelineAsync(id);
            if (!success) return NotFound(new { message = $"Guideline with ID {id} not found." });
            return Ok(new { message = $"Guideline ID {id} deleted successfully." });
        }
        #endregion

        #region Portal Configurations Management
        /// <summary>
        /// Gets all portal configurations
        /// </summary>
        [HttpGet("configs")]
        public async Task<ActionResult<IEnumerable<PortalConfigDto>>> GetAllConfigs()
        {
            var configs = await _portalService.GetAllConfigsAsync();
            return Ok(configs);
        }

        /// <summary>
        /// Gets a portal config by key
        /// </summary>
        [HttpGet("configs/{key}")]
        public async Task<ActionResult<PortalConfigDto>> GetConfigByKey(string key)
        {
            var config = await _portalService.GetConfigByKeyAsync(key);
            if (config == null) return NotFound(new { message = $"Config key '{key}' not found." });
            return Ok(config);
        }

        /// <summary>
        /// Updates a portal config value and status
        /// </summary>
        [HttpPut("configs/{key}")]
        public async Task<ActionResult<PortalConfigDto>> UpdateConfig(string key, [FromBody] UpdateConfigDto dto)
        {
            var result = await _portalService.UpdateConfigAsync(key, dto);
            if (result == null) return NotFound(new { message = $"Config key '{key}' not found." });
            return Ok(result);
        }
        #endregion

        #region Document Management
        /// <summary>
        /// Streams or downloads a PDF document by its file name
        /// </summary>
        [HttpGet("documents/{fileName}")]
        public IActionResult GetDocument(string fileName)
        {
            var sanitizedName = Path.GetFileName(fileName);
            var wwwrootDocs = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents", sanitizedName);

            if (!System.IO.File.Exists(wwwrootDocs))
            {
                return NotFound(new { message = $"Document '{sanitizedName}' not found on server." });
            }

            var fileBytes = System.IO.File.ReadAllBytes(wwwrootDocs);
            Response.Headers.Append("Content-Disposition", $"inline; filename=\"{sanitizedName}\"");
            return File(fileBytes, "application/pdf");
        }

        /// <summary>
        /// Uploads a new PDF document and saves it in the server documents repository
        /// </summary>
        [HttpPost("upload-document")]
        public async Task<IActionResult> UploadDocument([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file uploaded." });

            if (!file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                return BadRequest(new { message = "Only PDF documents are supported." });

            var sanitizedName = Path.GetFileName(file.FileName);
            var docsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents");
            if (!Directory.Exists(docsDir))
            {
                Directory.CreateDirectory(docsDir);
            }

            var filePath = Path.Combine(docsDir, sanitizedName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativeUrl = $"documents/{sanitizedName}";
            return Ok(new { message = "Document uploaded successfully.", fileName = sanitizedName, url = relativeUrl });
        }

        /// <summary>
        /// Uploads a new portal logo image and updates the database configuration
        /// </summary>
        [HttpPost("upload-logo")]
        public async Task<IActionResult> UploadLogo([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file uploaded." });

            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg", ".svg", ".webp", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
                return BadRequest(new { message = "Only image formats (.png, .jpg, .jpeg, .svg, .webp, .gif) are supported." });

            var sanitizedName = $"logo_{DateTime.UtcNow:yyyyMMddHHmmss}{extension}";
            var imagesDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(imagesDir))
            {
                Directory.CreateDirectory(imagesDir);
            }

            var filePath = Path.Combine(imagesDir, sanitizedName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativeUrl = $"images/{sanitizedName}";

            // Update the database configuration entry
            await _portalService.UpdateConfigAsync("PortalLogoUrl", new UpdateConfigDto
            {
                ConfigValue = relativeUrl,
                Description = "Current Indian Schools Oman Portal Logo",
                IsActive = true
            });

            return Ok(new
            {
                message = "Logo uploaded and updated in database successfully.",
                fileName = sanitizedName,
                url = relativeUrl
            });
        }
        #endregion
    }
}
