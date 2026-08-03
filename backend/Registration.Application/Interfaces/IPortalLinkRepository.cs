using Registration.Domain.Entities;

namespace Registration.Application.Interfaces
{
    public interface IPortalLinkRepository
    {
        Task<IEnumerable<PortalLinkMaster>> GetActiveLinksAsync();
        Task<IEnumerable<PortalLinkMaster>> GetAllLinksAsync();
        Task<PortalLinkMaster?> GetByIdAsync(int id);
        Task<PortalLinkMaster> AddAsync(PortalLinkMaster link);
        Task<PortalLinkMaster> UpdateAsync(PortalLinkMaster link);
        Task<bool> DeleteAsync(int id);
    }
}
