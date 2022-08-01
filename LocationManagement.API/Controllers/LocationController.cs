using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOCA.Core.DTOs.LocationManagment.Location;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Settings;
using System.Net.Http.Headers;

namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LocationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;
        private readonly FileSettings _fileSettings;
        public LocationController(IMapper mapper, ILocationService locationService, IOptions<FileSettings> fileSettings)
        {
            _mapper = mapper;
            _locationService = locationService;
            _fileSettings = fileSettings.Value;
        }

        /// <summary>
        /// Add A New Location 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Location added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddLocation")]
        public async Task<IActionResult> AddLocation([FromBody] LocationModel model)
        {
            var data = await _locationService.AddLocation(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Update Location 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Location Updated successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateLocation")]
        public async Task<IActionResult> UpdateLocation([FromBody] LocationModel model)
        {
            return Ok(await _locationService.UpdateLocation(model));
        }

        /// <summary>
        /// Deletes Location
        /// </summary>
        /// <param name="Id">an object holds the Id of Location</param>
        /// <response code="200">Deletes Location and all related data successfully</response>
        /// <response code="400">Location not found, or there is error while saving</response>
        [HttpDelete("DeleteLocation")]
        public async Task<IActionResult> DeleteLocation([FromQuery] long Id)
        {
            var response = await _locationService.DeleteLocation(Id);
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Upload Location File
        /// </summary>
        /// <response code="200">Location File Uploaded</response>
        /// <response code="400">Location File Not Uploaded, or there is error while saving</response>
        [HttpPost("UploadLocationFile")]
        public async Task<IActionResult> UploadLocationFile()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                List<string> lstFileNames = new List<string>();
                var folderName = _fileSettings.Location_FilePath;
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                foreach (var file in formCollection.Files)
                {
                    if (file.Length > 0)
                    {
                        if (((file.Length / 1024) / 1024) <= (_fileSettings.MaxSizeInMega * 1024))
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
                            
                            lstFileNames.Add(dbPath);
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

        /// <summary>
        /// Upload Location Images
        /// </summary>
        /// <response code="200">Location Images Uploaded</response>
        /// <response code="400">Location Images Not Uploaded, or there is error while saving</response>
        [HttpPost("UploadLocationImages")]
        public async Task<IActionResult> UploadLocationImages([FromQuery] string AlbumName)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();

                var folderName = _fileSettings.Location_FilePath + "//" + AlbumName.ToLower();
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                List<string> lstFileNames = new List<string>();
                foreach (var file in formCollection.Files)
                {
                    if (file.Length > 0)
                    {
                        if (((file.Length / 1024) / 1024) <= (_fileSettings.MaxSizeInMega * 1024))
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
                            lstFileNames.Add(dbPath);
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

        /// <summary>
        /// Get Location Drop Down For Filter(District, City, Locations)
        /// </summary>
        /// <response code="200">Location DropDown</response>
        /// <response code="400">there is error while request server</response>
        [HttpGet("GetAllForDropDown")]
        public async Task<IActionResult> GetAllForDropDown()
        {
            var response = await _locationService.GetAllForDropDown();
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Location By ID
        /// </summary>
        /// <param name="Id">an object holds the Id of Location</param>
        /// <response code="200">Returns the Location</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetLocationByID")]
        public async Task<IActionResult> GetLocationByID([FromQuery] long Id)
        {
            var response = await _locationService.GetLocationByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated Locations
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Locations List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllLocations")]
        public async Task<IActionResult> GetAllLocations([FromQuery] RequestParameter filter)
        {
            var response = await _locationService.GetAllLocationWithPagination(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Not Paginated Locations
        /// </summary>
        /// <response code="200">Returns the Locations List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllLocationsWithoutPagination")]
        public async Task<IActionResult> GetAllLocationsWithoutPagination()
        {
            var response = await _locationService.GetAllLocationWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Update Location Publish Status
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Location Published successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateLocationPublishStatus")]
        public async Task<IActionResult> UpdateLocationPublishStatus([FromBody] long LocationId)
        {
            var response = await _locationService.UpdateLocationPublishStatus(LocationId);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets All Locations
        /// </summary>
        /// <response code="200">Returns the Locations List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllLocationsPublishAndUnpublish")]
        public async Task<IActionResult> GetAllLocationsPublishAndUnpublish()
        {
            var response = await _locationService.GetAllPublishedAndUnpublishedLocation();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets All Unpublish Locations
        /// </summary>
        /// <response code="200">Returns the Locations List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllLocationsUnpublish")]
        public async Task<IActionResult> GetAllLocationsUnpublish()
        {
            var response = await _locationService.GetAllUnpublishedLocation();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
