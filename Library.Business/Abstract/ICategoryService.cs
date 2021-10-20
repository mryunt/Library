using Library.DAL.Dtos.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Business.Abstract
{
    public interface ICategoryService
    {
        public Task<List<GetListCategoryDto>> GetCategoryList();
        public Task<GetCategoryDto> GetCategoryById(int id);
        public Task<int> AddCategory(AddCategoryDto addCategoryDto);
        public Task<int> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto);
        public Task<int> DeleteCategory(int id);
    }
}
