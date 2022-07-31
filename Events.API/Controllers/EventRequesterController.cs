using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.Events.EventRequesterDtos.Request;
using MOCA.Core.Interfaces.Events.Services;

namespace Events.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    //[ApiExplorerSettings(GroupName = "Admin")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EventRequesterController : ControllerBase
    {
        private readonly IEventRequesterService _eventRequesterService;
        public EventRequesterController(IEventRequesterService eventRequesterService)
        {
            _eventRequesterService = eventRequesterService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllEventRequster([FromQuery] GetAllEventRequesterDto filter)
        {
            var respose = await _eventRequesterService.GetAllEventRequester(filter);
            return Ok(respose);
        }
    }


}
