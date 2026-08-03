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

        /// <summary>
        /// Gets active links for the landing page (Admission links and Footer links)
        /// </summary>
        [HttpGet("landing-data")]
        public async Task<ActionResult<LandingPageDataDto>> GetLandingPageData()
        {
            var data = await _portalService.GetLandingPageDataAsync();
            return Ok(data);
        }

        /// <summary>
        /// Gets all portal links including inactive ones (for management)
        /// </summary>
        [HttpGet("links")]
        public async Task<ActionResult<IEnumerable<PortalLinkDto>>> GetAllLinks()
        {
            var links = await _portalService.GetAllLinksAsync();
            return Ok(links);
        }

        /// <summary>
        /// Gets a specific link by its ID
        /// </summary>
        [HttpGet("links/{id}")]
        public async Task<ActionResult<PortalLinkDto>> GetById(int id)
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
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
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
        public async Task<ActionResult> ToggleStatus(int id, [FromBody] ToggleLinkStatusDto dto)
        {
            var success = await _portalService.ToggleStatusAsync(id, dto.IsActive);
            if (!success) return NotFound(new { message = $"Link with ID {id} not found." });
            return Ok(new { message = $"Link ID {id} active status updated to {dto.IsActive}." });
        }

        /// <summary>
        /// Deletes a portal link
        /// </summary>
        [HttpDelete("links/{id}")]
        public async Task<ActionResult> DeleteLink(int id)
        {
            var success = await _portalService.DeleteLinkAsync(id);
            if (!success) return NotFound(new { message = $"Link with ID {id} not found." });
            return Ok(new { message = $"Link ID {id} deleted successfully." });
        }

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
    }
}
