using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.DynamicLists;
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

        public DynamicListsController(IMapper mapper, IWorkSpaceCategoryService WorkSpaceCategoryService)
        {
            _mapper = mapper;
            _WorkSpaceCategoryService = WorkSpaceCategoryService;
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

    }
}

