using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.SeverityDtos.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Issues/[controller]")]
    public class SeveritiesController : ControllerBase
    {
        private readonly ISeveritiesService _severitiesService;

        public SeveritiesController(ISeveritiesService severitiesService)
        {
            _severitiesService = severitiesService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllSeverities()
        {
            var response = await _severitiesService.GetAllSeverityAsync();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpGet("{severityId}")]
        public async Task<IActionResult> GetSingleSeverity([FromRoute] long severityId)
        {
            var response = await _severitiesService.GetSingleSeverityAsync(severityId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }



        [HttpPost]
        public async Task<IActionResult> AddSeverity([FromBody] SeverityForCreationDto severityForCreationDto)
        {
            var response = await _severitiesService.AddSeverityAsync(severityForCreationDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }



        [HttpPut("{severityId}")]
        public async Task<IActionResult> UpdateSeverity([FromRoute] long severityId,
                                                        [FromBody] SeverityForCreationDto statusForCreationDto)
        {
            var response = await _severitiesService.UpdateSeverityAsync(severityId, statusForCreationDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpDelete("{severityId}")]
        public async Task<IActionResult> DeleteSeverity([FromRoute] long severityId)
        {
            var response = await _severitiesService.DeleteSeverityAsync(severityId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
