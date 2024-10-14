using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;


namespace SEVestTourAPI.Services
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

        // Get all categories
        public async Task<List<CategoryModel>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryModel>>(categories);
        }

        // Get category by ID
        public async Task<CategoryModel?> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            return _mapper.Map<CategoryModel>(category);
        }

        // Create a new category
        public async Task<int> AddCategoryAsync(CategoryModel category)
        {
            {
                var newCate = _mapper.Map<Category>(category);
                _context.Categories!.Add(newCate);
                await _context.SaveChangesAsync();
                return newCate.CategoryId;
            }
        }

        // Update a category
        public async Task UpdateCategoryAsync(int id, CategoryModel category)
        {

            if (id == category.CategoryId)
            {
                var updateCategory = _mapper.Map<Category>(category);
                _context.Categories!.Update(updateCategory);
                await _context.SaveChangesAsync();
            }
        }
       
        // Delete a category by ID
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
