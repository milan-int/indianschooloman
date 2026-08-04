using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Entities
{
    [Table("UserAccounts")]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;

        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(100)]
        public string PasswordSalt { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Role { get; set; } = "CLIENT"; // "ADMIN" or "CLIENT"

        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;

        public int? RegistrationId { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }

        // Navigation property for clients
        [ForeignKey(nameof(RegistrationId))]
        public Registration? Registration { get; set; }
    }
}
