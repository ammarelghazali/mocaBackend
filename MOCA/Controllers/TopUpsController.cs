using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.TopUpDtos.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TopUpsController : ControllerBase
    {
        private readonly ITopUpsService _topUpsService;

        public TopUpsController(ITopUpsService topUpsService)
        {
            _topUpsService = topUpsService;
        }

        /// <summary>
        /// Add TopUp
        /// </summary>
        /// <param name="topUpTypeId"></param>
        /// <param name="topUpCreationDto"></param>
        /// <response code="200">Added successfully</response>
        /// <response code="400">Ids are wrong</response>
        /// <response code="500">Server Error</response>
        [HttpPost]
        public async Task<IActionResult> Add(long topUpTypeId, TopUpCreateionDto topUpCreationDto)
        {
            var response = await _topUpsService.Add(topUpTypeId, topUpCreationDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get topUp by type and lobSpaceType (lobSpaceType is optional)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="topUpForLobSpaceType"></param>
        /// <response code="200">Added successfully</response>
        /// <response code="400">Ids are wrong</response>
        /// <response code="500">Server Error</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByTopUpTypeId(long id, [FromQuery] TopUpForLobSpaceTypeDto topUpForLobSpaceType)
        {
            var response = await _topUpsService.GetByTopUpTypeId(id, topUpForLobSpaceType);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Update topUp by type and lobSpaceType (lobSpaceType is optional)
        /// </summary>
        /// <param name="topUpTypeId"></param>
        /// <param name="topUpCreationDto"></param>
        /// <response code="200">Added successfully</response>
        /// <response code="400">Ids are wrong</response>
        /// <response code="500">Server Error</response>
        [HttpPut]
        public async Task<IActionResult> Update(long topUpTypeId, TopUpCreateionDto topUpCreationDto)
        {
            var response = await _topUpsService.Update(topUpTypeId,
                new TopUpForLobSpaceTypeDto { LobSpaceTypeId = topUpCreationDto.LobSpaceTypeId }
                , new UpdateTopUpDto { TermsOfUse = topUpCreationDto.TermsOfUse });

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Delete topUp by type and lobSpaceType (lobSpaceType is optional)
        /// </summary>
        /// <param name="topUpTypeId"></param>
        /// <param name="topUpForLobSpaceTypeDto"></param>
        /// <response code="200">Added successfully</response>
        /// <response code="400">Ids are wrong</response>
        /// <response code="500">Server Error</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(long topUpTypeId, TopUpForLobSpaceTypeDto topUpForLobSpaceTypeDto)
        {
            var response = await _topUpsService.Delete(topUpTypeId, topUpForLobSpaceTypeDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
