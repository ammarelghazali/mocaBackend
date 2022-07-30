using AutoMapper;
using Compolitan.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.LocationManagment.District;
using MOCA.Core.Interfaces.LocationManagment.Services;

namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DistrictController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDistrictService _districtService;

        public DistrictController(IMapper mapper, IDistrictService districtService)
        {
            _mapper = mapper;
            _districtService = districtService;
        }

        /// <summary>
        /// Add A New District 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">District added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddDistrict")]
        public async Task<IActionResult> AddDistrict([FromBody] DistrictModel model)
        {
            var data = await _districtService.AddDistrict(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Update District 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">District Updated successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateDistrict")]
        public async Task<IActionResult> UpdateDistrict([FromBody] DistrictModel model)
        {
            return Ok(await _districtService.UpdateDistrict(model));
        }

        /// <summary>
        /// Gets District By ID
        /// </summary>
        /// <param name="Id">an object holds the Id of District</param>
        /// <response code="200">Returns the District</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetDistrictByID")]
        public async Task<IActionResult> GetDistrictByID([FromQuery] long Id)
        {
            var response = await _districtService.GetDistrictByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated Districts
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Districts List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllDistricts")]
        public async Task<IActionResult> GetAllDistricts([FromQuery] RequestParameter filter)
        {
            var response = await _districtService.GetAllDistrictsWithPagination(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Not Paginated Districts
        /// </summary>
        /// <response code="200">Returns the Districts List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllDistrictsWithoutPagination")]
        public async Task<IActionResult> GetAllDistrictsWithoutPagination()
        {
            var response = await _districtService.GetAllDistrictsWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Districts By CityID
        /// </summary>
        /// <param name="CountryId">an object holds the Id of Country realtes to City</param>
        /// <response code="200">Returns the City</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetDistrictsByCityID")]
        public async Task<IActionResult> GetDistrictsByCityID([FromQuery] long CityId)
        {
            var response = await _districtService.GetAllDistrictsByCityID(CityId);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes District
        /// </summary>
        /// <param name="Id">an object holds the Id of District</param>
        /// <response code="200">Deletes District and all related data successfully</response>
        /// <response code="400">District not found, or there is error while saving</response>
        [HttpDelete("DeleteDistrict")]
        public async Task<IActionResult> DeleteDistrict([FromQuery] long Id)
        {
            var response = await _districtService.DeleteDistrict(Id);
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
