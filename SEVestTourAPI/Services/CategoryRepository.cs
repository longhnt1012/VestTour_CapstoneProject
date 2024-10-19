using AutoMapper;
using Microsoft.EntityFrameworkCore;
<<<<<<<< Updated upstream:SEVestTourAPI/Services/CategoryRepository.cs
using SEVestTourAPI.Entities;
using SEVestTourAPI.Models;


namespace SEVestTourAPI.Services
========
using VestTour.Domain.Entities;
using VestTour.Repository.Models;
using VestTour.Repository.Interface;
using VestTour.Repository.Data;

namespace VestTour.Repository.Implementation
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/CategoryRepository.cs
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
<<<<<<<< Updated upstream:SEVestTourAPI/Services/CategoryRepository.cs
       
        // Delete a category by ID
========

>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/CategoryRepository.cs
        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

<<<<<<<< Updated upstream:SEVestTourAPI/Services/CategoryRepository.cs
      

        
========
        public async Task<List<CategoryModel>> GetCategoriesByParentIdAsync(int parentId)
        {
            var categories = await _context.Categories
                .Where(c => c.CategoryParentId == parentId)
                .ToListAsync();
            return _mapper.Map<List<CategoryModel>>(categories);
        }
>>>>>>>> Stashed changes:Backend/VestTour.Repository/Repositories/CategoryRepository.cs
    }
}
