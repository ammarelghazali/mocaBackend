using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.Events.EventReccuranceDtos.Request;
using MOCA.Core.Interfaces.Events.Services;

namespace Events.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "Admin")]
    [ApiVersion("1.0")]
    public class EventReccuranceController : ControllerBase
    {
        private readonly IEventRecurrenceService _eventRecurrenceService;

        public EventReccuranceController(IEventRecurrenceService eventRecurrenceService)
        {
            _eventRecurrenceService = eventRecurrenceService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllEventReccuranceDto filter)
        {
            var response = await _eventRecurrenceService.GetAll(
                               new GetAllEventReccuranceQuery(filter.pageNumber, filter.pageSize)
                               {
                                   pageSize = filter.pageSize,
                                   pageNumber = filter.pageNumber
                               });

            return Ok(response);
        }
    }
}
