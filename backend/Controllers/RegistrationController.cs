using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Registration.Application.Interfaces;
using Registration.Application.DTOs;
using Registration.Domain.Entities;

namespace RegistrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationRepository _repository;
        private readonly IAuthService _authService;

        public RegistrationController(IRegistrationRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        // POST: api/Registration
        [HttpPost]
        public async Task<ActionResult<Registration.Domain.Entities.Registration>> PostRegistration(RegistrationDto dto)
        {
            if (await _repository.IsPassportNumberRegisteredAsync(dto.PassportNumber))
            {
                return BadRequest(new { Message = "A student with this passport number is already registered." });
            }

            // Create Master Registration Record
            var registration = new Registration.Domain.Entities.Registration
            {
                RegistrationNo = "REG-" + DateTime.Now.Year + "-" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper(),
                Status = "SUBMITTED",
                DeclarationAccepted = dto.Declaration,
                SubmittedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            // Map Student
            registration.Student = new Student
            {
                GivenName = dto.PupilFirstName,
                Surname = dto.PupilSurname,
                PassportNumber = dto.PassportNumber,
                PassportExpiryDate = dto.PassportExpiryDate,
                AdmissionClass = dto.ClassSought,
                Gender = dto.Sex,
                Nationality = dto.Nationality,
                VisaNumber = dto.VisaNumber,
                VisaExpiryDate = dto.VisaExpiryDate,
                MotherTongue = dto.MotherTongue,
                DateOfBirth = dto.DateOfBirth ?? DateTime.UtcNow,
                PlaceOfBirth = dto.PlaceOfBirth,
                ExistingSiblings = dto.ExistingSiblings?.Select(s => new StudentExistingSibling {
                    SiblingName = s.SiblingName,
                    SchoolName = s.SchoolName,
                    GrNumber = s.GrNumber,
                    ClassName = s.ClassName,
                    Division = s.Division
                }).ToList() ?? new List<StudentExistingSibling>(),
                NewApplicantSiblings = dto.NewApplicantSiblings?.Select(s => new StudentNewApplicantSibling {
                    PassportNo = s.PassportNo,
                    Relationship = s.Relationship
                }).ToList() ?? new List<StudentNewApplicantSibling>()
            };
            
            // Map Parent
            registration.Parent = new Parent
            {
                GivenName = dto.ParentName,
                Surname = dto.ParentSurname,
                Relationship = dto.Relationship,
                CivilId = dto.CivilNo,
                Email = dto.Email,
                MobileNo = dto.Gsm,
                Employer = dto.Employer,
                Occupation = dto.Occupation,
                Nationality = dto.ParentNationality
            };
            
            // Map Address
            registration.Address = new Address
            {
                PostalCode = dto.PostalCode,
                PoBox = dto.PoBox,
                HouseNo = dto.HouseFlatNo,
                LaneWayNo = dto.WayNo,
                StreetName = dto.StreetName,
                Locality = dto.Locality,
                PermanentAddress = dto.PermanentAddress
            };
            
            // Map Application Details
            registration.ApplicationDetail = new ApplicationDetail
            {
                PreviousSchoolName = dto.PreviousSchool,
                PreviousSchoolCountry = dto.Country,
                PreviousClassStudied = dto.ClassLastAttended,
                SiblingsInIndianSchools = dto.SiblingsStudyingCount,
                SiblingsSeekingAdmission = dto.SiblingsSeekingAdmissionCount
            };

            registration.SchoolPreferences = dto.SchoolPreferences
                .Select((name, index) => new RegistrationSchoolPreference
                {
                    SchoolName = name,
                    PreferenceOrder = index + 1
                }).ToList();
            
            await _repository.AddRegistrationAsync(registration);

            // Auto-provision client user account for parent/student login
            try
            {
                await _authService.CreateClientAccountForRegistrationAsync(registration);
            }
            catch
            {
                // Account creation exception suppressed if already exists
            }

            return CreatedAtAction(nameof(GetRegistration), new { id = registration.Id }, registration);
        }

        // GET: api/Registration/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Registration.Domain.Entities.Registration>> GetRegistration(int id)
        {
            var registration = await _repository.GetRegistrationByIdAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        // GET: api/Registration/preview/{id}
        [HttpGet("preview/{id:int}")]
        public async Task<ActionResult<Registration.Domain.Entities.Registration>> GetRegistrationPreview(int id)
        {
            var registration = await _repository.GetRegistrationByIdAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            return registration;
        }

        // GET: api/Registration/check-passport/{passportNumber}
        [HttpGet("check-passport/{passportNumber}")]
        public async Task<ActionResult<bool>> CheckPassportExists(string passportNumber)
        {
            bool exists = await _repository.IsPassportNumberRegisteredAsync(passportNumber);
            return Ok(new { exists });
        }
    }
}

