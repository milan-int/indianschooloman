using System.ComponentModel.DataAnnotations;

namespace Registration.Domain.Entities
{
    public class StudentNewApplicantSibling
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }

        [Required]
        [StringLength(50)]
        public string PassportNo { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Relationship { get; set; } = "Brother/Sister";

        public Student? Student { get; set; }
    }
}
