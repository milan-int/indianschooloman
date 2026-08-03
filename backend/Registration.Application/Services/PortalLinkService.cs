using Registration.Application.DTOs;
using Registration.Application.Interfaces;
using Registration.Domain.Entities;

namespace Registration.Application.Services
{
    public class PortalLinkService : IPortalLinkService
    {
        private readonly IPortalLinkRepository _repository;

        public PortalLinkService(IPortalLinkRepository repository)
        {
            _repository = repository;
        }

        public async Task<LandingPageDataDto> GetLandingPageDataAsync()
        {
            var links = await _repository.GetActiveLinksAsync();

            var admissionLinks = links
                .Where(l => l.Section.Equals("ADMISSION_LINK", StringComparison.OrdinalIgnoreCase))
                .OrderBy(l => l.DisplayOrder)
                .Select(MapToDto);

            var footerLinks = links
                .Where(l => l.Section.Equals("FOOTER_LINK", StringComparison.OrdinalIgnoreCase))
                .OrderBy(l => l.DisplayOrder)
                .Select(MapToDto);

            var schools = GetSchoolMatrixData();
            var guidelines = GetGuidelinesData();
            var contact = new PortalContactDto
            {
                HelplinePhone = "+968 2470 2567 / 2479 9700",
                HelplineEmail = "admissions@indianschoolsoman.com",
                OfficeHours = "Sunday to Thursday (8:00 AM – 2:00 PM)",
                AcademicYear = "2026–2027"
            };

            return new LandingPageDataDto
            {
                AdmissionLinks = admissionLinks,
                FooterLinks = footerLinks,
                Schools = schools,
                Guidelines = guidelines,
                Contact = contact
            };
        }

        public async Task<IEnumerable<PortalLinkDto>> GetAllLinksAsync()
        {
            var links = await _repository.GetAllLinksAsync();
            return links.Select(MapToDto);
        }

        public async Task<PortalLinkDto?> GetByIdAsync(int id)
        {
            var link = await _repository.GetByIdAsync(id);
            return link == null ? null : MapToDto(link);
        }

        public async Task<PortalLinkDto> CreateLinkAsync(CreatePortalLinkDto dto)
        {
            var entity = new PortalLinkMaster
            {
                Title = dto.Title.Trim(),
                Section = dto.Section.Trim().ToUpperInvariant(),
                LinkType = dto.LinkType.Trim().ToUpperInvariant(),
                TargetUrl = dto.TargetUrl.Trim(),
                Description = dto.Description?.Trim(),
                DisplayOrder = dto.DisplayOrder,
                IsActive = dto.IsActive,
                OpenInNewTab = dto.OpenInNewTab
            };

            var created = await _repository.AddAsync(entity);
            return MapToDto(created);
        }

        public async Task<PortalLinkDto?> UpdateLinkAsync(int id, UpdatePortalLinkDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Title = dto.Title.Trim();
            existing.Section = dto.Section.Trim().ToUpperInvariant();
            existing.LinkType = dto.LinkType.Trim().ToUpperInvariant();
            existing.TargetUrl = dto.TargetUrl.Trim();
            existing.Description = dto.Description?.Trim();
            existing.DisplayOrder = dto.DisplayOrder;
            existing.IsActive = dto.IsActive;
            existing.OpenInNewTab = dto.OpenInNewTab;

            var updated = await _repository.UpdateAsync(existing);
            return MapToDto(updated);
        }

