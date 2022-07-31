﻿using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.Events.EventCategoryDtos.Request;
using MOCA.Core.Interfaces.Events.Services;

namespace Events.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EventCategoryController : ControllerBase
    {
        private readonly IEventCategoryService _eventCategoryService;

        public EventCategoryController(IEventCategoryService eventCategoryService)
        {
            _eventCategoryService = eventCategoryService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] get_AllEventCategory_Parameter filter)
        {
            var response = await _eventCategoryService.GetAll(new get_AllEventCategory_Query(filter.pageNumber, filter.pageSize));
            return Ok(response);
        }
    }
}
