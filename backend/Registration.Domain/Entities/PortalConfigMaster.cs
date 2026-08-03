using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Entities
{
    [Table("PortalConfigsMaster")]
    public class PortalConfigMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ConfigKey { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string ConfigValue { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Section { get; set; } = "GENERAL"; // "CONTACT", "AUTH", "BANNER", "GENERAL"

        [StringLength(255)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
