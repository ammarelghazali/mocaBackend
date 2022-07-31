using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.TopUpTypeDtos.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TopUpTypesController : ControllerBase
    {
        private readonly ITopUpTypesService _topUpTypesService;

        public TopUpTypesController(ITopUpTypesService topUpTypesService)
        {
            _topUpTypesService = topUpTypesService;
        }

        /// <summary>
        /// Get All Top Up Types
        /// </summary>
        /// <response code="200">Returns the Available Top Up Types</response>
        /// <response code="400">the Request is not well formatted</response>
        [HttpGet]
        public async Task<IActionResult> GetAllTopUpTypes()
        {
            var response = await _topUpTypesService.GetAllTopUpTypes();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Single Top Up Type by Id
        /// </summary>
        /// <param name="id">Id of the Top Up Type</param>
        /// <response code="200">Returns the Top Up Type</response>
        /// <response code="400">the Request is not well formatted, or the id is wrong</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopUpTypeById([FromRoute] long id)
        {
            var response = await _topUpTypesService.GetTopUpTypeById(id);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Add new Top Up Type
        /// </summary>
        /// <param name="addTopUpTypeDto">an object that has name and URL of the Top Up Type</param>
        /// <response code="200">Added the Top Up Type Successfully</response>
        /// <response code="400">the Request is not well formatted, or top Up Type with the same name is already exist</response>
        [HttpPost]
        public async Task<IActionResult> AddTopUpType(AddTopUpTypeDto addTopUpTypeDto)
        {
            var response = await _topUpTypesService.AddTopUpType(addTopUpTypeDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deletes Top Up Type
        /// </summary>
        /// <param name="id">Id of the Top Up Type</param>
        /// <response code="200">Deletes the Top Up Type Successfully</response>
        /// <response code="400">the Request is not well formatted, or top Up Type id is wrong</response>      
        [HttpDelete]
        public async Task<IActionResult> DeleteTopUpType(long id)
        {
            var response = await _topUpTypesService.DeleteTopUpType(id);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates Top Up Type
        /// </summary>
        /// <param name="id">id of the Top Up</param>
        /// <param name="topUpTypeDto">an object that has the Top Up name and URL</param>
        /// <response code="200">Updates the Top Up Type Successfully</response>
        /// <response code="400">the Request is not well formatted, or top Up Type id is wrong</response> 
        [HttpPut]
        public async Task<IActionResult> UpdateTopUpType(long id, AddTopUpTypeDto topUpTypeDto)
        {
            var response = await _topUpTypesService.UpdateTopUpType(id, topUpTypeDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
