using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOCA.Core.DTOs.LocationManagment.FavouriteLocation;
using MOCA.Core.DTOs.LocationManagment.Location;
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
    public class LocationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILocationService _locationService;
        private readonly IFavouriteLocationService _favouriteLocationService;
        private readonly FileSettings _fileSettings;
        private readonly IUploadImageService _uploadImageService;
        public LocationController(IMapper mapper, ILocationService locationService, IFavouriteLocationService favouriteLocationService, IOptions<FileSettings> fileSettings, IUploadImageService uploadImageService)
        {
            _mapper = mapper;
            _locationService = locationService;
            _favouriteLocationService = favouriteLocationService;
            _fileSettings = fileSettings.Value;
            _uploadImageService = uploadImageService;
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
            var data = await _locationService.UpdateLocation(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
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
        public async Task<IActionResult> UploadLocationImages([FromBody] ImageUpload image)
        {
            var response = await _uploadImageService.Uploading(image, _fileSettings.Location_FilePath, "Location");
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
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
        public async Task<IActionResult> GetAllLocationsPublishAndUnpublish([FromQuery] RequestParameter filter)
        {
            var response = await _locationService.GetAllPublishedAndUnpublishedLocation(filter);

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
        [HttpGet("GetAllLocationsPublishAndUnpublishWithoutPagination")]
        public async Task<IActionResult> GetAllLocationsPublishAndUnpublishWithoutPagination()
        {
            var response = await _locationService.GetAllPublishedAndUnpublishedLocationWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets All Locations With Filter
        /// </summary>
        /// <param name="filter">an object holds the Filter Body of Location</param>
        /// <response code="200">Returns the Locations List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("GetAllLocationsPublishAndUnpublishFilter")]
        public async Task<IActionResult> GetAllLocationsPublishAndUnpublishFilter([FromBody] RequestGetAllLocationParameter filter)
        {
            var response = await _locationService.GetAllPublishedAndUnpublishedLocationFilter(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets All Locations With Filter WithoutPagination
        /// </summary>
        /// <param name="filter">an object holds the Filter Body of Location</param>
        /// <response code="200">Returns the Locations List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("GetAllLocationsPublishAndUnpublishFilterWithoutPagination")]
        public async Task<IActionResult> GetAllLocationsPublishAndUnpublishFilterWithoutPagination([FromBody] RequestGetAllLocationWithoutPaginationParameter filter)
        {
            var response = await _locationService.GetAllPublishedAndUnpublishedLocationFilterWithoutPagination(filter);

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

        /// <summary>
        /// Add A New Favourite Location 
        /// </summary>
        /// <param name="model">an object holds the LocationId, BasicUserId of Favourite Location</param>
        /// <response code="200">Favourite Location added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddFavouriteLocation")]
        public async Task<IActionResult> AddFavouriteLocation([FromBody] FavouriteLocationModel model)
        {
            var data = await _favouriteLocationService.AddFavouriteLocation(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Delete Favourite Location 
        /// </summary>
        /// <response code="200">Favourite Location Delete successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("DeleteFavouriteLocation")]
        public async Task<IActionResult> DeleteFavouriteLocation([FromBody] long LocationId, long BasicUserID)
        {
            var data = await _favouriteLocationService.DeleteFavouriteLocation(LocationId, BasicUserID);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }
    }
}
