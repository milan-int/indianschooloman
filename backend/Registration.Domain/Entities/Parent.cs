using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Entities
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }
        
        public int RegistrationId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string GivenName { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Surname { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Relationship { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string CivilId { get; set; } = string.Empty;
        
        [Required]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string MobileNo { get; set; } = string.Empty;
        
        [StringLength(150)]
        public string Employer { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Occupation { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Nationality { get; set; } = "INDIAN";

        public Registration? Registration { get; set; }
    }
}

