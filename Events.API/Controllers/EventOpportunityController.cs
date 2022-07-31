using Microsoft.AspNetCore.Mvc;
using MOCA.Core;
using MOCA.Core.DTOs.Events.EventOpportunityDtos.Request;
using MOCA.Core.Interfaces.Events.Services;

namespace Events.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    //[Authorize]
    public class EventOpportunityController : ControllerBase
    {
        private readonly IEventOpportunityService _eventOpportunityService;
        private readonly IUnitOfWork _unitOfWork;

        public EventOpportunityController(IEventOpportunityService eventOpportunityService, IUnitOfWork unitOfWork)
        {
            _eventOpportunityService = eventOpportunityService;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates New Opportunity "Initiated by Sales"
        /// </summary>
        /// <param name="filter">an object that contains the contact details and event requester of the opportunity</param>
        /// <reponse code="200">Saves the opportunity successfully</reponse>
        /// <reponse code="400">Failed to save the opportuniy, maybe that happened if the contacts has an
        /// already opened opportunity or a tour</reponse>
        [HttpPost("CreateNewOpportunity")]
        public async Task<IActionResult> CreateNewOpportunity([FromBody] cmd_Create_NewEventOpportunity_Parameter filter)
        {
            var data = await _eventOpportunityService.CreateNewOpportunity(filter);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Deletes Event Opportunity
        /// </summary>
        /// <param name="filter">Id of the Opportunity</param>
        /// <response code="200">Deletes the opportunity and all related data successfully</response>
        /// <response code="400">Opportunity not found, or there is error while saving</response>
        [HttpDelete("DeleteOpportunity")]
        public async Task<IActionResult> DeleteOpportunity([FromQuery] cmd_Delete_EventOpportunity_Parameter filter)
        {
            var response = await _eventOpportunityService.DeleteOpportunity(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Updated Opportunity 
        /// </summary>
        /// <param name="filter">an object of the opportunity data</param>
        /// <response code="200">Updated Opportunity Successfully</response>
        /// <response code="400">Failed to update the opportunity or the id is not found</response>
        [HttpPut("UpdateOpportunity")]
        public async Task<IActionResult> UpdateOpportunity([FromBody] cmd_Update_EventOpportunity_Parameter filter)
        {
            var response = await _eventOpportunityService.UpdateOpportunity(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Get all the opportunities list filtered and with pagination, used when filtering the opportunities list
        /// </summary>
        /// <param name="filter">filters parameters</param>
        /// <response code="200">Returns the oppportunities successfully</response>
        [HttpPost("FilterWithPagination")]
        public async Task<IActionResult> FilterWithPagination([FromBody] cmd_Filter_EventOpportunityDetails_WithPagination_Parameter filter)
        {

            var response = await _eventOpportunityService.FilterWithPagination(new cmd_Filter_EventOpportunityDetails_WithPagination_Query((int)filter.pageNumber, (int)filter.pageSize)
            {
                pageNumber = (int)filter.pageNumber,
                pageSize = (int)filter.pageSize,
                LocationType_ID = (int)filter.LocationType_ID,
                OwnerName = filter.OwnerName,
                Name = filter.Name,
                Id = filter.Id,
                Requester = filter.Requester,
                ToSubmissionDate = filter.ToSubmissionDate,
                FromSubmissionDate = filter.FromSubmissionDate,
                Initiated = filter.Initiated,
            });

            return Ok(response);
        }

        /// <summary>
        /// Gets the full opportunity details with the stage reports and data of the user submission if found
        /// </summary>
        /// <param name="filter">Id of the opportunity</param>
        /// <response code="200">Returns the opportunity details successfully</response>
        /// <response code="400">Not found opportunity</response>
        [HttpGet("GetOpportunityDetailsByEventOpportunityID")]
        public async Task<IActionResult> GetOpportunityDetailsByEventOpportunityID([FromQuery] cmd_Get_EventOpportunityDetails_Parameter filter)
        {
            var response = await _eventOpportunityService.GetOpportunityDetailsByEventOpportunityID(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }



        [HttpGet("GetOpportunityDetailsWithoutPagination")]
        public async Task<IActionResult> GetOpportunityDetailsWithoutPagination([FromQuery] int locationType) // it must be long not int
        {
            var response = await _eventOpportunityService.GetOpportunityDetailsWithoutPagination(locationType);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }



        /// <summary>
        /// Send Emails to the Contacts of a certain opportunity to make them complete their submission
        /// </summary>
        /// <param name="filter">an object that contains the data needed to send the email</param>
        /// <response code="200">Email Sent Successfully</response>
        /// <response code="400">failed to sent the Email Sent Successfully</response>
        [HttpPost("SendEmails")]
        public async Task<IActionResult> SendEmails([FromBody] cmd_Post_SendEmail_Parameter filter)
        {
            var response = await _eventOpportunityService.SendEmails(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        /// <summary>
        /// Get Event Opportunity Full Details, Used When Clicking on the Id of the Opportunity
        /// </summary>
        /// <param name="filter">the opportunity Id</param>
        /// <response code="400">if the opportunity is not found or deleted the id is wrong</response>
        /// <response code="200">Returns the Opportunity Details</response>
        [HttpGet("GetEventOpportunityDetails")]
        public async Task<IActionResult> GetEventOpportunityDetails([FromQuery] cmd_Get_DetailedEventOpportunity_Parameter filter)
        {
            var response = await _eventOpportunityService.GetEventOpportunityDetails(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Save Opportunity Details
        /// </summary>
        /// <param name="filter"></param>
        /// <response code="200"> Saved Successfully and report stage added Opp stage repoert </response>
        /// <response code="400"> Error happend </response>
        [HttpPost("SaveEventOpportunityDetails")]
        public async Task<IActionResult> SaveEventOpportunityDetails([FromBody] cmd_Post_EventOpportunityStageReport_Parameter filter)
        {
            var response = await _eventOpportunityService.SaveEventOpportunityDetails(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
