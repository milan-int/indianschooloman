using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Entities
{
    [Table("PortalSchoolsMaster")]
    public class PortalSchoolMaster
    {
        [Key]
        public int Id { get; set; }

        public int SlNo { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Syllabus { get; set; } = "CBSE"; // CBSE, CAMBRIDGE, etc.

        [Required]
        [StringLength(100)]
        public string Classes { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Website { get; set; } = string.Empty;

        public int DisplayOrder { get; set; } = 1;

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
