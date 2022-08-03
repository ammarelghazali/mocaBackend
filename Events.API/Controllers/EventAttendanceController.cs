using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.Events.EventAttendanceDtos.Request;
using MOCA.Core.Interfaces.Events.Services;

namespace Events.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventAttendanceController : ControllerBase
    {
        private readonly IEventAttendanceService _eventAttendanceService;

        public EventAttendanceController(IEventAttendanceService eventAttendanceService)
        {
            _eventAttendanceService = eventAttendanceService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllEventAttendanceParameter filter)
        {
            var response = await _eventAttendanceService.GetAll(new GetAllEventAttendanceDto { pageNumber = filter.pageNumber, pageSize = filter.pageSize });
            return Ok(response);
        }

    }
}
