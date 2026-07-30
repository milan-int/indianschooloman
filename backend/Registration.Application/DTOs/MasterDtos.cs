namespace Registration.Application.DTOs
{
    public class PostalCodeDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class MotherTongueDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class RelationshipDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class NationalityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class GradeDto
    {
        public int Id { get; set; }
        public string GradeCode { get; set; } = string.Empty;
        public string GradeDisplay { get; set; } = string.Empty;
        public int PreferenceType { get; set; }
        public System.Collections.Generic.List<GradeSchoolDto> Schools { get; set; } = new();
    }

    public class GradeSchoolDto
    {
        public int Id { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }

    public class SiblingSchoolDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SiblingClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class GenderDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
