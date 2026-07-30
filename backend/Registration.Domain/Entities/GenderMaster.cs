using System.ComponentModel.DataAnnotations;

namespace Registration.Domain.Entities
{
    public class GenderMaster
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        public int DisplayOrder { get; set; }
    }
}
