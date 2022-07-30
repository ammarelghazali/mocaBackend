using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.LobSpaceTypeDtos;
using MOCA.Core.DTOs.MocaSettings.PoliciesDtos.Requests;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PoliciesController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PoliciesController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        /// <summary>
        /// Adds Policy
        /// </summary>
        /// <param name="policyForCreationDto">an object that has the Descripiton of the Policy. 
        /// with Id of the Lob Space Type. If it is set to null, 
        /// it will be the non-relatable to a space type policies</param>
        /// <param name="policyTypeId">Id of the Policy Type</param>
        /// <response code="200">Adds the policy successfully</response>
        /// <response code="500">Server failed to add the policy</response>
        /// <response code="400">If the request is not well formatted, or the policy
        /// type id is not correct</response>
        [HttpPost("PolicyType/{policyTypeId}")]
        public async Task<IActionResult> AddPolicy([FromBody] PolicyForCreationDto policyForCreationDto,
                                             [FromRoute] long policyTypeId)
        {
            var response = await _policyService.AddPolicyAsync(policyForCreationDto, policyTypeId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Update Policy
        /// </summary>
        /// <param name="policyId">Id of the Policy</param>
        /// <param name="policyForCreationDto">an object that has the Descripiton of the Policy. 
        /// with the the Lob Space Type. If it is set to null, 
        /// it will be the non-relatable to a space type policies</param>
        /// <response code="200">Updates the policy successfully</response>
        /// <response code="500">Server failed to add the policy</response>
        /// <response code="400">If the request is not well formatted, or the policy
        /// id is not correct</response>
        [HttpPut("{policyId}")]
        public async Task<IActionResult> UpdatePolicy([FromRoute] long policyId,
                                                      [FromBody] PolicyForCreationDto policyForCreationDto)
        {
            var response = await _policyService.UpdatePolicyAsync(policyId, policyForCreationDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Policy by Policy Id
        /// </summary>
        /// <param name="policyId">Id of the Policy</param>
        /// <response code="200">Gets the policy successfully</response>
        /// <response code="400">If the request is not well formatted, or the policy
        /// id is not correct</response>
        [HttpGet("{policyId}")]
        public async Task<IActionResult> GetPolicyById([FromRoute] long policyId)
        {
            var response = await _policyService.GetPolicyByIdAsync(policyId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Policy by PolicyType Id
        /// </summary>
        /// <param name="policyTypeId">Id of the Policy Type</param>
        /// <param name="spaceTypeDto">Id of the Lob Space Type. If it is set to null, 
        /// it will be the non-relatable to a space type policies</param>
        /// <response code="200">Gets the policy successfully</response>
        /// <response code="400">If the request is not well formatted, or the policy type
        /// id is not correct</response>
        [HttpGet("PolicyType/{policyTypeId}")]
        public async Task<IActionResult> GetPolicyByPolicyTypeId([FromRoute] long policyTypeId,
                                                            [FromQuery] LobSpaceTypeIdDto spaceTypeDto)
        {
            var response = await _policyService.GetPolicyByTypeIdAsync(policyTypeId,
                                                                        spaceTypeDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get All Policies with Name of its Policy Type 
        /// </summary>
        /// <param name="lobSpaceTypeId">Id of the Lob Space Type. If it is set to null, 
        /// it will be the non-relatable to a space type policies</param>
        /// <response code="200">Gets all the policies successfully</response>
        /// <response code="400">If the request is not well formatted</response>
        [HttpGet("LobSpaceType/{lobSpaceTypeId?}")]
        public async Task<IActionResult> GetAllPolicies([FromRoute] long lobSpaceTypeId)
        {
            var response = await _policyService.GetAllPoliciesAsync(lobSpaceTypeId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Delete Policy
        /// </summary>
        /// <param name="policyId">Id of the Policy</param>
        /// <param name="spaceTypeDto">Id of the Lob Space Type. If it is set to null, 
        /// it will be the non-relatable to a space type policies</param>
        /// <response code="204">Deletes the policy successfully</response>
        /// <response code="500">Server failed to delete the policy</response>
        /// <response code="400">If the request is not well formatted, or the policy
        /// id is not correct</response>
        [HttpDelete("{policyId}")]
        public async Task<IActionResult> DeletePolicy([FromRoute] long policyId,
                                                      [FromBody] LobSpaceTypeIdDto spaceTypeDto)
        {
            var response = await _policyService.DeletePolicyAsync(policyId, spaceTypeDto);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
