using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        
        public int RegistrationId { get; set; }
        
        [StringLength(50)]
        public string PostalCode { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string PoBox { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string HouseNo { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string LaneWayNo { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string StreetName { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string Locality { get; set; } = string.Empty;
        
        [Required]
        public string PermanentAddress { get; set; } = string.Empty;

        public Registration? Registration { get; set; }
    }
}

