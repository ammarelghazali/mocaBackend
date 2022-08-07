using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs;
using MOCA.Core.DTOs.LocationManagment.Building;
using MOCA.Core.DTOs.LocationManagment.Building.FilterParameter;
using MOCA.Core.Interfaces.LocationManagment.Services;

namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class BuildingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBuildingService _buildingService;

        public BuildingController(IMapper mapper, IBuildingService buildingService)
        {
            _mapper = mapper;
            _buildingService = buildingService;
        }

        /// <summary>
        /// Add A New Building 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Building added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddBuilding")]
        public async Task<IActionResult> AddBuilding([FromBody] BuildingModel model)
        {
            var data = await _buildingService.AddBuilding(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Update Building 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Building Updated successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateBuilding")]
        public async Task<IActionResult> UpdateBuilding([FromBody] BuildingModel model)
        {
            return Ok(await _buildingService.UpdateBuilding(model));
        }

        /// <summary>
        /// Deletes Building
        /// </summary>
        /// <param name="Id">an object holds the Id of Building</param>
        /// <response code="200">Deletes Building and all related data successfully</response>
        /// <response code="400">Building not found, or there is error while saving</response>
        [HttpDelete("DeleteBuilding")]
        public async Task<IActionResult> DeleteBuilding([FromQuery] long Id)
        {
            var response = await _buildingService.DeleteBuilding(Id);
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Building By ID
        /// </summary>
        /// <param name="Id">an object holds the Id of Building</param>
        /// <response code="200">Returns the Building</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetBuildingByID")]
        public async Task<IActionResult> GetBuildingByID([FromQuery] long Id)
        {
            var response = await _buildingService.GetBuildingByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Building By LocationID
        /// </summary>
        /// <param name="LocationID">an object holds the Id of Location</param>
        /// <response code="200">Returns the Building List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetBuildingByLocationID")]
        public async Task<IActionResult> GetBuildingByLocationID([FromQuery] long LocationID)
        {
            var response = await _buildingService.GetBuildingByLocationId(LocationID);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated Buildings
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Buildings List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllBuildingsWithPagination")]
        public async Task<IActionResult> GetAllBuildingsWithPagination([FromQuery] RequestParameter filter)
        {
            var response = await _buildingService.GetAllBuildingPaginated(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated Buildings
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Buildings List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("GetAllFilterBuildingsWithPagination")]
        public async Task<IActionResult> GetAllFilterBuildingsWithPagination([FromBody] GetPaginatedBuildingFilterParameter filter)
        {
            var response = await _buildingService.GetAllFilterBuildingPaginated(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Buildings Without Pagination
        /// </summary>
        /// <response code="200">Returns the Buildings List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllBuildingsWithoutPagination")]
        public async Task<IActionResult> GetAllBuildingsWithoutPagination()
        {
            var response = await _buildingService.GetAllBuildingWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Buildings Without Pagination
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Buildings List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("GetAllFilterBuildingsWithoutPagination")]
        public async Task<IActionResult> GetAllFilterBuildingsWithoutPagination([FromBody] GetWithoutPaginatedBuildingFilterParameter filter)
        {
            var response = await _buildingService.GetAllFilterBuildingWithoutPagination(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
