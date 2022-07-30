using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.PolicyTypesDtos.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PolicyTypesController : ControllerBase
    {
        private readonly IPolicyTypesService _policyTypesService;

        public PolicyTypesController(IPolicyTypesService policyTypesService)
        {
            _policyTypesService = policyTypesService;
        }

        /// <summary>
        /// Adds Policy Type
        /// </summary>
        /// <param name="policyTypeDto">an object that has the name of the Policy Type</param>
        /// <response code="200">Policy Type Added Successfully</response>
        /// <response code="400">If the request is not well formatted</response>
        /// <response code="500">If the server failed to add the Policy Type</response>
        [HttpPost]
        public async Task<IActionResult> AddPolicyType([FromBody] PolicyTypeForCreationDto policyTypeDto)
        {
            var response = await _policyTypesService.AddPolicyType(policyTypeDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates a Policy Type
        /// </summary>
        /// <param name="policyTypeId">Id of the Policy Type</param>
        /// <param name="policyTypeDto">an Object that has the updated name of the Policy Type</param>
        /// <response code="200">Policy Type Updated Successfully</response>
        /// <response code="400">If the request is not well formatted, or the id is not correct</response>
        /// <response code="500">If the server failed to update the Policy Type</response>
        [HttpPut("{PolicyTypeId}")]
        public async Task<IActionResult> UpdatePolicyType([FromRoute] long policyTypeId,
                                                    [FromBody] PolicyTypeForCreationDto policyTypeDto)
        {
            var response = await _policyTypesService.UpdatePolicyType(policyTypeId, policyTypeDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes a Policy Type
        /// </summary>
        /// <param name="policyTypeId">Id of the Policy Type</param>
        /// <response code="204">Policy Type Deleted Successfully</response>
        /// <response code="400">If the request is not well formatted, or the id is not correct</response>
        /// <response code="500">If the server failed to Delete the Policy Type</response>
        [HttpDelete("{PolicyTypeId}")]
        public async Task<IActionResult> DeletePolicyType([FromRoute] long policyTypeId)
        {
            var response = await _policyTypesService.DeletePolicyType(policyTypeId);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Single Policy Type
        /// </summary>
        /// <param name="policyTypeId">Id of the Policy Type</param>
        /// <response code="200">Policy Type returned Successfully</response>
        /// <response code="400">If the request is not well formatted, 
        /// or the id is not correct</response>
        [HttpGet("{PolicyTypeId}")]
        public async Task<IActionResult> GetPolicyTypeById(long policyTypeId)
        {
            var response = await _policyTypesService.GetSinglePolicyType(policyTypeId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get all Policy Types
        /// </summary>
        /// <param name="policyTypesDto">an object that has a flag to determine whether to 
        /// return the policy types with their related Policy Description. The Default is true</param>
        /// <response code="200">Policy Types returned Successfully</response>
        /// <response code="400">If the request is not well formatted</response>
        [HttpGet]
        public async Task<IActionResult> GetAllPolicyTypes([FromQuery]
                                                                GetAllPolicyTypesDto policyTypesDto)
        {
            var response = await _policyTypesService
                                           .GetAllPolicyTypes(policyTypesDto.WithRelatedDescription,
                                                              policyTypesDto.LobSpaceTypeId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
