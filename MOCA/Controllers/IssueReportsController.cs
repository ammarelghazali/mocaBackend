using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.IssueReportDtos.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [Route("api/v{version:apiVersion}/Issues/Reports")]
    [ApiVersion("1.0")]
    [ApiController]
    public class IssueReportsController : ControllerBase
    {
        private readonly IIssueReportService _issueReportService;

        public IssueReportsController(IIssueReportService issueReportService)
        {
            _issueReportService = issueReportService;
        }

        //[HttpPost("GetInfoForNewIssue")]
        //public async Task<IActionResult> GetInfoForNewIssueReport([FromBody] NewIssueInfoDto newIssue)
        //{
        //    var response = await _issueReportService.GetInfoForNewIssueReportAsync(newIssue.);
        //    if (!response.Succeeded)
        //    {
        //        return BadRequest(response);
        //    }

        //    return Ok(response);
        //}


        [HttpGet("single/{issueReportId}")]
        public async Task<IActionResult> GetSingleIssueReport([FromRoute] long issueReportId)
        {
            var response = await _issueReportService.GetSingleIssueReportAsync(issueReportId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Add Issue Report
        /// </summary>
        /// <param name="issueReportForCreationDto">an object that contains the data required to add the issue report</param>
        /// <response code="200">Adds the issue report successfully</response>
        /// <response code="400">If the request is nor well formatted or the id is wrong</response>
        /// <response code="500">Server failed to add the issue report</response>
        /// 
        [HttpPost]
        public async Task<IActionResult> AddIssueReport([FromBody] IssueReportForCreationDto issueReportForCreationDto)
        {
            var response = await _issueReportService.AddIssueReportAsync(issueReportForCreationDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get All Issue Reports (Paginated&Filtered)
        /// </summary>
        /// <param name="lobSpaceTypeId">Id of the Lob Space Type Id</param>
        /// <param name="resourceParameters">an object that contains the pagination and the filter date</param>
        /// <response code="200">Get the issue reports successfully</response>
        /// <response code="400">If the request is nor well formatted or the Lob Space Id is wrong</response>
        [HttpPost("LobSpaceTypes/{LobSpaceTypeId?}")]
        public async Task<IActionResult> GetPaginatedIssueReports([FromRoute] long? lobSpaceTypeId,
                                                            [FromBody] IssueReportsResourceParameters resourceParameters)
        {
            var response = await _issueReportService.GetPaginatedIssueReportsAsync(lobSpaceTypeId, resourceParameters);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Report Case Stage
        /// </summary>
        /// <param name="issueReportId">Id of the issue report</param>
        /// <response code="200">Get the report case stage successfully</response>
        /// <response code="400">if the id is wrong</response>
        [HttpGet("CaseStages/{IssueReportId}")]
        public async Task<IActionResult> GetIssueReportCaseStages([FromRoute] long issueReportId)
        {
            var response = await _issueReportService.GetIssueReportCaseStages(issueReportId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Delete Issue Report
        /// </summary>
        /// <param name="issueReportId">Id of the issue report</param>
        /// <response code="204">Delete the issue report successfully</response>
        /// <response code="500">Server failed to delete the issue report</response>
        [HttpDelete("{IssueReportId}")]
        public async Task<IActionResult> DeleteIssueReport([FromRoute] long issueReportId)
        {
            var response = await _issueReportService.DeleteIssueReportAsync(issueReportId);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{issueReportId}")]
        public async Task<IActionResult> UpdateIssueReport([FromRoute] long issueReportId,
                                                     [FromBody] UpdateIssueReportDto updateIssueReportDto)
        {
            var response = await _issueReportService.UpdateIssueReportAsync(issueReportId, updateIssueReportDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
