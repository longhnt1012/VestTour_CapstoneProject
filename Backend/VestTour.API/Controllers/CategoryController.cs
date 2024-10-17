using Microsoft.AspNetCore.Mvc;
using VestTour.Repository.Models;
using VestTour.Service.Interface;
using VestTour.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CategoryModel>>>> GetCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("parent/{parentId}")]
        public async Task<ActionResult<ServiceResponse<List<CategoryModel>>>> GetCategoriesByParentId(int parentId)
        {
            var response = await _categoryService.GetCategoriesByParentIdAsync(parentId);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<CategoryModel>>> GetCategory(int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response.Message);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<int>>> CreateCategory(CategoryModel categoryModel)
        {
            var response = await _categoryService.AddCategoryAsync(categoryModel);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return CreatedAtAction(nameof(GetCategory), new { id = response.Data }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel categoryModel)
        {
            if (id != categoryModel.CategoryId)
            {
                return BadRequest(Error.CategoryIDmismatch);
            }
            var response = await _categoryService.UpdateCategoryAsync(id, categoryModel);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var response = await _categoryService.DeleteCategoryAsync(id);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return NoContent();
        }
    }
}