        public async Task<bool> ToggleStatusAsync(int id, bool isActive)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IsActive = isActive;
            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteLinkAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static List<SchoolMatrixDto> GetSchoolMatrixData()
        {
            return new List<SchoolMatrixDto>
            {
                new SchoolMatrixDto { SlNo = 1, Name = "Indian School Muscat", Code = "ISM", Syllabus = "CBSE", Classes = "KG I – IX & XI", Location = "Darsait / Muscat", Website = "https://ismoman.com" },
                new SchoolMatrixDto { SlNo = 2, Name = "Indian School Darsait", Code = "ISD", Syllabus = "CBSE", Classes = "KG I – IX & XI", Location = "Darsait", Website = "https://isdoman.com" },
                new SchoolMatrixDto { SlNo = 3, Name = "Indian School Al Wadi Al Kabir", Code = "ISWK", Syllabus = "CBSE", Classes = "KG I – IX & XI", Location = "Wadi Kabir", Website = "https://iswkoman.com" },
                new SchoolMatrixDto { SlNo = 4, Name = "Indian School Al Wadi Al Kabir International", Code = "ISWKi", Syllabus = "CAMBRIDGE", Classes = "KG I – IX & XI", Location = "Wadi Kabir", Website = "https://iswkoman.com" },
                new SchoolMatrixDto { SlNo = 5, Name = "Indian School Al Ghubra", Code = "ISG", Syllabus = "CBSE", Classes = "KG I – IX & XI", Location = "Al Ghubra", Website = "https://isgoman.com" },
                new SchoolMatrixDto { SlNo = 6, Name = "Indian School Al Ghubra International", Code = "ISGi", Syllabus = "CAMBRIDGE", Classes = "KG I – IX & XI", Location = "Al Ghubra", Website = "https://isgoman.com" },
                new SchoolMatrixDto { SlNo = 7, Name = "Indian School Bousher", Code = "ISB", Syllabus = "CBSE", Classes = "KG I – IX & XI", Location = "Bousher", Website = "https://isboman.com" },
                new SchoolMatrixDto { SlNo = 8, Name = "Indian School Seeb", Code = "ISAS", Syllabus = "CBSE", Classes = "KG I – IX & XI", Location = "Al Seeb", Website = "https://isseeoman.com" },
                new SchoolMatrixDto { SlNo = 9, Name = "Indian School Maabela", Code = "ISAM", Syllabus = "CBSE", Classes = "KG I – IX & XI", Location = "Al Maabela", Website = "https://isamoman.com" }
            };
        }

        private static List<GuidelineInstructionDto> GetGuidelinesData()
        {
            return new List<GuidelineInstructionDto>
            {
                new GuidelineInstructionDto { Id = 1, Title = "Eligibility", Detail = "This online registration form is meant for Indian Nationals seeking new admissions in Indian Schools in the capital area for the academic year 2026-2027." },
                new GuidelineInstructionDto { Id = 2, Title = "Single Mandatory Application", Detail = "Online registration is mandatory. There is only one application form required for one child; our system will not accept duplicate passport entries." },
                new GuidelineInstructionDto { Id = 3, Title = "Credentials & Notifications", Detail = "A unique login registration number and password will be generated automatically upon submission and sent to your registered email and mobile number." },
                new GuidelineInstructionDto { Id = 4, Title = "Application Processing Fee", Detail = "A non-refundable processing fee of OMR 15/- is payable upon successful submission of the application form." },
                new GuidelineInstructionDto { Id = 5, Title = "Sibling Preference Rule", Detail = "Online application is mandatory even for sibling admissions. To claim sibling preference, the parent must select the sibling's school as their First Preference." },
                new GuidelineInstructionDto { Id = 6, Title = "Seat Vacancies", Detail = "Tentative vacancies across different schools are dynamically updated on the portal for parents to review before submitting preferences." },
                new GuidelineInstructionDto { Id = 7, Title = "Admission Allotment", Detail = "School allotment is strictly subject to vacancy availability and merit criteria set by the Board of Directors." },
                new GuidelineInstructionDto { Id = 8, Title = "Help & Queries", Detail = "Parents are strongly advised to check the Frequently Asked Questions (FAQs) section for guidance on common registration questions." },
                new GuidelineInstructionDto { Id = 9, Title = "Inter-School Transfer", Detail = "Parents seeking inter-school transfer for their wards must complete the dedicated transfer portal:", Link = "https://forms.gle/P29avN2BoVufqWGz5", LinkText = "Inter-School Transfer Form" },
                new GuidelineInstructionDto { Id = 10, Title = "Other Nationalities", Detail = "Parents of non-Indian nationalities seeking admission in Indian schools must apply through the external foreign quota portal:", Link = "https://forms.gle/hEUAnuLePfyTveD89", LinkText = "Other Nationalities Form" }
            };
        }

        private static PortalLinkDto MapToDto(PortalLinkMaster entity)
        {
            return new PortalLinkDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Section = entity.Section,
                LinkType = entity.LinkType,
                TargetUrl = entity.TargetUrl,
                Description = entity.Description,
                DisplayOrder = entity.DisplayOrder,
                IsActive = entity.IsActive,
                OpenInNewTab = entity.OpenInNewTab
            };
        }
    }
}
