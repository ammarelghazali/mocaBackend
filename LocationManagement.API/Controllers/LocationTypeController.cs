using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.LocationManagment.LocationType;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.Interfaces.LocationManagment.Services;

namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class LocationTypeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILocationTypeService _locationTypeService;

        public LocationTypeController(IMapper mapper, ILocationTypeService locationTypeService)
        {
            _mapper = mapper;
            _locationTypeService = locationTypeService;
        }

        /// <summary>
        /// Add A New LocationType 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">LocationType added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddLocationType")]
        public async Task<IActionResult> AddLocationType([FromBody] LocationTypeModel model)
        {
            var data = await _locationTypeService.AddLocationType(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Update LocationType 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">LocationType Updated successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateLocationType")]
        public async Task<IActionResult> UpdateLocationType([FromBody] LocationTypeModel model)
        {
            return Ok(await _locationTypeService.UpdateLocationType(model));
        }

        /// <summary>
        /// Gets LocationType By ID
        /// </summary>
        /// <param name="Id">an object holds the Id of LocationType</param>
        /// <response code="200">Returns the City</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetLocationTypeByID")]
        public async Task<IActionResult> GetLocationTypeByID([FromQuery] long Id)
        {
            var response = await _locationTypeService.GetLocationTypeByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated LocationTypes
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the LocationTypes List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllLocationTypes")]
        public async Task<IActionResult> GetAllLocationTypes([FromQuery] RequestParameter filter)
        {
            var response = await _locationTypeService.GetAllLocationTypesWithPagination(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Not Paginated LocationTypes
        /// </summary>
        /// <response code="200">Returns the LocationTypes List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllLocationTypesWithoutPagination")]
        public async Task<IActionResult> GetAllLocationTypesWithoutPagination()
        {
            var response = await _locationTypeService.GetAllLocationTypesWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes LocationType
        /// </summary>
        /// <param name="Id">an object holds the Id of LocationType</param>
        /// <response code="200">Deletes LocationType and all related data successfully</response>
        /// <response code="400">City not found, or there is error while saving</response>
        [HttpDelete("DeleteLocationType")]
        public async Task<IActionResult> DeleteLocationType([FromQuery] long Id)
        {
            var response = await _locationTypeService.DeleteLocationType(Id);
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
