using Registration.Domain.Entities;

namespace Registration.Application.Interfaces
{
    public interface IPortalLinkRepository
    {
        // Portal Links
        Task<IEnumerable<PortalLinkMaster>> GetActiveLinksAsync();
        Task<IEnumerable<PortalLinkMaster>> GetAllLinksAsync(bool includeDeleted = false);
        Task<PortalLinkMaster?> GetByIdAsync(int id);
        Task<PortalLinkMaster> AddAsync(PortalLinkMaster link);
        Task<PortalLinkMaster> UpdateAsync(PortalLinkMaster link);
        Task<bool> SoftDeleteAsync(int id);

        // Schools Matrix
        Task<IEnumerable<PortalSchoolMaster>> GetActiveSchoolsAsync();
        Task<IEnumerable<PortalSchoolMaster>> GetAllSchoolsAsync(bool includeDeleted = false);
        Task<PortalSchoolMaster?> GetSchoolByIdAsync(int id);
        Task<PortalSchoolMaster> AddSchoolAsync(PortalSchoolMaster school);
        Task<PortalSchoolMaster> UpdateSchoolAsync(PortalSchoolMaster school);
        Task<bool> SoftDeleteSchoolAsync(int id);

        // Guidelines
        Task<IEnumerable<PortalGuidelineMaster>> GetActiveGuidelinesAsync();
        Task<IEnumerable<PortalGuidelineMaster>> GetAllGuidelinesAsync(bool includeDeleted = false);
        Task<PortalGuidelineMaster?> GetGuidelineByIdAsync(int id);
        Task<PortalGuidelineMaster> AddGuidelineAsync(PortalGuidelineMaster guideline);
        Task<PortalGuidelineMaster> UpdateGuidelineAsync(PortalGuidelineMaster guideline);
        Task<bool> SoftDeleteGuidelineAsync(int id);

        // Portal Configs
        Task<IEnumerable<PortalConfigMaster>> GetActiveConfigsAsync();
        Task<IEnumerable<PortalConfigMaster>> GetAllConfigsAsync();
        Task<PortalConfigMaster?> GetConfigByKeyAsync(string key);
        Task<PortalConfigMaster> UpdateConfigAsync(PortalConfigMaster config);
    }
}
