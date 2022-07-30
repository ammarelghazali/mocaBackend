using Microsoft.AspNetCore.Mvc;
using MOCA.Core.DTOs.MocaSettings.CategoryDtos.Request;
using MOCA.Core.Interfaces.MocaSettings.Services;

namespace MocaSettings.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="categoryForCreation">an object hold the category name and 
        /// the Lob Space Type Id, If it is set to null, 
        /// it will be the non-relatable to a space type categories</param>
        /// <response code="200">If the category added successfully</response>
        /// <response code="400">If the request body is not well formatted, or Lob space id
        /// is not correct</response>
        /// <response code="500">If the server failed to add the category</response>
        [HttpPost]
        public async Task<IActionResult> AddNewCategory([FromBody] CategoryForCreationDto categoryForCreation)
        {
            var response = await _categoryService.AddCategoryAsync(categoryForCreation);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get All the Categories of the Given Space
        /// </summary>
        /// <param name="getAllCategoriesDto">an object with the Lob Space Type Id, 
        /// If it is set to null, it will be the non-relatable to a space type categories and two boolean flags, one 
        /// to determine whether to returns the related faqs of each category or not, and the other
        /// to determine whether to return the non-categorized faqs or not. The default of both is false</param>
        /// <response code="200">Returns the categories successfully,or Lob space id
        /// is not correct</response>
        /// <response code="400">If the request body is not well formatted</response>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoriesDto getAllCategoriesDto)
        {
            var response = await _categoryService.GetAllCategoriesAsync(getAllCategoriesDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get Single Category By its Id
        /// </summary>
        /// <param name="categoryId">Id of the category</param>
        /// <param name="getSingleCategoryDto">an object that has the Lob space type id,
        /// If it is set to null, it will be the non-relatable to a space type categories and
        /// a flag to determine whether to returns the related faqs of each category or not and 
        /// its default is true</param>
        /// <response code="200">If the category returned successfully</response>
        /// <response code="400">If the Request is not well formatted or 
        /// the id of the category is not correct or not in the Lob space id</response>
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetSingleCategory([FromRoute] long categoryId,
                                                           [FromQuery] GetSingleCategoryDto getSingleCategoryDto)
        {
            var response = await _categoryService.GetSingleCategoryAsync(getSingleCategoryDto, categoryId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Delete a Category by its Id
        /// </summary>
        /// <param name="categoryId">Id of the Category</param>
        /// <param name="deleteCategoryDto">an object that has the lob space type id,
        /// If it is set to null, it will be the non-relatable to a space type categories. and a boolean flag
        /// to determine whether to delete the realted faqs of that category or not, if it is false
        /// the faqs will be moved to the non-categorized faqs. the default is true</param>
        /// <response code="204">If the category deleted successfully</response>
        /// <response code="400">If the request is not well formatted, 
        /// or the category id is not correct, or not in the given lob space type id</response>
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] long categoryId,
                                                        [FromQuery] DeleteCategoryDto deleteCategoryDto)
        {
            var response = await _categoryService.DeleteCategoryAsync(categoryId, deleteCategoryDto);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Update a Category
        /// </summary>
        /// <param name="categoryId">Id of the Category</param>
        /// <param name="categoryForUpdate">an object that has the updated name of the category. 
        /// with Id of the Lob Space Type. If it is set to null, 
        /// it will be the non-relatable to a space type categories</param>
        /// <response code="200">If the Category Updated Successfully</response>
        /// <response code="400">If the request is not formatted well, 
        /// or the category id is not correct, or not in the given lob space type id</response>
        /// <response code="500">If the server failed to update the category</response>
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] long categoryId,
                                                        [FromBody] CategoryForUpdateDto categoryForUpdate)
        {
            var response = await _categoryService.UpdateCategoryAsync(categoryId, categoryForUpdate);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Updates the Categories Display Order
        /// </summary>
        /// <param name="categoriesOrderDto">an objec that has the list of categories ids with their new
        /// display order</param>
        /// <param name="lobSpaceTypeId">Id of the Lob Space Type, if it's not given so it will 
        /// be the categories of the non-relatable to space type id</param>
        /// <response code="204">If the categories display order updated successfully</response>
        /// <response code="400">If the request is not formatted well, 
        /// one or more categories ids are not correct, 
        /// or their are categories with the same order, or not in the given lob space type id</response>
        /// <response code="500">If the server failed to update the categories display order</response>
        [HttpPut("LobSpaceType/{lobSpaceTypeId?}")]
        public async Task<IActionResult> UpdateCategoriesDisplayOrder(
                                        [FromBody] List<UpdateCategoriesOrderDto> categoriesOrderDto,
                                        long? lobSpaceTypeId = null)
        {
            var response = await _categoryService.UpdateCategoriesDisplayOrderAsync(categoriesOrderDto, lobSpaceTypeId);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
