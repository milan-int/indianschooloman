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

        #region Portal Links
        public async Task<IEnumerable<PortalLinkMaster>> GetActiveLinksAsync()
        {
            return await _context.PortalLinks
                .Where(x => x.IsActive && !x.IsDeleted)
                .OrderBy(x => x.Section)
                .ThenBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<PortalLinkMaster>> GetAllLinksAsync(bool includeDeleted = false)
        {
            return await _context.PortalLinks
                .Where(x => includeDeleted || !x.IsDeleted)
                .OrderBy(x => x.Section)
                .ThenBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<PortalLinkMaster?> GetByIdAsync(int id)
        {
            return await _context.PortalLinks.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<PortalLinkMaster> AddAsync(PortalLinkMaster link)
        {
            link.CreatedAt = DateTime.UtcNow;
            link.IsDeleted = false;
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

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var link = await _context.PortalLinks.FindAsync(id);
            if (link == null || link.IsDeleted) return false;

            link.IsDeleted = true;
            link.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Schools Matrix
        public async Task<IEnumerable<PortalSchoolMaster>> GetActiveSchoolsAsync()
        {
            return await _context.PortalSchools
                .Where(x => x.IsActive && !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.SlNo)
                .ToListAsync();
        }

        public async Task<IEnumerable<PortalSchoolMaster>> GetAllSchoolsAsync(bool includeDeleted = false)
        {
            return await _context.PortalSchools
                .Where(x => includeDeleted || !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.SlNo)
                .ToListAsync();
        }

        public async Task<PortalSchoolMaster?> GetSchoolByIdAsync(int id)
        {
            return await _context.PortalSchools.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<PortalSchoolMaster> AddSchoolAsync(PortalSchoolMaster school)
        {
            school.CreatedAt = DateTime.UtcNow;
            school.IsDeleted = false;
            await _context.PortalSchools.AddAsync(school);
            await _context.SaveChangesAsync();
            return school;
        }

        public async Task<PortalSchoolMaster> UpdateSchoolAsync(PortalSchoolMaster school)
        {
            school.UpdatedAt = DateTime.UtcNow;
            _context.PortalSchools.Update(school);
            await _context.SaveChangesAsync();
            return school;
        }

        public async Task<bool> SoftDeleteSchoolAsync(int id)
        {
            var school = await _context.PortalSchools.FindAsync(id);
            if (school == null || school.IsDeleted) return false;

            school.IsDeleted = true;
            school.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Guidelines
        public async Task<IEnumerable<PortalGuidelineMaster>> GetActiveGuidelinesAsync()
        {
            return await _context.PortalGuidelines
                .Where(x => x.IsActive && !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<PortalGuidelineMaster>> GetAllGuidelinesAsync(bool includeDeleted = false)
        {
            return await _context.PortalGuidelines
                .Where(x => includeDeleted || !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();
        }

        public async Task<PortalGuidelineMaster?> GetGuidelineByIdAsync(int id)
        {
            return await _context.PortalGuidelines.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<PortalGuidelineMaster> AddGuidelineAsync(PortalGuidelineMaster guideline)
        {
            guideline.CreatedAt = DateTime.UtcNow;
            guideline.IsDeleted = false;
            await _context.PortalGuidelines.AddAsync(guideline);
            await _context.SaveChangesAsync();
            return guideline;
        }

        public async Task<PortalGuidelineMaster> UpdateGuidelineAsync(PortalGuidelineMaster guideline)
        {
            guideline.UpdatedAt = DateTime.UtcNow;
            _context.PortalGuidelines.Update(guideline);
            await _context.SaveChangesAsync();
            return guideline;
        }

        public async Task<bool> SoftDeleteGuidelineAsync(int id)
        {
            var guideline = await _context.PortalGuidelines.FindAsync(id);
            if (guideline == null || guideline.IsDeleted) return false;

            guideline.IsDeleted = true;
            guideline.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Portal Configs
        public async Task<IEnumerable<PortalConfigMaster>> GetActiveConfigsAsync()
        {
            return await _context.PortalConfigs
                .Where(x => x.IsActive && !x.IsDeleted)
                .OrderBy(x => x.Section)
                .ThenBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<PortalConfigMaster>> GetAllConfigsAsync()
        {
            return await _context.PortalConfigs
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Section)
                .ThenBy(x => x.Id)
                .ToListAsync();
        }

        public async Task<PortalConfigMaster?> GetConfigByKeyAsync(string key)
        {
            return await _context.PortalConfigs
                .FirstOrDefaultAsync(x => x.ConfigKey.ToLower() == key.ToLower() && !x.IsDeleted);
        }

        public async Task<PortalConfigMaster> UpdateConfigAsync(PortalConfigMaster config)
        {
            config.UpdatedAt = DateTime.UtcNow;
            _context.PortalConfigs.Update(config);
            await _context.SaveChangesAsync();
            return config;
        }
        #endregion
    }
}
