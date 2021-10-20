using Library.DAL.Dtos.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Abstract
{
    public interface ISubCategoryService
    {
        public Task<List<GetListSubCategoryDto>> GetListSubCategory();
        public Task<GetSubCategoryDto> GetSubCategoryById(int id);
        public Task<int> AddSubCategory(AddSubCategoryDto addSubCategoryDto);
        public Task<int> UpdateSubCategory(int id, UpdateSubCategoryDto updateSubCategoryDto);
        public Task<int> DeleteSubCategory(int id);
    }
}
