using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MOCA.Core.Interfaces.MocaSettings.Services;
using MOCA.Core.DTOs.MocaSettings.PlanDtos.Response;
using MOCA.Core.DTOs.MocaSettings.PlanDtos.Request;

namespace MocaSettings.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlansController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlansService _plansService;

        public PlansController(IPlansService plansService, IMapper mapper)
        {
            _plansService = plansService;
            _mapper = mapper;
        }



        /// <summary>
        /// Gets plan
        /// </summary>
        /// <param name="plantypeId"></param>
        /// <param name="planByLobTypeDto"></param>
        /// <response code="200">plan founded successfully</response>
        /// <response code="400">no plan founded</response>

        [HttpGet("{plantypeId}")]
        public async Task<IActionResult> GetByType(long plantypeId, [FromQuery] PlanByLobTypeDto planByLobTypeDto)
        {
            var response = await _plansService.GetByType(planByLobTypeDto.LobSpaceTypeId, plantypeId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }



        /// <summary>
        /// Adds plan
        /// </summary>
        /// <param name="planTypeId"></param>
        /// <param name="creationPlanDto"></param>
        /// <response code="200">plan added successfully</response>
        /// <response code="400">plan type not exist</response>
        /// <response code="500">server error</response>

        [HttpPost]
        public async Task<IActionResult> Add(long planTypeId, CreationPlanDto creationPlanDto)
        {
            var response = await _plansService.Add(creationPlanDto.LobSpaceTypeId, planTypeId, _mapper.Map<PlanDtoBase>(creationPlanDto));
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        /// <summary>
        /// updates plan
        /// </summary>
        /// <param name="planTypeId"></param>
        /// <param name="planDto"></param>
        /// <response code="200">plan updated successfully</response>
        /// <response code="400">plan not exist</response>
        /// <response code="500">server error</response>

        [HttpPut]
        public async Task<IActionResult> Update(long planTypeId, UpdatePlanDto planDto)
        {
            var response = await _plansService.Update(planDto.LobSpaceTypeId, planTypeId, _mapper.Map<PlanDtoBase>(planDto));
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        /// <summary>
        /// deletes plan
        /// </summary>
        /// <param name="planTypeId"></param>
        /// <param name="planByLobTypeDto"></param>
        /// <response code="200">plan deleted successfully</response>
        /// <response code="400">plan not exist</response>
        /// <response code="500">server error</response>

        [HttpDelete]
        public async Task<IActionResult> Delete(long planTypeId, PlanByLobTypeDto planByLobTypeDto)
        {
            var response = await _plansService.Delete(planByLobTypeDto.LobSpaceTypeId, planTypeId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
