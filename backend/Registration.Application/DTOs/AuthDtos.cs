namespace Registration.Application.DTOs
{
    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public UserAccountDto? User { get; set; }
    }

    public class UserAccountDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "ADMIN" or "CLIENT"
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int? RegistrationId { get; set; }
        public string? RegistrationNo { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }

    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }

    public class UpdateApplicationStatusDto
    {
        public string Status { get; set; } = string.Empty; // "SUBMITTED", "UNDER_VERIFICATION", "APPROVED", "SEAT_ALLOTTED", "REJECTED"
        public string? Remarks { get; set; }
        public string? AllottedSchool { get; set; }
    }

    public class AdminDashboardStatsDto
    {
        public int TotalApplications { get; set; }
        public int SubmittedCount { get; set; }
        public int UnderVerificationCount { get; set; }
        public int ApprovedCount { get; set; }
        public int SeatAllottedCount { get; set; }
        public int RejectedCount { get; set; }
        public int TotalSchools { get; set; }
        public int TotalGuidelines { get; set; }
        public int TotalUsers { get; set; }
        public Dictionary<string, int> ApplicationsByClass { get; set; } = new();
        public Dictionary<string, int> ApplicationsByFirstPreference { get; set; } = new();
    }

    public class ApplicantSummaryDto
    {
        public int Id { get; set; }
        public string RegistrationNo { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }

        // Student Info
        public string StudentFullName { get; set; } = string.Empty;
        public string PassportNumber { get; set; } = string.Empty;
        public string AdmissionClass { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        // Parent Info
        public string ParentFullName { get; set; } = string.Empty;
        public string ParentRelationship { get; set; } = string.Empty;
        public string ParentMobileNo { get; set; } = string.Empty;
        public string ParentEmail { get; set; } = string.Empty;
        public string ParentCivilId { get; set; } = string.Empty;

        // Preferences & Address
        public string FirstSchoolPreference { get; set; } = string.Empty;
        public List<string> SchoolPreferences { get; set; } = new();
        public string Locality { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public int SiblingCount { get; set; }
    }
}
