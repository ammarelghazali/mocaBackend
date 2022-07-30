using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.StatusDto.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Issues/[controller]")]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusesService _statusesService;

        public StatusesController(IStatusesService statusesService)
        {
            _statusesService = statusesService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllStatuses()
        {
            var response = await _statusesService.GetAllStatusesAsync();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpGet("{statusId}")]
        public async Task<IActionResult> GetSingleStatus([FromRoute] long statusId)
        {
            var response = await _statusesService.GetSingleStatusAsync(statusId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddStatus([FromBody] StatusForCreationDto statusForCreationDto)
        {
            var response = await _statusesService.AddStatusyAsync(statusForCreationDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{statusId}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] long statusId,
                                                     [FromBody] StatusForCreationDto statusForCreationDto)
        {
            var response = await _statusesService.UpdateStatusAsync(statusId, statusForCreationDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{statusId}")]
        public async Task<IActionResult> DeleteStatus([FromRoute] long statusId)
        {
            var response = await _statusesService.DeleteStatusyAsync(statusId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
