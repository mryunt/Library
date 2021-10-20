using Library.Business.Abstract;
using Library.DAL.Context;
using Library.DAL.Dtos.Category;
using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryDbContext _libraryDbContext;
        public CategoryService(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public async Task<List<GetListCategoryDto>> GetCategoryList()
        {
            return await _libraryDbContext.Categories.Where(p => !p.isDeleted).Select(p => new GetListCategoryDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToListAsync();
        }
        public async Task<GetCategoryDto> GetCategoryById(int id)
        {
            return await _libraryDbContext.Categories.Where(p => p.isDeleted && p.Id == id).Select(p => new GetCategoryDto
            {
                Name = p.Name
            }).FirstOrDefaultAsync();
        }
        public async Task<int> AddCategory(AddCategoryDto addCategoryDto)
        {
            var addCategory = new Category
            {
                Name = addCategoryDto.Name
            };
            await _libraryDbContext.Categories.AddAsync(addCategory);
            return await _libraryDbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var currentCategory = await _libraryDbContext.Categories.FirstOrDefaultAsync(p => !p.isDeleted && p.Id == id);
            if (currentCategory!=null)
            {
                currentCategory.Name = updateCategoryDto.Name;
                currentCategory.MDate = DateTime.Now;
                _libraryDbContext.Update(currentCategory);
                return await _libraryDbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> DeleteCategory(int id)
        {
            var currentCategory = await _libraryDbContext.Categories.FirstOrDefaultAsync(p => !p.isDeleted && p.Id == id);
            if (currentCategory!=null)
            {
                currentCategory.isDeleted = true;
                return await _libraryDbContext.SaveChangesAsync();
            }
            return -1;
        }
    }
}
