using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Entities
{
    [Table("PortalGuidelinesMaster")]
    public class PortalGuidelineMaster
    {
        [Key]
        public int Id { get; set; }

        public int DisplayOrder { get; set; } = 1;

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Detail { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Link { get; set; }

        [StringLength(150)]
        public string? LinkText { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
