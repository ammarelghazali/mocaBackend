using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.Interfaces.DynamicLists.Services;
using MOCA.Core.Interfaces.LocationManagment.Services;
using MOCA.Core.Interfaces.Shared.Services;
using MOCA.Core.Settings;




namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class DynamicListsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWorkSpaceCategoryService _WorkSpaceCategoryService;
        private readonly IWorkSpaceTypeService _WorkSpaceTypeService;
        private readonly IAmenityService _AmenityService;
        private readonly IVenueSetupService _VenueSetupService;
        private readonly IUploadImageService _UploadImageService;
        private readonly FileSettings _fileSettings;

        public DynamicListsController(IMapper mapper, IWorkSpaceCategoryService WorkSpaceCategoryService,
            IWorkSpaceTypeService WorkSpaceTypeService, IAmenityService amenityService, IUploadImageService uploadImageService, IVenueSetupService venueSetupService)
        {
            _mapper = mapper;
            _WorkSpaceCategoryService = WorkSpaceCategoryService;
            _WorkSpaceTypeService = WorkSpaceTypeService;
            _AmenityService = amenityService;
            _UploadImageService = uploadImageService;
            _VenueSetupService = venueSetupService;
        }
        ///////////////////WORK SPACE CATEGORY///////////////////WORK SPACE CATEGORY///////////////////WORK SPACE CATEGORY///////////////////


        [HttpPost("AddWorkSpaceCategory")]
        public async Task<IActionResult> AddWorkSpaceCategory([FromBody] WorkSpaceCategoryModel model)
        {
            var data = await _WorkSpaceCategoryService.AddWorkSpaceCategory(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("GetAllWorkSpaceCategoryWithoutPagination")]
        public async Task<IActionResult> GetAllWorkSpaceCategoryWithoutPagination()
        {
            var data = await _WorkSpaceCategoryService.GetAllWorkSpaceCategoryWithoutPagination();
            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }


        [HttpPut("UpdateWorkCategoryWorkSpace")]
        public async Task<IActionResult> UpdateWorkSpaceCategory([FromBody] WorkSpaceCategoryModel model)
        {
            var data = await _WorkSpaceCategoryService.UpdateWorkSpaceCategory(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);

        }

        [HttpDelete("DeleteWorkCategoryWorkSpace")]
        public async Task<IActionResult> DeleteWorkCategoryWorkSpace(long id)
        {
            var data = await _WorkSpaceCategoryService.DeleteWorkSpaceCategory(id);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }


        [HttpPost("AddListOfWorkSpaceCategory")]
        public async Task<IActionResult> AddListOfWorkSpaceCategory([FromBody] List<WorkSpaceCategoryModel> model)
        {
            var data = await _WorkSpaceCategoryService.AddListOfWorkSpaceCategory(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("GetWorkSpaceCategorybyID")]
        public async Task<IActionResult> GetWorkSpaceCategorybyID([FromHeader] long Id)
        {
            var response = await _WorkSpaceCategoryService.GetWorkSpaceCategoryByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        ///////////////////WORK SPACE TYPE///////////////////WORK SPACE TYPE///////////////////WORK SPACE TYPE///////////////////

        [HttpPost("AddWorkSpaceType")]
        public async Task<IActionResult> AddWorkSpaceType([FromBody] WorkSpaceTypeModel model)
        {
            var data = await _WorkSpaceTypeService.AddWorkSpaceType(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpPost("AddListOfWorkSpaceType")]
        public async Task<IActionResult> AddListOfWorkSpaceType([FromBody] List<WorkSpaceTypeModel> model)
        {
            var data = await _WorkSpaceTypeService.AddListOfWorkSpaceTypes(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpDelete("DeleteWorkTypeWorkSpace")]
        public async Task<IActionResult> DeleteWorkSpaceType(long id)
        {
            var data = await _WorkSpaceTypeService.DeleteWorkSpaceType(id);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("GetWorkSpaceTypeByID")]
        public async Task<IActionResult> GetWorkSpaceTypebyID([FromHeader] long Id)
        {
            var response = await _WorkSpaceTypeService.GetWorkSpaceTypeById(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllWorkSpaceTypeWithoutPagination")]
        public async Task<IActionResult> GetAllWorkSpaceTypeWithoutPagination()
        {
            var data = await _WorkSpaceTypeService.GetWorkSpaceTypesWithoutPagination();
            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpPut("UpdateWorkTypeWorkSpace")]
        public async Task<IActionResult> UpdateWorkSpaceType([FromBody] WorkSpaceTypeModel model)
        {
            var data = await _WorkSpaceTypeService.UpdateWorkSpaceType(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);

        }

        [HttpGet("GetAllWorkSpaceTypesPagination")]
        public async Task<IActionResult> GetAllWorkSpaceTypesPagination([FromQuery] RequestParameter filter)
        {
            var response = await _WorkSpaceTypeService.GetAllWorkSpaceTypePaginated(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
            }

            ///////////////////AMENITY///////////////////AMENITY///////////////////AMENITY///////////////////AMENITY//////////////////////////
            [HttpPost("AddAmenity")]
        public async Task<IActionResult> AddAmenity([FromBody] AmenityModel model)
        {
            var amenity = await _AmenityService.AddAmenity(model);
       
            if (amenity.Succeeded == false)
            {
                return BadRequest(amenity);
            }
            return Ok(amenity);
        }

        [HttpPost("UploadAmenityIcon")]
        public async Task<IActionResult> UploadAmenityIcon([FromBody] ImageUpload image)
        {
            var response = await _UploadImageService.Uploading(image, _fileSettings.Location_FilePath, "Amenity");
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("AddListOfAmenity")]
        public async Task<IActionResult> AddListOfAmenity([FromBody] List<AmenityModel> model)
        {
            var data = await _AmenityService.AddListOfAmenity(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpDelete("DeleteAmenity")]
        public async Task<IActionResult> DeleteWorkAmenity(long id)
        {
            var data = await _AmenityService.DeleteAmenity(id);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("GetAllAmenityPagination")]
        public async Task<IActionResult> GetAllAmenityPagination([FromQuery] RequestParameter filter)
        {
            var response = await _AmenityService.GetAllAmenityPaginated(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAmenityByID")]
        public async Task<IActionResult> GetAmenityByID([FromHeader] long Id)
        {
            var response = await _AmenityService.GetAmenityById(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllAmenityWithoutPagination")]
        public async Task<IActionResult> GetAllAmenityWithoutPagination()
        {
            var data = await _AmenityService.GetAmenityWithoutPagination();
            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpPut("UpdateAmenity")]
        public async Task<IActionResult> UpdateAmenity([FromBody] AmenityModel model)
        {
            var data = await _AmenityService.UpdateAmenity(model);
            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);

        }
        ///////////////////VENUE SETUP///////////////////VENUE SETUP///////////////////VENUE SETUP/////////////VENUE SETUP//////////////////VENUE SETUP///////////////////
        [HttpPost("AddListOfVenueSetup")]
        public async Task<IActionResult> AddListOfVenueSetup([FromBody] List<VenueSetupModel> model)
        {
            var data = await _VenueSetupService.AddListOfVenueSetup(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpPost("AddVenueSetup")]
        public async Task<IActionResult> AddVenueSetup([FromBody] VenueSetupModel model)
        {
            var setup = await _VenueSetupService.AddVenueSetup(model);

            if (setup.Succeeded == false)
            {
                return BadRequest(setup);
            }
            return Ok(setup);
        }

        [HttpPost("UploadVenueSetupIcon")]
        public async Task<IActionResult> UploadSetupVenueIcon([FromBody] ImageUpload image)
        {
            var response = await _UploadImageService.Uploading(image, _fileSettings.Location_FilePath, "Venue Setup");
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteVenueSetup")]
        public async Task<IActionResult> DeleteVenueSetup(long id)
        {
            var data = await _VenueSetupService.DeleteVenueSetup(id);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpGet("GetAllVenueSetupPagination")]
        public async Task<IActionResult> GetAllVenueSetupPagination([FromQuery] RequestParameter filter)
        {
            var response = await _VenueSetupService.GetAllVenueSetupPaginated(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetVenueSetupByID")]
        public async Task<IActionResult> GetVenueSetupByID([FromHeader] long Id)
        {
            var response = await _VenueSetupService.GetVenueSetupById(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllVenueSetupWithoutPagination")]
        public async Task<IActionResult> GetAllVenueSetupWithoutPagination()
        {
            var data = await _VenueSetupService.GetVenueSetupWithoutPagination();
            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        [HttpPut("UpdateVenueSetup")]
        public async Task<IActionResult> UpdateVenueSetup([FromBody] VenueSetupModel model)
        {
            var data = await _VenueSetupService.UpdateVenueSetup(model);
            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);

        }

        ///////////////////FURNITURE TYPE///////////////////FURNITURE TYPE///////////////////FURNITURE TYPE/////////////FURNITURE TYPE///////////FURNITURE TYPE//////////////
       


    }
}

