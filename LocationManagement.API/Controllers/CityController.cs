using AutoMapper;
using Compolitan.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.LocationManagment.City;
using MOCA.Core.Interfaces.LocationManagment.Services;

namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;

        public CityController(IMapper mapper, ICityService cityService)
        {
            _mapper = mapper;
            _cityService = cityService;
        }

        /// <summary>
        /// Add A New City 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">City added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddCity")]
        public async Task<IActionResult> AddCity([FromBody] CityModel model)
        {
            var data = await _cityService.AddCity(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Update City 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">City Updated successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateCity")]
        public async Task<IActionResult> UpdateCity([FromBody] CityModel model)
        {
            return Ok(await _cityService.UpdateCity(model));
        }

        /// <summary>
        /// Gets City By ID
        /// </summary>
        /// <param name="Id">an object holds the Id of City</param>
        /// <response code="200">Returns the City</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetCityByID")]
        public async Task<IActionResult> GetCityByID([FromQuery] long Id)
        {
            var response = await _cityService.GetCityByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated Cities
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Cities List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllCities")]
        public async Task<IActionResult> GetAllCities([FromQuery] RequestParameter filter)
        {
            var response = await _cityService.GetAllCityWithPagination(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Not Paginated Cities
        /// </summary>
        /// <response code="200">Returns the Cities List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllCitiesWithoutPagination")]
        public async Task<IActionResult> GetAllCitiesWithoutPagination()
        {
            var response = await _cityService.GetAllCityWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Cities By CountryID
        /// </summary>
        /// <param name="CountryId">an object holds the Id of Country realtes to City</param>
        /// <response code="200">Returns the City</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetCitiesByCountryID")]
        public async Task<IActionResult> GetCitiesByCountryID([FromQuery] long CountryId)
        {
            var response = await _cityService.GetAllCityByCountryID(CountryId);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes City
        /// </summary>
        /// <param name="Id">an object holds the Id of City</param>
        /// <response code="200">Deletes City and all related data successfully</response>
        /// <response code="400">City not found, or there is error while saving</response>
        [HttpDelete("DeleteCountry")]
        public async Task<IActionResult> DeleteCountry([FromQuery] long Id)
        {
            var response = await _cityService.DeleteCity(Id);
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
