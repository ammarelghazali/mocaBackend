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
    public class MeetingSpaceReservationsController : ControllerBase
    {   
        private readonly IMeetingSpaceReservationsServices _meetingSpaceReservationsServices;
        public MeetingSpaceReservationsController(IMeetingSpaceReservationsServices meetingSpaceReservationsServices)
        {
            _meetingSpaceReservationsServices = meetingSpaceReservationsServices;
        }

        #region CRM

        /// <summary>
        /// Gets all meeting reservations with pagination sent in DTO
        /// </summary>
        /// <param name="dto"></param>
        /// <response code="200"> returns all paged meeting reservations with details or null if not found data</response>

        [HttpGet("GetAllSubmissionsWithPagination")]
        public async Task<IActionResult> GetAllSubmissionsWithPagination([FromQuery] GetAllMeetingsSubmissionsDto dto)
        {
            var response = await _meetingSpaceReservationsServices.GetAllSubmissionsWithPagination(dto.PageNumber, dto.PageSize);
            return Ok(response);
        }

        /// <summary>
        /// Gets all meeting reservations without pagination to be in excel sheet
        /// </summary>
        /// <response code="200"> returns all meeting reservations with details or null if not found data</response>
        
        [HttpGet("GetAllSubmissionsWithoutPagination")]
        public async Task<IActionResult> GetAllSubmissionsWithoutPagination()
        {
            var response = await _meetingSpaceReservationsServices.GetAllSubmissionsWithoutPagination();
            return Ok(response);
        }


        /// <summary>
        /// Gets a meeting reservations info with Id
        /// </summary>
        /// <response code="200"> returns a meeting reservations info details or null if not found data</response>

        [HttpGet("GetById")]
        public async Task<IActionResult> GetMeetingReservationById(long id)
        {
            var response = await _meetingSpaceReservationsServices.GetMeetingReservationById(id);
           return Ok(response);
        }


        /// <summary>
        /// Gets all meeting reservations with filter sent in DTO
        /// </summary>
        /// <param name="dto"></param>
        /// <response code="200"> returns all filterd meeting reservations with details or null if not found data</response>

        [HttpGet("GetAllWithFilter")]
        public async Task<IActionResult> GetAllWithFilter([FromQuery] GetAllMeetingReservationsWithFilterRequestDto dto)
        {
            var response = await _meetingSpaceReservationsServices.GetAllMeetingReservationsWithFilter(dto);
            return Ok(response);
        }


        /// <summary>
        /// Gets all locations that have meeting reservations for dropdown list
        /// </summary>
        /// <response code="200"> returns all distinct location (Id, Name) or null if not found data</response>
        
        [HttpGet("GetAllLocations")]
        public async Task<IActionResult> GetAllLocations()
        {
            var response = await _meetingSpaceReservationsServices.GetAllMeetingReservationLocations();
            return Ok(response);
        }

        #endregion

        #region Mobile

        [HttpPost("BookMeetingReservation")]
        public async Task<IActionResult> BookMeetingReservation(BookMeetingReservationRequestDto dto)
        {
            var response = await _meetingSpaceReservationsServices.BookMeetingReservation(dto);
            if(!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        [HttpPost("AddAttendees")]
        public async Task<IActionResult> AddAttendees(List<MeetingAttendeeDto> dto)
        {
            var response = await _meetingSpaceReservationsServices.AddAttendees(dto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        [HttpPut("UpdatePaymentMethod")]
        public async Task<IActionResult> UpdatePaymentMethod(long meetingReservationId, long paymentMethodId)
        {
            var response = await _meetingSpaceReservationsServices.UpdatePaymentMethod(meetingReservationId, paymentMethodId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        #endregion

    }
}
