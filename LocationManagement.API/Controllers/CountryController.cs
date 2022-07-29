using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.LocationManagment;
using MOCA.Core.DTOs.Shared.Responses;
using MOCA.Core.Entities.LocationManagment;
using MOCA.Core.Interfaces.LocationManagment.Repositories;

namespace LocationManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public CountryController(IMapper mapper, ICountryRepository countryRepository)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        /// <summary>
        /// Add A New Country 
        /// </summary>
        /// <param name="model"></param>
        /// <response code="200">Country added successfully</response>
        /// <response code="400">something goes wrong in backend</response>
        [HttpPost]
        public async Task<IActionResult> EventSpaceBooking(CountryModel model)
        {
            var response = new Response<long>();
            try
            {
                var data = _countryRepository.Insert(_mapper.Map<Country>(model), "System");
                await _countryRepository.SaveChanges();
                response.Succeeded = true;
                response.Data = data.Id;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}
