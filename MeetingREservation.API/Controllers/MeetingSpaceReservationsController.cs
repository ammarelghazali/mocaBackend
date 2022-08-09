using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MeetingReservations.Request;
using MOCA.Core.Interfaces.MeetingSpaceReservations.Services;

namespace MeetingREservation.API.Controllers
{
    //[ApiVersion("1.0")]
    //[ApiExplorerSettings(GroupName = "Admin")]
    //[Authorize]
    [ApiController]
    public class MeetingSpaceReservationsController : Controller
    {   
        private readonly IMeetingSpaceReservationsServices _meetingSpaceReservationsServices;
        public MeetingSpaceReservationsController(IMeetingSpaceReservationsServices meetingSpaceReservationsServices)
        {
            _meetingSpaceReservationsServices = meetingSpaceReservationsServices;
        }

        [HttpGet("GetAllSubmissionsWithPagination")]
        public async Task<IActionResult> GetAllSubmissionsWithPagination(GetAllMeetingsSubmissionsDto dto)
        {
            var response = await _meetingSpaceReservationsServices.GetAllSubmissionsWithPagination(dto.PageNumber, dto.PageSize);
            return Ok(response);
        }


        [HttpGet("GetAllSubmissionsWithoutPagination")]
        public async Task<IActionResult> GetAllSubmissionsWithoutPagination()
        {
            var response = await _meetingSpaceReservationsServices.GetAllSubmissionsWithoutPagination();
            return Ok(response);
        }

        [HttpGet("GetAllLocations")]
        public async Task<IActionResult> GetAllLocations()
        {
            var response = await _meetingSpaceReservationsServices.GetAllMeetingReservationLocations();
            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetMeetingReservationById(long id)
        {
            var response = await _meetingSpaceReservationsServices.GetMeetingReservationById(id);
           return Ok(response);
        }

        [HttpGet("GetAllWithFilter")]
        public async Task<IActionResult> GetAllWithFilter(GetAllMeetingReservationsWithFilterRequestDto dto)
        {
            var response = await _meetingSpaceReservationsServices.GetAllMeetingReservationsWithFilter(dto);
            return Ok(response);
        }

    }
}
