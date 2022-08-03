using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.Events.EventTypeDtos.Requset;
using MOCA.Core.Interfaces.Events.Services;

namespace Events.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    //[ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EventTypeController : ControllerBase
    {
        private readonly IEventTypeService _eventTypeService;
        public EventTypeController(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllEventTypeDto filter)
        {
            var response = await _eventTypeService.GetAllEventTypes(filter);
            return Ok(response);
        }

    }
}
