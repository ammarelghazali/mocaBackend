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
