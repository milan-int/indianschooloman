using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Registration.Application.DTOs
{
    public class ExistingSiblingDto
    {
        public string SiblingName { get; set; } = string.Empty;
        public string SchoolName { get; set; } = string.Empty;
        public string GrNumber { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string Division { get; set; } = string.Empty;
    }

    public class NewApplicantSiblingDto
    {
        public string PassportNo { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
    }

    public class RegistrationDto
    {
        // Pupil
        [Required]
        public string PupilFirstName { get; set; } = string.Empty;
        public string PupilSurname { get; set; } = string.Empty;
        [Required]
        public string PassportNumber { get; set; } = string.Empty;
        public DateTime? PassportExpiryDate { get; set; }
        [Required]
        public string ClassSought { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string VisaNumber { get; set; } = string.Empty;
        public DateTime? VisaExpiryDate { get; set; }
        [Required]
        public string MotherTongue { get; set; } = string.Empty;
        
        // Previous School / Birth
        public string PreviousSchool { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string ClassLastAttended { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string PlaceOfBirth { get; set; } = string.Empty;
        
        // Parent
        [Required]
        public string ParentName { get; set; } = string.Empty;
        public string ParentSurname { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        [Required]
        public string ParentPassportNo { get; set; } = string.Empty;
        [Required]
        public string CivilNo { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Gsm { get; set; } = string.Empty;
        public string ResidentialPhone { get; set; } = string.Empty;
        public string OfficePhone { get; set; } = string.Empty;
        [Required]
        public string Employer { get; set; } = string.Empty;
        [Required]
        public string Occupation { get; set; } = string.Empty;
        public string ParentNationality { get; set; } = string.Empty;
        
        // Contact
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        public string PoBox { get; set; } = string.Empty;
        [Required]
        public string PermanentAddress { get; set; } = string.Empty;
        [Required]
        public string HouseFlatNo { get; set; } = string.Empty;
        [Required]
        public string WayNo { get; set; } = string.Empty;
        [Required]
        public string StreetName { get; set; } = string.Empty;
        [Required]
        public string Locality { get; set; } = string.Empty;
        
        // Siblings & Preferences
        public List<string> SchoolPreferences { get; set; } = new();
        public int SiblingsStudyingCount { get; set; } = 0;
        public int SiblingsSeekingAdmissionCount { get; set; } = 0;

        public List<ExistingSiblingDto> ExistingSiblings { get; set; } = new();
        public List<NewApplicantSiblingDto> NewApplicantSiblings { get; set; } = new();
        
        public bool Declaration { get; set; } = false;
    }
}
