using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.LocationManagment.Industry;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.Interfaces.LocationManagment.Services;

namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class IndustryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIndustryService _industryService;
        public IndustryController(IMapper mapper, IIndustryService industryService)
        {
            _mapper = mapper;
            _industryService = industryService;
        }

        /// <summary>
        /// Add A New Industry 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Industry added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddIndustry")]
        public async Task<IActionResult> AddIndustry([FromBody] IndustryModel model)
        {
            var data = await _industryService.AddIndustry(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Update Industry 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Industry Updated successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateIndustry")]
        public async Task<IActionResult> UpdateIndustry([FromBody] IndustryModel model)
        {
            return Ok(await _industryService.UpdateIndustry(model));
        }

        /// <summary>
        /// Gets Industry By ID
        /// </summary>
        /// <param name="Id">an object holds the Id of Industry</param>
        /// <response code="200">Returns the Industry</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetIndustryByID")]
        public async Task<IActionResult> GetIndustryByID([FromQuery] long Id)
        {
            var response = await _industryService.GetIndustryByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated Industrys
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Industrys List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllIndustrys")]
        public async Task<IActionResult> GetAllIndustrys([FromQuery] RequestParameter filter)
        {
            var response = await _industryService.GetAllIndustriesWithPagination(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Not Paginated Industrys
        /// </summary>
        /// <response code="200">Returns the Industrys List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllIndustrysWithoutPagination")]
        public async Task<IActionResult> GetAllIndustrysWithoutPagination()
        {
            var response = await _industryService.GetAllIndustriesWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes Industry
        /// </summary>
        /// <param name="Id">an object holds the Id of Industry</param>
        /// <response code="200">Deletes Industry and all related data successfully</response>
        /// <response code="400">Industry not found, or there is error while saving</response>
        [HttpDelete("DeleteIndustry")]
        public async Task<IActionResult> DeleteIndustry([FromQuery] long Id)
        {
            var response = await _industryService.DeleteIndustry(Id);
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
