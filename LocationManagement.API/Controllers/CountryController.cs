using AutoMapper;
using Compolitan.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.LocationManagment.Country;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;
using MOCA.Core.Interfaces.LocationManagment.Services;

namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CountryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryService _countryService;

        public CountryController(IMapper mapper, ICountryService countryService)
        {
            _mapper = mapper;
            _countryService = countryService;
        }

        /// <summary>
        /// Add A New Country 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Country added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddCountry")]
        public async Task<IActionResult> AddCountry([FromBody] CountryModel model)
        {
            var data = await _countryService.AddCountry(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Update Country 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Country Updated successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateCountry")]
        public async Task<IActionResult> UpdateCountry([FromBody] CountryModel model)
        {
            return Ok(await _countryService.UpdateCountry(model));
        }

        /// <summary>
        /// Gets Country By ID
        /// </summary>
        /// <param name="Id">an object holds the Id of Country</param>
        /// <response code="200">Returns the Country</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetCountryByID")]
        public async Task<IActionResult> GetCountryByID([FromQuery] long Id)
        {
            var response = await _countryService.GetCountryByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated Countries
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Countries List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllCountries")]
        public async Task<IActionResult> GetAllCountries([FromQuery] RequestParameter filter)
        {
            var response = await _countryService.GetAllCountryWithPagination(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Not Paginated Countries
        /// </summary>
        /// <response code="200">Returns the Countries List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllCountriesWithoutPagination")]
        public async Task<IActionResult> GetAllCountriesWithoutPagination()
        {
            var response = await _countryService.GetAllCountryWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes Country
        /// </summary>
        /// <response code="200">Deletes Country and all related data successfully</response>
        /// <response code="400">Country not found, or there is error while saving</response>
        [HttpDelete("DeleteCountry")]
        public async Task<IActionResult> DeleteCountry([FromQuery] long Id)
        {
            var response = await _countryService.DeleteCountry(Id);
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
