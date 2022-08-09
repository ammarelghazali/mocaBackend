using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOCA.Core.DTOs.LocationManagment.Inclusion;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Settings;

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
        private readonly FileSettings _fileSettings;
        public InclusionController(IMapper mapper, IInclusionService inclusionService, IOptions<FileSettings> fileSettings)
        {
            _mapper = mapper;
            _inclusionService = inclusionService;
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
        [Authorize]
        public async Task<IActionResult> UploadIcon()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                List<string> lstFileNames = new List<string>();
                var folderName = fileSettings.Inclusion_IconPath;
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                Directory.CreateDirectory(pathToSave);
                foreach (var file in formCollection.Files)
                {
                    if (file.Length > 0)
                    {
                        if (((file.Length / 1024) / 1024) <= (fileSettings.MaxSizeInMega * 1024))
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var fileNameWithoutExtention = Path.GetFileNameWithoutExtension(fileName);
                            var extension = Path.GetExtension(fileName);
                            Random rnd = new Random();
                            string randomNumber = (rnd.Next(1000, 9999)).ToString();
                            string renameFile = fileNameWithoutExtention + "_" + randomNumber + extension;

                            var fullPath = Path.Combine(pathToSave, renameFile);
                            var dbPath = Path.Combine(folderName, renameFile);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            return Ok(new { dbPath });

                            //lstFileNames.Add(dbPath);
                        }

                    }

                }
                return Ok(lstFileNames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
