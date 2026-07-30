using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Entities
{
    public class Student
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
        public string PassportNumber { get; set; } = string.Empty;
        
        [Column(TypeName = "date")]
        public DateTime? PassportExpiryDate { get; set; }
        
        [StringLength(10)]
        public string Gender { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Nationality { get; set; } = "INDIAN";
        
        [StringLength(50)]
        public string VisaNumber { get; set; } = string.Empty;
        
        [Column(TypeName = "date")]
        public DateTime? VisaExpiryDate { get; set; }
        
        [StringLength(50)]
        public string MotherTongue { get; set; } = string.Empty;
        
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        
        [StringLength(100)]
        public string PlaceOfBirth { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string AdmissionClass { get; set; } = string.Empty;

        public Registration? Registration { get; set; }

        public System.Collections.Generic.ICollection<StudentExistingSibling> ExistingSiblings { get; set; } = new System.Collections.Generic.List<StudentExistingSibling>();
        public System.Collections.Generic.ICollection<StudentNewApplicantSibling> NewApplicantSiblings { get; set; } = new System.Collections.Generic.List<StudentNewApplicantSibling>();
    }
}


