using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.DTOs.WorkSpaceReservation.CRM.Request;
using MOCA.Core.Interfaces.WorkSpaceReservations.CoworkSpace.Services;

namespace WorkSpaceReservations.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CoworkSpaceCRMController : ControllerBase
    {
        private readonly ICoworkSpaceReservationServiceCRM _reservationServiceCRM;

        public CoworkSpaceCRMController(ICoworkSpaceReservationServiceCRM reservationServiceCRM)
        {
            _reservationServiceCRM = reservationServiceCRM;
        }

        /// <summary>
        /// Get All Work Space Submissions List
        /// </summary>
        /// <param name="request">an object that has pageNumber, and pageSize</param>
        /// <response code="200">returns the List Successfully</response>
        /// <response code="400">Failed to get the List due to Wrong Input</response>
        [HttpGet("GetAllCoworkSpaceSubmissions")]
        public async Task<IActionResult> GetAllCoworkSpaceSubmissions([FromQuery] RequestParameter request)
        {
            var response = await _reservationServiceCRM
                              .GetAllWorkSpaceSubmissionsSP(request);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get All Locations Used in CoworkSubmissions
        /// </summary>
        /// <response code="200">Returns All Locations</response>
        [HttpGet("GetAllLocationDropDowns")]
        public async Task<IActionResult> GetAllLocationDropDowns()
        {
            var response = await _reservationServiceCRM.GetWorkSpaceLocationsDropDowns();

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get CoworkSpaceReservation Info
        /// </summary>
        /// <param name="request">an Object has the WorkSpaceReservationId and the ReservationTypeId, if it is 1 
        /// then it's Hourly, 2 it's Tailored, or 3 it's Bundle</param>
        /// <response code="200">Returns the Info Successfully</response>
        /// <response code="400">if the Id is Wrong, or the request not formatted well</response>
        [HttpGet("GetCoworkSpaceOpportunityInfo")]
        public async Task<IActionResult> GetCoworkSpaceOpportunityInfo([FromQuery] GetWorkSpaceReservationHistoryDto request)
        {
            var response = await _reservationServiceCRM.GetWorkSpaceOpportunityInfoHistory(request);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
