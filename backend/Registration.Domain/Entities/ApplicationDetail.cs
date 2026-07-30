using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Entities
{
    public class ApplicationDetail
    {
        [Key]
        public int Id { get; set; }
        
        public int RegistrationId { get; set; }
        
        [StringLength(200)]
        public string PreviousSchoolName { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string PreviousSchoolCountry { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string PreviousClassStudied { get; set; } = string.Empty;
        
        public int SiblingsInIndianSchools { get; set; } = 0;
        
        public int SiblingsSeekingAdmission { get; set; } = 0;

        public Registration? Registration { get; set; }
    }
}

