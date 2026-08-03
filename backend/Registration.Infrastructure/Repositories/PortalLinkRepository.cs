using Microsoft.EntityFrameworkCore;
using Registration.Application.Interfaces;
using Registration.Domain.Entities;
using Registration.Infrastructure.Data;

namespace Registration.Infrastructure.Repositories
{
    public class PortalLinkRepository : IPortalLinkRepository
    {
        private readonly AppDbContext _context;

        public PortalLinkRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PortalLinkMaster>> GetActiveLinksAsync()
        {
            return await _context.PortalLinks
                .Where(x => x.IsActive)
                .OrderBy(x => x.Section)
                .ThenBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<PortalLinkMaster>> GetAllLinksAsync()
        {
            return await _context.PortalLinks
                .OrderBy(x => x.Section)
                .ThenBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<PortalLinkMaster?> GetByIdAsync(int id)
        {
            return await _context.PortalLinks.FindAsync(id);
        }

        public async Task<PortalLinkMaster> AddAsync(PortalLinkMaster link)
        {
            link.CreatedAt = DateTime.UtcNow;
            await _context.PortalLinks.AddAsync(link);
            await _context.SaveChangesAsync();
            return link;
        }

        public async Task<PortalLinkMaster> UpdateAsync(PortalLinkMaster link)
        {
            link.UpdatedAt = DateTime.UtcNow;
            _context.PortalLinks.Update(link);
            await _context.SaveChangesAsync();
            return link;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var link = await _context.PortalLinks.FindAsync(id);
            if (link == null) return false;

            _context.PortalLinks.Remove(link);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
