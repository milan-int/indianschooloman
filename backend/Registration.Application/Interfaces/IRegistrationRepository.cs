using Registration.Domain.Entities;

namespace Registration.Application.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<Domain.Entities.Registration> AddRegistrationAsync(Domain.Entities.Registration registration);
        Task<Domain.Entities.Registration?> GetRegistrationByIdAsync(int id);
        Task<Domain.Entities.Registration?> GetRegistrationByNoAsync(string registrationNo);
        Task<bool> IsPassportNumberRegisteredAsync(string passportNumber);
    }
}
