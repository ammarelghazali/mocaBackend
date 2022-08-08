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

    }
}
