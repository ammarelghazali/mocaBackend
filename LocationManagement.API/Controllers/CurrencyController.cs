using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.LocationManagment.Currency;
using MOCA.Core.DTOs.Shared;
using MOCA.Core.Interfaces.LocationManagment.Services;

namespace LocationManagement.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CurrencyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICurrencyService _currencyService;
        public CurrencyController(IMapper mapper, ICurrencyService currencyService)
        {
            _mapper = mapper;
            _currencyService = currencyService;
        }

        /// <summary>
        /// Add A New Currency 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Currency added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost("AddCurrency")]
        public async Task<IActionResult> AddCurrency([FromBody] CurrencyModel model)
        {
            var data = await _currencyService.AddCurrency(model);

            if (data.Succeeded == false)
            {
                return BadRequest(data);
            }
            return Ok(data);
        }

        /// <summary>
        /// Update Currency 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Currency Updated successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPut("UpdateCurrency")]
        public async Task<IActionResult> UpdateCurrency([FromBody] CurrencyModel model)
        {
            return Ok(await _currencyService.UpdateCurrency(model));
        }

        /// <summary>
        /// Gets Currency By ID
        /// </summary>
        /// <param name="Id">an object holds the Id of Currency</param>
        /// <response code="200">Returns the Currency</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetCurrencyByID")]
        public async Task<IActionResult> GetCurrencyByID([FromQuery] long Id)
        {
            var response = await _currencyService.GetCurrencyByID(Id);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Paginated Currencies
        /// </summary>
        /// <param name="filter">an object holds the filter data</param>
        /// <response code="200">Returns the Currencies List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllCurrencies")]
        public async Task<IActionResult> GetAllCurrencies([FromQuery] RequestParameter filter)
        {
            var response = await _currencyService.GetAllCurrenciesWithPagination(filter);

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Gets Not Paginated Currencies
        /// </summary>
        /// <response code="200">Returns the Currencies List</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpGet("GetAllCurrenciesWithoutPagination")]
        public async Task<IActionResult> GetAllCurrenciesWithoutPagination()
        {
            var response = await _currencyService.GetAllCurrenciesWithoutPagination();

            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes Currency
        /// </summary>
        /// <param name="Id">an object holds the Id of Currency</param>
        /// <response code="200">Deletes Currency and all related data successfully</response>
        /// <response code="400">Currency not found, or there is error while saving</response>
        [HttpDelete("DeleteCurrency")]
        public async Task<IActionResult> DeleteCurrency([FromQuery] long Id)
        {
            var response = await _currencyService.DeleteCurrency(Id);
            if (response.Succeeded == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
