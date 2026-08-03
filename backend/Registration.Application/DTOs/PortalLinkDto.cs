namespace Registration.Application.DTOs
{
    public class PortalLinkDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string LinkType { get; set; } = string.Empty;
        public string TargetUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool OpenInNewTab { get; set; }
    }

    public class SchoolMatrixDto
    {
        public int SlNo { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Syllabus { get; set; } = "CBSE"; // CBSE or CAMBRIDGE
        public string Classes { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
    }

    public class GuidelineInstructionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public string? Link { get; set; }
        public string? LinkText { get; set; }
    }

    public class PortalContactDto
    {
        public string HelplinePhone { get; set; } = "+968 2470 2567 / 2479 9700";
        public string HelplineEmail { get; set; } = "admissions@indianschoolsoman.com";
        public string OfficeHours { get; set; } = "Sunday to Thursday (8:00 AM – 2:00 PM)";
        public string AcademicYear { get; set; } = "2026–2027";
    }

    public class LandingPageDataDto
    {
        public IEnumerable<PortalLinkDto> AdmissionLinks { get; set; } = new List<PortalLinkDto>();
        public IEnumerable<PortalLinkDto> FooterLinks { get; set; } = new List<PortalLinkDto>();
        public IEnumerable<SchoolMatrixDto> Schools { get; set; } = new List<SchoolMatrixDto>();
        public IEnumerable<GuidelineInstructionDto> Guidelines { get; set; } = new List<GuidelineInstructionDto>();
        public PortalContactDto Contact { get; set; } = new PortalContactDto();
    }

    public class CreatePortalLinkDto
    {
        public string Title { get; set; } = string.Empty;
        public string Section { get; set; } = "ADMISSION_LINK";
        public string LinkType { get; set; } = "PDF_DOCUMENT";
        public string TargetUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DisplayOrder { get; set; } = 1;
        public bool IsActive { get; set; } = true;
        public bool OpenInNewTab { get; set; } = false;
    }

    public class UpdatePortalLinkDto
    {
        public string Title { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string LinkType { get; set; } = string.Empty;
        public string TargetUrl { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool OpenInNewTab { get; set; }
    }

    public class ToggleLinkStatusDto
    {
        public bool IsActive { get; set; }
    }
}
