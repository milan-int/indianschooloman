using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Entities
{
    [Table("PortalLinksMaster")]
    public class PortalLinkMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Section { get; set; } = "ADMISSION_LINK"; // e.g. "ADMISSION_LINK", "FOOTER_LINK"

        [Required]
        [StringLength(30)]
        public string LinkType { get; set; } = "PDF_DOCUMENT"; // "INTERNAL_ROUTE", "PDF_DOCUMENT", "EXTERNAL_URL"

        [Required]
        [StringLength(500)]
        public string TargetUrl { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        public int DisplayOrder { get; set; } = 1;

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public bool OpenInNewTab { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
