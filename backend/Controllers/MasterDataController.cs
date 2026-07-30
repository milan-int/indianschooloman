using Microsoft.AspNetCore.Mvc;
using Registration.Application.DTOs;
using Registration.Application.Interfaces;

namespace RegistrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService _masterDataService;

        public MasterDataController(IMasterDataService masterDataService)
        {
            _masterDataService = masterDataService;
        }

        [HttpGet("postalcodes")]
        public async Task<ActionResult<IEnumerable<PostalCodeDto>>> GetPostalCodes()
        {
            var codes = await _masterDataService.GetPostalCodesAsync();
            return Ok(codes);
        }

        [HttpGet("mothertongues")]
        public async Task<ActionResult<IEnumerable<MotherTongueDto>>> GetMotherTongues()
        {
            var tongues = await _masterDataService.GetMotherTonguesAsync();
            return Ok(tongues);
        }

        [HttpGet("relationships")]
        public async Task<ActionResult<IEnumerable<RelationshipDto>>> GetRelationships()
        {
            var relations = await _masterDataService.GetRelationshipsAsync();
            return Ok(relations);
        }

        [HttpGet("siblingrelationships")]
        public async Task<ActionResult<IEnumerable<RelationshipDto>>> GetSiblingRelationships()
        {
            var relations = await _masterDataService.GetSiblingRelationshipsAsync();
            return Ok(relations);
        }

        [HttpGet("nationalities")]
        public async Task<ActionResult<IEnumerable<NationalityDto>>> GetNationalities()
        {
            var nationalities = await _masterDataService.GetNationalitiesAsync();
            return Ok(nationalities);
        }

        [HttpGet("countries")]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            var countries = await _masterDataService.GetCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("genders")]
        public async Task<ActionResult<IEnumerable<GenderDto>>> GetGenders()
        {
            var genders = await _masterDataService.GetGendersAsync();
            return Ok(genders);
        }

        [HttpGet("grades")]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetGrades()
        {
            var grades = await _masterDataService.GetGradesWithSchoolsAsync();
            return Ok(grades);
        }

        [HttpGet("siblingschools")]
        public async Task<ActionResult<IEnumerable<SiblingSchoolDto>>> GetSiblingSchools()
        {
            var schools = await _masterDataService.GetSiblingSchoolsAsync();
            return Ok(schools);
        }

        [HttpGet("siblingclasses")]
        public async Task<ActionResult<IEnumerable<SiblingClassDto>>> GetSiblingClasses()
        {
            var classes = await _masterDataService.GetSiblingClassesAsync();
            return Ok(classes);
        }
    }
}

