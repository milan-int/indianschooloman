using Registration.Application.DTOs;

namespace Registration.Application.Interfaces
{
    public interface IPortalLinkService
    {
        Task<LandingPageDataDto> GetLandingPageDataAsync();
        Task<IEnumerable<PortalLinkDto>> GetAllLinksAsync();
        Task<PortalLinkDto?> GetByIdAsync(int id);
        Task<PortalLinkDto> CreateLinkAsync(CreatePortalLinkDto dto);
        Task<PortalLinkDto?> UpdateLinkAsync(int id, UpdatePortalLinkDto dto);
        Task<bool> ToggleStatusAsync(int id, bool isActive);
        Task<bool> DeleteLinkAsync(int id);
    }
}
