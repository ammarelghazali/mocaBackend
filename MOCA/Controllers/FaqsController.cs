using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.FaqDtos.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class FaqsController : ControllerBase
    {
        private readonly IFaqService _faqService;

        public FaqsController(IFaqService faqService)
        {
            _faqService = faqService;
        }

        /// <summary>
        /// Add a faq
        /// </summary>
        /// <param name="faqForCreation">an object of the faq data with the lob space Id. if it is
        /// set to null, it will be added to the non-relatable to an lob space type faqs</param>
        /// <param name="categoryId">Id of the category that the faq will be assigned to. 
        /// if it is not given or set to 0 the faq will be added to non categorized faqs</param>
        /// <response code="400">Request not formatted well or the question or answer are null or empty</response>
        /// <response code="500">If the server unables to add the faq due to server error</response>
        /// <response code="200">Add the Faq Successfully</response>
        [HttpPost("Category/{categoryId?}")]
        public async Task<IActionResult> AddFaq([FromBody] FaqForCreationDto faqForCreation,
                                                [FromRoute] long categoryId = 0)
        {
            var response = await _faqService.AddFaqAsync(faqForCreation, categoryId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get All Faqs of the space
        /// </summary>
        /// <param name="getFaqsDto">an object that has the id of the the lob space Id. if it is
        /// set to null, it will  the non-relatable to an lob space type faqs</param>
        /// <response code="200">Returns all the space faqs</response>
        /// <response code="400">If the given lob space type id is not correct</response>
        [HttpGet]
        public async Task<IActionResult> GetAllFaqs([FromQuery] FaqsRequestSpaceIdDto getFaqsDto)
        {
            var response = await _faqService.GetAllFaqsAsync(getFaqsDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get all faqs of a category by its id
        /// </summary>
        /// <param name="getFaqsDto">an object has the the lob space Id. if it is
        /// set to null, it will the non-relatable to an lob space type faqs</param>
        /// <param name="categoryId">Id of the category. 
        /// the default is zero (or it is not given) that will return the non-categorized faqs</param>
        /// <response code="400">If the request is not formatted well or 
        /// cannot find the faqs of the category with the given category id</response>
        /// <response code="200">Returns all the faqs of the category</response>
        [HttpGet("Category/{categoryId?}")]
        public async Task<IActionResult> GetFaqsByCategoryIdAsync([FromQuery] FaqsRequestSpaceIdDto getFaqsDto,
                                                                  [FromRoute] long categoryId = 0)
        {
            var response = await _faqService.GetFaqsByCategoryIdAsync(getFaqsDto, categoryId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get a single faq by id
        /// </summary>
        /// <param name="faqId">Id of the faq</param>
        /// <response code="400">Cannot find a faq by the given id</response>
        /// <response code="200">Returns the faq Successfully</response>
        [HttpGet("{faqId}")]
        public async Task<IActionResult> GetSingleFaq([FromRoute] long faqId)
        {
            var response = await _faqService.GetSingleFaqAsync(faqId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates a single faq
        /// </summary>
        /// <param name="faqId">Id of the faq to be updated</param>
        /// <param name="faqForUpdateDto">an object with the category id (new or old one, and if it is not given 
        /// or set to null the faq will be moved to the non-categorized faqs) new question and answer
        /// with the lob space Id. if it is set to null, it will 
        /// work on the non-relatable to an lob space type faqs</param>
        /// <response code="200">Updates Faq Successfully</response>
        /// <response code="400">If Request is not formatted well, Question or Answer are null or empty, or 
        /// Cannot Find category or faq with the given ids </response>
        /// <response code="500">If the server unables to update the faq due to server error</response>

        [HttpPut("{faqId}")]
        public async Task<IActionResult> UpdateFaq([FromRoute] long faqId,
                                                   [FromBody] FaqForUpdateDto faqForUpdateDto)
        {
            var response = await _faqService.UpdateFaqAsync(faqId, faqForUpdateDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates Faqs Display Order 
        /// </summary>
        /// <param name="updateFaqsDisplayOrderDto">an object that has the lob space Id. if it is
        /// set to null, it will be work on the non-relatable to an lob space type faqs and
        /// a list of categories ids, each category id (new or old one, and if it is not given 
        /// or set to null so it's the display order the non-categorized faqs)
        /// with their faqs display order</param>
        /// <response code="204">Updates faqs successfully</response>
        /// <response code="400">Request not Formatted well or there are faqs of single category
        /// with the same order or Cannot Find category or faq with the given ids</response>
        /// <response code="500">If the server failed to update the faq due to server error</response>

        [HttpPut]
        public async Task<IActionResult> UpdateFaqsDisplayOrder(
                                               [FromBody] UpdateFaqsDisplayOrderDto updateFaqsDisplayOrderDto)
        {
            var response = await _faqService.UpdateFaqsDisplayOrderAsync(updateFaqsDisplayOrderDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Delete Faq
        /// </summary>
        /// <param name="faqDto">an object has the lob space Id. if it is
        /// set to null, it will be added to the non-relatable to an lob space type faqs</param>
        /// <param name="faqId">Id of the faq to be deleted</param>
        /// <response code="400">Cannot find a faq with the given id or 
        /// the request is not formatted well</response>
        /// <response code="204">Deletes the faq successfully</response>
        /// <response code="500">If the server unables to delete the faq due to server error</response>
        [HttpDelete("{faqId}")]
        public async Task<IActionResult> DeleteFaq([FromQuery] FaqsRequestSpaceIdDto faqDto,
                                                   long faqId)
        {
            var response = await _faqService.DeleteFaqAsync(faqDto, faqId);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
