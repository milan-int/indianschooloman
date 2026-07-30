using Microsoft.EntityFrameworkCore;
using Registration.Application.Interfaces;
using Registration.Domain.Entities;
using Registration.Infrastructure.Data;

namespace Registration.Infrastructure.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly AppDbContext _context;

        public RegistrationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Registration> AddRegistrationAsync(Domain.Entities.Registration registration)
        {
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
            return registration;
        }

        public async Task<Domain.Entities.Registration?> GetRegistrationByIdAsync(int id)
        {
            return await _context.Registrations
                .Include(r => r.Student)
                    .ThenInclude(s => s.ExistingSiblings)
                .Include(r => r.Student)
                    .ThenInclude(s => s.NewApplicantSiblings)
                .Include(r => r.Parent)
                .Include(r => r.Address)
                .Include(r => r.ApplicationDetail)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Domain.Entities.Registration?> GetRegistrationByNoAsync(string registrationNo)
        {
            return await _context.Registrations
                .Include(r => r.Student)
                    .ThenInclude(s => s.ExistingSiblings)
                .Include(r => r.Student)
                    .ThenInclude(s => s.NewApplicantSiblings)
                .Include(r => r.Parent)
                .Include(r => r.Address)
                .Include(r => r.ApplicationDetail)
                .FirstOrDefaultAsync(r => r.RegistrationNo == registrationNo);
        }

        public async Task<bool> IsPassportNumberRegisteredAsync(string passportNumber)
        {
            return await _context.Registrations
                .AnyAsync(r => r.Student.PassportNumber.ToLower() == passportNumber.ToLower());
        }
    }
}
