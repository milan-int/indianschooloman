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

        #region Landing Page Data
        public async Task<LandingPageDataDto> GetLandingPageDataAsync()
        {
            var links = await _repository.GetActiveLinksAsync();

            var admissionLinks = links
                .Where(l => l.Section.Equals("ADMISSION_LINK", StringComparison.OrdinalIgnoreCase))
                .OrderBy(l => l.DisplayOrder)
                .Select(MapLinkToDto);

            var footerLinks = links
                .Where(l => l.Section.Equals("FOOTER_LINK", StringComparison.OrdinalIgnoreCase))
                .OrderBy(l => l.DisplayOrder)
                .Select(MapLinkToDto);

            var schools = (await _repository.GetActiveSchoolsAsync())
                .OrderBy(s => s.DisplayOrder)
                .ThenBy(s => s.SlNo)
                .Select(MapSchoolToDto);

            var guidelines = (await _repository.GetActiveGuidelinesAsync())
                .OrderBy(g => g.DisplayOrder)
                .Select(MapGuidelineToDto);

            var configs = (await _repository.GetActiveConfigsAsync()).ToList();

            var contact = new PortalContactDto
            {
                HelplinePhone = configs.FirstOrDefault(c => c.ConfigKey.Equals("HelplinePhone", StringComparison.OrdinalIgnoreCase))?.ConfigValue ?? "+968 2470 2567 / 2479 9700",
                HelplineEmail = configs.FirstOrDefault(c => c.ConfigKey.Equals("HelplineEmail", StringComparison.OrdinalIgnoreCase))?.ConfigValue ?? "admissions@indianschoolsoman.com",
                OfficeHours = configs.FirstOrDefault(c => c.ConfigKey.Equals("OfficeHours", StringComparison.OrdinalIgnoreCase))?.ConfigValue ?? "Sunday to Thursday (8:00 AM – 2:00 PM)",
                AcademicYear = configs.FirstOrDefault(c => c.ConfigKey.Equals("AcademicYear", StringComparison.OrdinalIgnoreCase))?.ConfigValue ?? "2026–2027"
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
        #endregion

        #region Portal Links
        public async Task<IEnumerable<PortalLinkDto>> GetAllLinksAsync(bool includeDeleted = false)
        {
            var links = await _repository.GetAllLinksAsync(includeDeleted);
            return links.Select(MapLinkToDto);
        }

        public async Task<PortalLinkDto?> GetByIdAsync(int id)
        {
            var link = await _repository.GetByIdAsync(id);
            return link == null ? null : MapLinkToDto(link);
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
                IsDeleted = false,
                OpenInNewTab = dto.OpenInNewTab
            };

            var created = await _repository.AddAsync(entity);
            return MapLinkToDto(created);
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
            return MapLinkToDto(updated);
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
            return await _repository.SoftDeleteAsync(id);
        }
        #endregion

        #region Schools Matrix
        public async Task<IEnumerable<PortalSchoolDto>> GetAllSchoolsAsync(bool includeDeleted = false)
        {
            var schools = await _repository.GetAllSchoolsAsync(includeDeleted);
            return schools.Select(MapSchoolToDto);
        }

        public async Task<PortalSchoolDto?> GetSchoolByIdAsync(int id)
        {
            var school = await _repository.GetSchoolByIdAsync(id);
            return school == null ? null : MapSchoolToDto(school);
        }

        public async Task<PortalSchoolDto> CreateSchoolAsync(CreateSchoolDto dto)
        {
            var entity = new PortalSchoolMaster
            {
                SlNo = dto.SlNo,
                Name = dto.Name.Trim(),
                Code = dto.Code.Trim().ToUpperInvariant(),
                Syllabus = dto.Syllabus.Trim().ToUpperInvariant(),
                Classes = dto.Classes.Trim(),
                Location = dto.Location.Trim(),
                Website = dto.Website.Trim(),
                DisplayOrder = dto.DisplayOrder,
                IsActive = dto.IsActive,
                IsDeleted = false
            };

            var created = await _repository.AddSchoolAsync(entity);
            return MapSchoolToDto(created);
        }

        public async Task<PortalSchoolDto?> UpdateSchoolAsync(int id, UpdateSchoolDto dto)
        {
            var existing = await _repository.GetSchoolByIdAsync(id);
            if (existing == null) return null;

            existing.SlNo = dto.SlNo;
            existing.Name = dto.Name.Trim();
            existing.Code = dto.Code.Trim().ToUpperInvariant();
            existing.Syllabus = dto.Syllabus.Trim().ToUpperInvariant();
            existing.Classes = dto.Classes.Trim();
            existing.Location = dto.Location.Trim();
            existing.Website = dto.Website.Trim();
            existing.DisplayOrder = dto.DisplayOrder;
            existing.IsActive = dto.IsActive;

            var updated = await _repository.UpdateSchoolAsync(existing);
            return MapSchoolToDto(updated);
        }

        public async Task<bool> ToggleSchoolStatusAsync(int id, bool isActive)
        {
            var existing = await _repository.GetSchoolByIdAsync(id);
            if (existing == null) return false;

            existing.IsActive = isActive;
            await _repository.UpdateSchoolAsync(existing);
            return true;
        }

        public async Task<bool> DeleteSchoolAsync(int id)
        {
            return await _repository.SoftDeleteSchoolAsync(id);
        }
        #endregion

        #region Guidelines
        public async Task<IEnumerable<PortalGuidelineDto>> GetAllGuidelinesAsync(bool includeDeleted = false)
        {
            var guidelines = await _repository.GetAllGuidelinesAsync(includeDeleted);
            return guidelines.Select(MapGuidelineToDto);
        }

        public async Task<PortalGuidelineDto?> GetGuidelineByIdAsync(int id)
        {
            var guideline = await _repository.GetGuidelineByIdAsync(id);
            return guideline == null ? null : MapGuidelineToDto(guideline);
        }

        public async Task<PortalGuidelineDto> CreateGuidelineAsync(CreateGuidelineDto dto)
        {
            var entity = new PortalGuidelineMaster
            {
                DisplayOrder = dto.DisplayOrder,
                Title = dto.Title.Trim(),
                Detail = dto.Detail.Trim(),
                Link = dto.Link?.Trim(),
                LinkText = dto.LinkText?.Trim(),
                IsActive = dto.IsActive,
                IsDeleted = false
            };

            var created = await _repository.AddGuidelineAsync(entity);
            return MapGuidelineToDto(created);
        }

        public async Task<PortalGuidelineDto?> UpdateGuidelineAsync(int id, UpdateGuidelineDto dto)
        {
            var existing = await _repository.GetGuidelineByIdAsync(id);
            if (existing == null) return null;

            existing.DisplayOrder = dto.DisplayOrder;
            existing.Title = dto.Title.Trim();
            existing.Detail = dto.Detail.Trim();
            existing.Link = dto.Link?.Trim();
            existing.LinkText = dto.LinkText?.Trim();
            existing.IsActive = dto.IsActive;

            var updated = await _repository.UpdateGuidelineAsync(existing);
            return MapGuidelineToDto(updated);
        }

        public async Task<bool> ToggleGuidelineStatusAsync(int id, bool isActive)
        {
            var existing = await _repository.GetGuidelineByIdAsync(id);
            if (existing == null) return false;

            existing.IsActive = isActive;
            await _repository.UpdateGuidelineAsync(existing);
            return true;
        }

        public async Task<bool> DeleteGuidelineAsync(int id)
        {
            return await _repository.SoftDeleteGuidelineAsync(id);
        }
        #endregion

        #region Configs
        public async Task<IEnumerable<PortalConfigDto>> GetAllConfigsAsync()
        {
            var configs = await _repository.GetAllConfigsAsync();
            return configs.Select(MapConfigToDto);
        }

        public async Task<PortalConfigDto?> GetConfigByKeyAsync(string key)
        {
            var config = await _repository.GetConfigByKeyAsync(key);
            return config == null ? null : MapConfigToDto(config);
        }

        public async Task<PortalConfigDto?> UpdateConfigAsync(string key, UpdateConfigDto dto)
        {
            var config = await _repository.GetConfigByKeyAsync(key);
            if (config == null) return null;

            config.ConfigValue = dto.ConfigValue.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Description))
            {
                config.Description = dto.Description.Trim();
            }
            config.IsActive = dto.IsActive;

            var updated = await _repository.UpdateConfigAsync(config);
            return MapConfigToDto(updated);
        }
        #endregion

        #region Mappers
        private static PortalLinkDto MapLinkToDto(PortalLinkMaster entity)
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
                IsDeleted = entity.IsDeleted,
                OpenInNewTab = entity.OpenInNewTab
            };
        }

        private static PortalSchoolDto MapSchoolToDto(PortalSchoolMaster entity)
        {
            return new PortalSchoolDto
            {
                Id = entity.Id,
                SlNo = entity.SlNo,
                Name = entity.Name,
                Code = entity.Code,
                Syllabus = entity.Syllabus,
                Classes = entity.Classes,
                Location = entity.Location,
                Website = entity.Website,
                DisplayOrder = entity.DisplayOrder,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted
            };
        }

        private static PortalGuidelineDto MapGuidelineToDto(PortalGuidelineMaster entity)
        {
            return new PortalGuidelineDto
            {
                Id = entity.Id,
                DisplayOrder = entity.DisplayOrder,
                Title = entity.Title,
                Detail = entity.Detail,
                Link = entity.Link,
                LinkText = entity.LinkText,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted
            };
        }

        private static PortalConfigDto MapConfigToDto(PortalConfigMaster entity)
        {
            return new PortalConfigDto
            {
                Id = entity.Id,
                ConfigKey = entity.ConfigKey,
                ConfigValue = entity.ConfigValue,
                Section = entity.Section,
                Description = entity.Description,
                IsActive = entity.IsActive,
                IsDeleted = entity.IsDeleted
            };
        }
        #endregion
    }
}
