using Registration.Application.DTOs;
using Registration.Application.Interfaces;
using Registration.Domain.Entities;

namespace Registration.Application.Services
{
    public class MasterDataService : IMasterDataService
    {
        private readonly IMasterDataRepository _repository;

        public MasterDataService(IMasterDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PostalCodeDto>> GetPostalCodesAsync()
        {
            var codes = await _repository.GetPostalCodesAsync();
            return codes.Select(p => new PostalCodeDto
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name
            });
        }

        public async Task<IEnumerable<MotherTongueDto>> GetMotherTonguesAsync()
        {
            var tongues = await _repository.GetMotherTonguesAsync();
            return tongues.Select(m => new MotherTongueDto
            {
                Id = m.Id,
                Name = m.Name
            });
        }

        public async Task<IEnumerable<RelationshipDto>> GetRelationshipsAsync()
        {
            var relations = await _repository.GetRelationshipsAsync();
            return relations.Select(r => new RelationshipDto
            {
                Id = r.Id,
                Name = r.Name
            });
        }

        public async Task<IEnumerable<RelationshipDto>> GetSiblingRelationshipsAsync()
        {
            var relations = await _repository.GetRelationshipsAsync();
            var siblingNames = new[] { "Brother/Sister", "Twins", "Triplets" };
            return relations.Where(r => siblingNames.Contains(r.Name)).Select(r => new RelationshipDto
            {
                Id = r.Id,
                Name = r.Name
            });
        }

        public async Task<IEnumerable<NationalityDto>> GetNationalitiesAsync()
        {
            var nationalities = await _repository.GetNationalitiesAsync();
            return nationalities.Select(n => new NationalityDto
            {
                Id = n.Id,
                Name = n.Name
            });
        }

        public async Task<IEnumerable<CountryDto>> GetCountriesAsync()
        {
            var countries = await _repository.GetCountriesAsync();
            return countries.Select(c => new CountryDto
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code
            });
        }

        public async Task<IEnumerable<GradeDto>> GetGradesWithSchoolsAsync()
        {
            var grades = await _repository.GetGradesWithSchoolsAsync();
            return grades.Select(g => new GradeDto
            {
                Id = g.Id,
                GradeCode = g.GradeCode,
                GradeDisplay = g.GradeDisplay,
                PreferenceType = g.PreferenceCount,
                Schools = g.GradeSchools.Select(s => new GradeSchoolDto
                {
                    Id = s.Id,
                    SchoolName = s.SchoolName
                }).ToList()
            });
        }

        public async Task<IEnumerable<SiblingSchoolDto>> GetSiblingSchoolsAsync()
        {
            var schools = await _repository.GetSiblingSchoolsAsync();
            return schools.Select(s => new SiblingSchoolDto
            {
                Id = s.Id,
                Name = s.Name
            });
        }

        public async Task<IEnumerable<SiblingClassDto>> GetSiblingClassesAsync()
        {
            var classes = await _repository.GetSiblingClassesAsync();
            return classes.Select(c => new SiblingClassDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<IEnumerable<GenderDto>> GetGendersAsync()
        {
            var genders = await _repository.GetGendersAsync();
            return genders.Select(g => new GenderDto
            {
                Id = g.Id,
                Name = g.Name
            });
        }
    }
}
