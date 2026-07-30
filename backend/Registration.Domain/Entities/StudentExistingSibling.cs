using System.ComponentModel.DataAnnotations;

namespace Registration.Domain.Entities
{
    public class StudentExistingSibling
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }

        [Required]
        [StringLength(150)]
        public string SiblingName { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string SchoolName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string GrNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string ClassName { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string Division { get; set; } = string.Empty;

        public Student? Student { get; set; }
    }
}
