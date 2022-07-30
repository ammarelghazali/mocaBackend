using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.CaseTypesDtos.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [Route("api/v{version:apiVersion}/Issues/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CaseTypesController : ControllerBase
    {
        private readonly ICaseTypeService _caseTypeService;

        public CaseTypesController(ICaseTypeService caseTypeService)
        {
            _caseTypeService = caseTypeService;
        }

        /// <summary>
        /// Get All Case Types
        /// </summary>
        /// <response code="200">Returns all the case types successfully</response>
        [HttpGet]
        public async Task<IActionResult> GetAllCaseTypes()
        {
            var response = await _caseTypeService.GetAllCaseTypesAsync();

            if (!response.Succeeded)
            {
                return BadRequest(response); 
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Single Case Type By Id
        /// </summary>
        /// <param name="caseTypeId">Id of the case type</param>
        /// <response code="200">Returns the case type successfully</response>
        /// <response code="400">The case type is not found or the id is wrong</response>
        [HttpGet("{CaseTypeId}")]
        public async Task<IActionResult> GetSingleCaseType([FromRoute] long caseTypeId)
        {
            var response = await _caseTypeService.GetSingleCaseTypeAsync(caseTypeId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Adds a case type
        /// </summary>
        /// <param name="caseType">an object that holds the name of the case type</param>
        /// <response code="200">Adds the case type successfully</response>
        /// <response code="400">Request is not well formatted</response>
        /// <response code="500">Server failed to add the case type</response>
        [HttpPost]
        public async Task<IActionResult> AddCaseType([FromBody] CaseTypeForCreationDto caseType)
        {
            var response = await _caseTypeService.AddCaseTypeAsync(caseType);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates a Case Type
        /// </summary>
        /// <param name="caseTypeId">Id of the case type</param>
        /// <param name="caseType">an object that holds the name of the case type</param>
        /// <response code="200">Updates the case type successfully</response>
        /// <response code="400">Request is not well formatted or the id is wrong</response>
        /// <response code="500">Server failed to update the case type</response>
        [HttpPut("{CaseTypeId}")]
        public async Task<IActionResult> UpdateCaseType([FromRoute] long caseTypeId,
                                                  [FromBody] CaseTypeForCreationDto caseType)
        {
            var response = await _caseTypeService.UpdateCaseTypeAsync(caseTypeId, caseType);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes the case type
        /// </summary>
        /// <param name="caseTypeId">Id of the case type</param>
        /// <response code="204">Deletes the case type successfully</response>
        /// <response code="400">Case Type not found or the id is wrong</response>
        /// <response code="500">Server failed to delete the case type</response>
        [HttpDelete("{CaseTypeId}")]
        public async Task<IActionResult> DeleteCaseType([FromRoute] long caseTypeId)
        {
            var response = await _caseTypeService.DeleteCaseTypeAsync(caseTypeId);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
