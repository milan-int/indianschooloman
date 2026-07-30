using System.ComponentModel.DataAnnotations;

namespace Registration.Domain.Entities
{
    public class PostalCodeMaster
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Code { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }

    public class MotherTongueMaster
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }

    public class RelationshipMaster
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }

    public class NationalityMaster
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }

    [System.ComponentModel.DataAnnotations.Schema.Table("master_grades")]
    public class GradeMaster
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("grade_id")]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [System.ComponentModel.DataAnnotations.Schema.Column("grade_code")]
        public string GradeCode { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [System.ComponentModel.DataAnnotations.Schema.Column("grade_display")]
        public string GradeDisplay { get; set; } = string.Empty;

        [System.ComponentModel.DataAnnotations.Schema.Column("preference_count")]
        public int PreferenceCount { get; set; }

        public ICollection<GradeSchoolMaster> GradeSchools { get; set; } = new List<GradeSchoolMaster>();
    }

    [System.ComponentModel.DataAnnotations.Schema.Table("grade_schools")]
    public class GradeSchoolMaster
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("school_option_id")]
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.Column("grade_id")]
        public int GradeId { get; set; }
        public GradeMaster Grade { get; set; } = null!;

        [Required]
        [StringLength(150)]
        [System.ComponentModel.DataAnnotations.Schema.Column("school_name")]
        public string SchoolName { get; set; } = string.Empty;
    }
    [System.ComponentModel.DataAnnotations.Schema.Table("master_countries")]
    public class CountryMaster
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.Column("country_id")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [System.ComponentModel.DataAnnotations.Schema.Column("country_name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        [System.ComponentModel.DataAnnotations.Schema.Column("country_code")]
        public string Code { get; set; } = string.Empty;
    }

    [System.ComponentModel.DataAnnotations.Schema.Table("sibling_school_master")]
    public class SiblingSchoolMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }

    [System.ComponentModel.DataAnnotations.Schema.Table("sibling_class_master")]
    public class SiblingClassMaster
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }
}

