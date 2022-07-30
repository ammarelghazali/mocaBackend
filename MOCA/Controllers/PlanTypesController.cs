using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.PlanTypesDto.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlanTypesController : ControllerBase
    {
        private readonly IPlanTypesService _planTypesService;

        public PlanTypesController(IPlanTypesService planTypesService)
        {
            _planTypesService = planTypesService;
        }


        /// <summary>
        /// Get All plan types
        /// </summary>
        /// <response code="200">Gets all plan types successfully</response>
        /// <response code="400">No plan types found for this space</response>
        /// 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _planTypesService.GetAll();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Adds new plan type
        /// </summary>
        /// <param name="planTypeDto"></param>
        /// <response code="200">Adds plan type successfully</response>
        /// <response code="500">Server Error</response>
        /// 
        [HttpPost]
        public async Task<IActionResult> Add(PlanTypeForCreationDto planTypeDto)
        {
            var response = await _planTypesService.Add(planTypeDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        /// <summary>
        /// updates planType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="planTypeDto"></param>
        /// <response code="200">updated successfully</response>
        /// <response code="400">id not found or name is repeated</response>
        /// <response code="500">server error</response>
        /// 
        [HttpPut]
        public async Task<IActionResult> Update(long id, PlanTypeForCreationDto planTypeDto)
        {
            var response = await _planTypesService.Update(id, planTypeDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Deletes plan type by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Adds plan type successfully</response>
        /// <response code="500">Server Error</response>

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _planTypesService.Delete(id);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
