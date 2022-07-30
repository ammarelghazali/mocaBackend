using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.LocationManagment.Feature;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.Interfaces.LocationManagment.Services;

namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FeatureController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFeatureService _featureService;

        public FeatureController(IMapper mapper, IFeatureService featureService)
        {
            _mapper = mapper;
            _featureService = featureService;
        }

        /// <summary>
        /// Add A New Feature 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Feature added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddFeature")]
        public async Task<IActionResult> AddFeature([FromBody] FeatureModel model)
        {
            var data = await _featureService.AddFeature(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Update Feature 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Feature Updated successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateFeature")]
        public async Task<IActionResult> UpdateFeature([FromBody] FeatureModel model)
        {
            return Ok(await _featureService.UpdateFeature(model));
        }

        /// <summary>
        /// Gets Feature By ID
        /// </summary>
        /// <param name="Id">an object holds the Id of Feature</param>
        /// <response code="200">Returns the Feature</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetFeatureByID")]
        public async Task<IActionResult> GetFeatureByID([FromQuery] long Id)
        {
            var response = await _featureService.GetFeatureByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated Features
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Features List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllFeatures")]
        public async Task<IActionResult> GetAllFeatures([FromQuery] RequestParameter filter)
        {
            var response = await _featureService.GetAllFeaturesWithPagination(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Not Paginated Features
        /// </summary>
        /// <response code="200">Returns the Features List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllFeaturesWithoutPagination")]
        public async Task<IActionResult> GetAllFeaturesWithoutPagination()
        {
            var response = await _featureService.GetAllFeaturesWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes Feature
        /// </summary>
        /// <param name="Id">an object holds the Id of Feature</param>
        /// <response code="200">Deletes Feature and all related data successfully</response>
        /// <response code="400">Feature not found, or there is error while saving</response>
        [HttpDelete("DeleteFeature")]
        public async Task<IActionResult> DeleteFeature([FromQuery] long Id)
        {
            var response = await _featureService.DeleteFeature(Id);
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
