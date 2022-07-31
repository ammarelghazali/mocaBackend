using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.Events.EventsOpportunitiesDtos.Request;
using MOCA.Core.Interfaces.Events.Services;

namespace Events.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    //[ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EventsOpportunitiesController : ControllerBase
    {
        private readonly IEventsOpportunitiesService _eventsOpportunitiesService;
        public EventsOpportunitiesController(IEventsOpportunitiesService eventsOpportunitiesService)
        {
            _eventsOpportunitiesService = eventsOpportunitiesService;
        }

        /// <summary>
        /// Get the Email Templater of a Specific EmailTemplateId 
        /// </summary>
        /// <param name="filter">Email Template Id</param>
        /// <response code="200">Returns the Email Template Successfully, or the Email Template not found or not created
        /// before</response>
        [HttpGet("GetEmail")]
        public async Task<IActionResult> GetEmail([FromQuery] GetEmailTempleteEventOpportunityDto filter)
        {
            return Ok(await _eventsOpportunitiesService.GetEmailTempletType(filter));
        }

    }
}
