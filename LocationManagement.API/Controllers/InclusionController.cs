using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOCA.Core.DTOs.LocationManagment.Inclusion;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Settings;
using System.Net.Http.Headers;

namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class InclusionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IInclusionService _inclusionService;
        private readonly IUploadImageService _uploadImageService;
        private readonly FileSettings _fileSettings;
        public InclusionController(IMapper mapper, IInclusionService inclusionService, IOptions<FileSettings> fileSettings, IUploadImageService uploadImageService)
        {
            _mapper = mapper;
            _inclusionService = inclusionService;
            _uploadImageService = uploadImageService;
            _fileSettings = fileSettings.Value;
        }

        /// <summary>
        /// Add A New Inclusion 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Inclusion added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddInclusion")]
        public async Task<IActionResult> AddInclusion([FromBody] InclusionModel model)
        {
            var data = await _inclusionService.AddInclusion(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Update Inclusion 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Inclusion Updated successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateInclusion")]
        public async Task<IActionResult> UpdateInclusion([FromBody] InclusionModel model)
        {
            return Ok(await _inclusionService.UpdateInclusion(model));
        }

        /// <summary>
        /// Gets Inclusion By ID
        /// </summary>
        /// <param name="Id">an object holds the Id of Inclusion</param>
        /// <response code="200">Returns the Inclusion</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetInclusionByID")]
        public async Task<IActionResult> GetInclusionByID([FromQuery] long Id)
        {
            var response = await _inclusionService.GetInclusionByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated Inclusions
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Inclusions List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllInclusions")]
        public async Task<IActionResult> GetAllInclusions([FromQuery] RequestParameter filter)
        {
            var response = await _inclusionService.GetAllInclusionsWithPagination(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Not Paginated Inclusions
        /// </summary>
        /// <response code="200">Returns the Inclusions List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllInclusionsWithoutPagination")]
        public async Task<IActionResult> GetAllInclusionsWithoutPagination()
        {
            var response = await _inclusionService.GetAllInclusionsWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes Inclusion
        /// </summary>
        /// <param name="Id">an object holds the Id of Inclusion</param>
        /// <response code="200">Deletes Inclusion and all related data successfully</response>
        /// <response code="400">Inclusion not found, or there is error while saving</response>
        [HttpDelete("DeleteInclusion")]
        public async Task<IActionResult> DeleteInclusion([FromQuery] long Id)
        {
            var response = await _inclusionService.DeleteInclusion(Id);
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("UploadIcon")]
        public async Task<IActionResult> UploadIcon([FromBody] ImageUpload image)
        {
            var response = await _uploadImageService.Uploading(image, _fileSettings.Aminity_IconPath, "Aminity");
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
