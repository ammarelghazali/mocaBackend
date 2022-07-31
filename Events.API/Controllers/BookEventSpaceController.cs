using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.Events.BookEventSpaceDtos.Request;
using MOCA.Core.Interfaces.Events.Services;

namespace Events.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "Admin")]
    [ApiVersion("1.0")]
    public class BookEventSpaceController : ControllerBase
    {
        private readonly IBookEventSpaceService _eventSpaceService;
        public BookEventSpaceController(IBookEventSpaceService eventSpaceService)
        {
            _eventSpaceService = eventSpaceService;
        }



        /// <summary>
        /// Gets Paginated Opportunities List by Location Type
        /// </summary>
        /// <param name="filter">an object holds the filter data, TypeLocation if it was 2 then it's coopolitan or 3 then it's 
        /// mocca, pageNumber and pageSize is used for pagination, LocationName(not used), SortBy(not used), and SortDirection(not used)</param>
        /// <response code="200">Returns the Opportunities List</response>
        [HttpGet("GetAllByType")]
        public async Task<IActionResult> GetAllOpportunitiesByTypeAsync([FromQuery] GetAllBookedEventSpacesByTypeRequestDto filter)
        {
            var response = await _eventSpaceService.GetAllBookedEventSpaceTypeAsync(
            new GetAllBookedEventSpaceByType_Query(filter.pageNumber, filter.pageSize)
            {
                LocationName = filter.LocationName,
                SortBy = filter.SortBy,
                SortDirection = filter.SortDirection,
                TypeLocation = filter.TypeLocation
            });

            return Ok(response);
        }

        /// <summary>
        /// Get All Opportunities List Dropdown Used in Filter
        /// </summary>
        /// <param name="filter">an object that hold LocTypeId, if it was 2 then it's coopolitan or 3 then it's mocca</param>
        /// <response code="200">Returns the Dropdowns</response>
        [HttpGet("GetAllDataForDropDowns")]
        public async Task<IActionResult> GetAllDataForDropDowns([FromQuery] GetAllBookedEventSpaceDropDownsDto dto)
        {
            var response = await _eventSpaceService.GetAllDataForDropDowns(dto);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// book a new event 
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Event added successfully and email is Sent</response>
        /// <response code="400">Event added before statment means something goes wrong in backend</response>
        [HttpPost("EventSpaceBooking")]
        public async Task<IActionResult> EventSpaceBooking(BooEventSpaceDto command)
        {
            var response = await _eventSpaceService.EventSpaceBooking(command);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
