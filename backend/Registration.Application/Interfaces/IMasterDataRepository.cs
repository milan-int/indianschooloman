using Registration.Domain.Entities;

namespace Registration.Application.Interfaces
{
    public interface IMasterDataRepository
    {
        Task<IEnumerable<PostalCodeMaster>> GetPostalCodesAsync();
        Task<IEnumerable<MotherTongueMaster>> GetMotherTonguesAsync();
        Task<IEnumerable<RelationshipMaster>> GetRelationshipsAsync();
        Task<IEnumerable<NationalityMaster>> GetNationalitiesAsync();
        Task<IEnumerable<GradeMaster>> GetGradesWithSchoolsAsync();
        Task<IEnumerable<CountryMaster>> GetCountriesAsync();
        Task<IEnumerable<SiblingSchoolMaster>> GetSiblingSchoolsAsync();
        Task<IEnumerable<SiblingClassMaster>> GetSiblingClassesAsync();
        Task<IEnumerable<GenderMaster>> GetGendersAsync();
    }
}
