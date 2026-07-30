using Registration.Application.DTOs;
using Registration.Domain.Entities;

namespace Registration.Application.Interfaces
{
    public interface IMasterDataService
    {
        Task<IEnumerable<PostalCodeDto>> GetPostalCodesAsync();
        Task<IEnumerable<MotherTongueDto>> GetMotherTonguesAsync();
        Task<IEnumerable<RelationshipDto>> GetRelationshipsAsync();
        Task<IEnumerable<RelationshipDto>> GetSiblingRelationshipsAsync();
        Task<IEnumerable<NationalityDto>> GetNationalitiesAsync();
        Task<IEnumerable<CountryDto>> GetCountriesAsync();
        Task<IEnumerable<GradeDto>> GetGradesWithSchoolsAsync();
        Task<IEnumerable<SiblingSchoolDto>> GetSiblingSchoolsAsync();
        Task<IEnumerable<SiblingClassDto>> GetSiblingClassesAsync();
        Task<IEnumerable<GenderDto>> GetGendersAsync();
    }
}

