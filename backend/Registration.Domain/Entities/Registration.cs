using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Entities
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string RegistrationNo { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string Status { get; set; } = "DRAFT";
        
        public bool DeclarationAccepted { get; set; } = false;
        
        public DateTime? SubmittedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation properties
        public Student? Student { get; set; }
        public Parent? Parent { get; set; }
        public Address? Address { get; set; }
        public ApplicationDetail? ApplicationDetail { get; set; }

        public ICollection<RegistrationSchoolPreference> SchoolPreferences { get; set; } = new List<RegistrationSchoolPreference>();
    }
}

