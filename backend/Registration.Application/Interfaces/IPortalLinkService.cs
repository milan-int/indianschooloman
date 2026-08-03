using Registration.Application.DTOs;

namespace Registration.Application.Interfaces
{
    public interface IPortalLinkService
    {
        Task<LandingPageDataDto> GetLandingPageDataAsync();

        // Portal Links
        Task<IEnumerable<PortalLinkDto>> GetAllLinksAsync(bool includeDeleted = false);
        Task<PortalLinkDto?> GetByIdAsync(int id);
        Task<PortalLinkDto> CreateLinkAsync(CreatePortalLinkDto dto);
        Task<PortalLinkDto?> UpdateLinkAsync(int id, UpdatePortalLinkDto dto);
        Task<bool> ToggleStatusAsync(int id, bool isActive);
        Task<bool> DeleteLinkAsync(int id);

        // Schools Matrix
        Task<IEnumerable<PortalSchoolDto>> GetAllSchoolsAsync(bool includeDeleted = false);
        Task<PortalSchoolDto?> GetSchoolByIdAsync(int id);
        Task<PortalSchoolDto> CreateSchoolAsync(CreateSchoolDto dto);
        Task<PortalSchoolDto?> UpdateSchoolAsync(int id, UpdateSchoolDto dto);
        Task<bool> ToggleSchoolStatusAsync(int id, bool isActive);
        Task<bool> DeleteSchoolAsync(int id);

        // Guidelines
        Task<IEnumerable<PortalGuidelineDto>> GetAllGuidelinesAsync(bool includeDeleted = false);
        Task<PortalGuidelineDto?> GetGuidelineByIdAsync(int id);
        Task<PortalGuidelineDto> CreateGuidelineAsync(CreateGuidelineDto dto);
        Task<PortalGuidelineDto?> UpdateGuidelineAsync(int id, UpdateGuidelineDto dto);
        Task<bool> ToggleGuidelineStatusAsync(int id, bool isActive);
        Task<bool> DeleteGuidelineAsync(int id);

        // Configs
        Task<IEnumerable<PortalConfigDto>> GetAllConfigsAsync();
        Task<PortalConfigDto?> GetConfigByKeyAsync(string key);
        Task<PortalConfigDto?> UpdateConfigAsync(string key, UpdateConfigDto dto);
    }
}
