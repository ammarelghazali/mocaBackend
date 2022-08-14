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
        [HttpGet]
        public async Task<IActionResult> GetAllCoworkSpaceSubmissions([FromQuery] RequestParameter request)
        {
            var response = await _reservationServiceCRM
                              .GetAllWorkSpaceSubmissionsSP(new GetAllWorkSpaceReservationsDto(request.PageNumber, request.PageSize));

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


    }
}
