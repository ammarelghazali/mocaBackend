using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.WifiDtos.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class WifisController : ControllerBase
    {
        private readonly IWifisService _wifisService;

        public WifisController(IWifisService wifisService)
        {
            _wifisService = wifisService;
        }

        /// <summary>
        /// Add Wifi
        /// </summary>
        /// <param name="wifiForCreation">an object that has the wifi description</param>
        /// <param name="lobSpaceTypeId">Id of the Lob Space Type.</param>
        /// <response code="200">Wifi is added Successfully</response>
        /// <response code="400">if the request is not formatted well, or the Lob Space Id is not
        /// correct</response>
        /// <response code="500">Wifi Failed to save the Wifi</response>
        [HttpPost("{lobSpaceTypeId?}")]
        public async Task<IActionResult> AddWifi([FromBody] WifiForCreationDto wifiForCreation,
                                                 [FromRoute] long lobSpaceTypeId)
        {
            var response = await _wifisService.AddWifi(lobSpaceTypeId, wifiForCreation);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Update Wifi
        /// </summary>
        /// <param name="wifiForCreation">an object that has the wifi description</param>
        /// <param name="lobSpaceTypeId">Id of the Lob Space Type.</param>
        /// <response code="200">Wifi is updated Successfully</response>
        /// <response code="400">if the request is not formatted well, or the Lob Space Id is not
        /// correct</response>
        /// <response code="500">Wifi Failed to save the Wifi</response>
        [HttpPut("{lobSpaceTypeId?}")]
        public async Task<IActionResult> UpdateWifi([FromBody] WifiForCreationDto wifiForCreation,
                                              [FromRoute] long lobSpaceTypeId)
        {
            var response = await _wifisService.UpdateWifi(lobSpaceTypeId, wifiForCreation);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Wifi
        /// </summary>
        /// <param name="lobSpaceTypeId">Id of the Lob Space Type.</param>
        /// <response code="200">Wifi is returned Successfully</response>
        /// <response code="400">if the request is not formatted well, or the Lob Space Id is not
        /// correct</response>
        [HttpGet("{lobSpaceTypeId?}")]
        public async Task<IActionResult> GetWifi([FromRoute] long lobSpaceTypeId)
        {
            var response = await _wifisService.GetWifi(lobSpaceTypeId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Delete Wifi
        /// </summary>
        /// <param name="lobSpaceTypeId">Id of the Lob Space Type.</param>
        /// <response code="204">Wifi is deleted Successfully</response>
        /// <response code="400">if the request is not formatted well, or the Lob Space Id is not
        /// correct</response>
        /// <response code="500">Wifi Failed to save the Wifi</response>
        [HttpDelete("{lobSpaceTypeId?}")]
        public async Task<IActionResult> DeleteWifi([FromRoute] long lobSpaceTypeId)
        {
            var response = await _wifisService.DeleteWifi(lobSpaceTypeId);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
