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

        /// <summary>
        /// Get All Stautuses
        /// </summary>
        /// <response code="200">Returns all the Statuses</response>
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

        /// <summary>
        /// Get Single Status By Id
        /// </summary>
        /// <param name="statusId">Status Id</param>
        /// <response code="200">Returns the Status Successfully</response>
        /// <response code="400">Request is not well formatted, or the id is wrong</response>
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

        /// <summary>
        /// Add new Status
        /// </summary>
        /// <param name="statusForCreationDto">an object that has the name of the status</param>
        /// <response code="200">Added the Status Successfully</response>
        /// <response code="400">Request is not well formatted</response>
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


        /// <summary>
        /// Update Status
        /// </summary>
        /// <param name="statusId">Status Id</param>
        /// <param name="statusForCreationDto">an object that has the new name of the status</param>
        /// <response code="200">Updates the Status Successfully</response>
        /// <response code="400">Request is not well formatted, or the id is wrong</response>
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

        /// <summary>
        /// Delete Status
        /// </summary>
        /// <param name="statusId">Status Id</param>
        /// <response code="200">Deletes the Status Successfully</response>
        /// <response code="400">Request is not well formatted, or the id is wrong</response>
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
