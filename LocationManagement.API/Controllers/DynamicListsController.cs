using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs;
using MOCA.Core.DTOs.DynamicLists;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.DynamicLists;
using MOCA.Core.Interfaces.DynamicLists.Services;
using MOCA.Core.Interfaces.LocationManagment.Services;

namespace LocationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class DynamicListsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWorkSpaceCategoryService _WorkSpaceCategoryService;
        private readonly IWorkSpaceTypeService _WorkSpaceTypeService;
        public DynamicListsController(IMapper mapper, IWorkSpaceCategoryService WorkSpaceCategoryService, IWorkSpaceTypeService WorkSpaceTypeService)

        public DynamicListsController(IMapper mapper, IWorkSpaceCategoryService WorkSpaceCategoryService)
        {
            _mapper = mapper;
            _WorkSpaceCategoryService = WorkSpaceCategoryService;
            _WorkSpaceTypeService = WorkSpaceTypeService;
        }


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

        ////////////////////////////////////////////////////////////////////////////////////////////////

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

    }
}

