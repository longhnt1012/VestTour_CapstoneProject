﻿using Microsoft.AspNetCore.Mvc;
using SEVestTourAPI.Models;
using SEVestTourAPI.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SEVestTourAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }
        // GET: api/Category/parent/3
        [HttpGet("parent/{parentId}")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoriesByParentId(int parentId)
        {
            var categories = await _categoryRepository.GetCategoriesByParentIdAsync(parentId);

            if (categories == null || categories.Count == 0)
            {
                return NotFound();
            }

            return Ok(categories);
        }
        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<int>> CreateCategory(CategoryModel categoryModel)
        {
           
            // Add the new category
            var newCategoryId = await _categoryRepository.AddCategoryAsync(categoryModel);
            return CreatedAtAction(nameof(GetCategory), new { id = newCategoryId }, newCategoryId);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel categoryModel)
        {
            if (id != categoryModel.CategoryId)
            {
                return BadRequest("Category ID mismatch.");
            }

            var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            await _categoryRepository.UpdateCategoryAsync(id, categoryModel);
            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryRepository.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
