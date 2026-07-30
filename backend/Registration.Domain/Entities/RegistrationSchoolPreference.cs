using System.ComponentModel.DataAnnotations;

namespace Registration.Domain.Entities
{
    public class RegistrationSchoolPreference
    {
        [Key]
        public int Id { get; set; }

        public int RegistrationId { get; set; }

        public int PreferenceOrder { get; set; }

        [Required]
        [StringLength(200)]
        public string SchoolName { get; set; } = string.Empty;

        public Registration? Registration { get; set; }
    }
}
