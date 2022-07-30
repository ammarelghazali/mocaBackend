using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.PriorityDtos.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [Route("api/v{version:apiVersion}/Issues/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private readonly IPriorityService _priorityService;

        public PrioritiesController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        /// <summary>
        /// Get All Priorities
        /// </summary>
        /// <response code="200">Gets all the priorities successfully</response>
        [HttpGet]
        public async Task<IActionResult> GetAllPriorities()
        {
            var response = await _priorityService.GetAllPrioritiesAsync();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get priority by id
        /// </summary>
        /// <param name="priorityId">Id of the priority</param>
        /// <response code="200">Gets the case type successfully</response>
        /// <response code="400">Request is not well formatted or the id is wrong</response>
        [HttpGet("{PriorityId}")]
        public async Task<IActionResult> GetSinglePriority([FromRoute] long priorityId)
        {
            var response = await _priorityService.GetSinglePriorityAsync(priorityId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Adds a priority
        /// </summary>
        /// <param name="priorityForCreation">an object that holds the priority name</param>
        /// <response code="200">Adds the case type successfully</response>
        /// <response code="400">Request is not well formatted</response>
        /// <response code="500">Serve failed to add the priority</response>
        [HttpPost]
        public async Task<IActionResult> AddPriority([FromBody] PriorityForCreationDto priorityForCreation)
        {
            var response = await _priorityService.AddPriorityAsync(priorityForCreation);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates a priority
        /// </summary>
        /// <param name="priorityId">Id of the priority</param>
        /// <param name="priorityForCreation">an object that holds the priority name</param>
        /// <response code="200">Updates the case type successfully</response>
        /// <response code="400">Request is not well formatted or the id is wrong</response>
        /// <response code="500">Serve failed to update the priority</response>
        [HttpPut("{PriorityId}")]
        public async Task<IActionResult> UpdatePriority([FromRoute] long priorityId,
                                                  [FromBody] PriorityForCreationDto priorityForCreation)
        {
            var response = await _priorityService.UpdatePriorityAsync(priorityId, priorityForCreation);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes a priority
        /// </summary>
        /// <param name="priorityId">Id of the priority</param>
        /// <response code="204">Deletes the case type successfully</response>
        /// <response code="400">Request is not well formatted or the id is wrong</response>
        /// <response code="500">Serve failed to delete the priority</response>
        [HttpDelete("{PriorityId}")]
        public async Task<IActionResult> DeletePriority([FromRoute] long priorityId)
        {
            var response = await _priorityService.DeletePriorityAsync(priorityId);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
