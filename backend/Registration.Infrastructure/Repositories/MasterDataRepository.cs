using Microsoft.EntityFrameworkCore;
using Registration.Application.Interfaces;
using Registration.Domain.Entities;
using Registration.Infrastructure.Data;

namespace Registration.Infrastructure.Repositories
{
    public class MasterDataRepository : IMasterDataRepository
    {
        private readonly AppDbContext _context;

        public MasterDataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PostalCodeMaster>> GetPostalCodesAsync() => await _context.PostalCodes.ToListAsync();
        public async Task<IEnumerable<MotherTongueMaster>> GetMotherTonguesAsync() => await _context.MotherTongues.OrderBy(m => m.DisplayOrder).ToListAsync();
        public async Task<IEnumerable<RelationshipMaster>> GetRelationshipsAsync() => await _context.Relationships.OrderBy(r => r.DisplayOrder).ToListAsync();
        public async Task<IEnumerable<NationalityMaster>> GetNationalitiesAsync() => await _context.Nationalities.OrderBy(n => n.DisplayOrder).ToListAsync();
        public async Task<IEnumerable<GradeMaster>> GetGradesWithSchoolsAsync() => await _context.Grades.Include(g => g.GradeSchools).ToListAsync();
        public async Task<IEnumerable<CountryMaster>> GetCountriesAsync() => await _context.Countries.OrderBy(c => c.Name).ToListAsync();
        public async Task<IEnumerable<SiblingSchoolMaster>> GetSiblingSchoolsAsync() => await _context.SiblingSchools.OrderBy(s => s.DisplayOrder).ToListAsync();
        public async Task<IEnumerable<SiblingClassMaster>> GetSiblingClassesAsync()
        {
            return await _context.SiblingClasses.OrderBy(c => c.DisplayOrder).ToListAsync();
        }

        public async Task<IEnumerable<GenderMaster>> GetGendersAsync()
        {
            return await _context.Genders.OrderBy(g => g.DisplayOrder).ToListAsync();
        }
    }
}
