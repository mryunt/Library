using Library.Business.Abstract;
using Library.DAL.Context;
using Library.DAL.Dtos.SubCategory;
using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Business.Concrete
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly LibraryDbContext _libraryDbContext;
        public SubCategoryService(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public async Task<List<GetListSubCategoryDto>> GetListSubCategory()
        {
            return await _libraryDbContext.SubCategories.Where(p => !p.isDeleted).Select(p => new GetListSubCategoryDto
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                Name = p.Name
            }).ToListAsync();
        }
        public async Task<GetSubCategoryDto> GetSubCategoryById(int id)
        {
            return await _libraryDbContext.SubCategories.Where(p => !p.isDeleted && p.Id == id).Select(p => new GetSubCategoryDto
            {
                CategoryId = p.CategoryId,
                Name = p.Name
            }).FirstOrDefaultAsync();
        }
        public async Task<int> AddSubCategory(AddSubCategoryDto addSubCategoryDto)
        {
            var addSubCategory = new SubCategory
            {
                Name = addSubCategoryDto.Name,
                CategoryId = addSubCategoryDto.CategoryId
            };
            await _libraryDbContext.AddAsync(addSubCategory);
            return await _libraryDbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateSubCategory(int id, UpdateSubCategoryDto updateSubCategoryDto)
        {
            var currentSubCategory = await _libraryDbContext.SubCategories.FirstOrDefaultAsync(p => !p.isDeleted && p.Id == id);
            if (currentSubCategory != null)
            {
                currentSubCategory.Name = updateSubCategoryDto.Name;
                currentSubCategory.MDate = DateTime.Now;
                _libraryDbContext.SubCategories.Update(currentSubCategory);
                return await _libraryDbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> DeleteSubCategory(int id)
        {
            var currentSubCategory = await _libraryDbContext.SubCategories.FirstOrDefaultAsync(p => !p.isDeleted && p.Id == id);
            if (currentSubCategory != null)
            {
                currentSubCategory.isDeleted = true;
                return await _libraryDbContext.SaveChangesAsync();
            }
            return -1;
        }
    }
}
