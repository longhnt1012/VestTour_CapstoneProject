using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;

namespace VestTour.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly VestTourDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(VestTourDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryModel>>(categories);
        }

        public async Task<CategoryModel?> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            return _mapper.Map<CategoryModel>(category);
        }

        public async Task<int> AddCategoryAsync(CategoryModel category)
        {
            var newCategory = _mapper.Map<Category>(category);
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory.CategoryId;
        }

        public async Task UpdateCategoryAsync(int id, CategoryModel category)
        {
            var updateCategory = _mapper.Map<Category>(category);
            _context.Categories.Update(updateCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CategoryModel>> GetCategoriesByParentIdAsync(int parentId)
        {
            var categories = await _context.Categories
                .Where(c => c.CategoryParentId == parentId)
                .ToListAsync();
            return _mapper.Map<List<CategoryModel>>(categories);
        }
    }
}
