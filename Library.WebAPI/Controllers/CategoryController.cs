using Library.Business.Abstract;
using Library.Business.Validation.Category;
using Library.DAL.Dtos.Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.WebAPI.Controllers
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
        [HttpGet("GetCategoryList")]
        public async Task<ActionResult<List<GetListCategoryDto>>> GetCategoryList()
        {
            try
            {
                return Ok(await _categoryService.GetCategoryList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<GetCategoryDto>> GetCategoryById(int id)
        {
            var list = new List<string>();
            if (id <= 0)
            {
                list.Add("Geçersiz ID!");
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var currentCategory = await _categoryService.GetCategoryById(id);
                if (currentCategory == null)
                {
                    list.Add("Kayıt Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return currentCategory;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddCategory")]
        public async Task<ActionResult<string>> AddCategor(AddCategoryDto addCategoryDto)
        {
            var list = new List<string>();
            var validator = new CategoryAddValidator();
            var validationResults = validator.Validate(addCategoryDto);
            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            try
            {
                var result = await _categoryService.AddCategory(addCategoryDto);
                if (result > 0)
                {
                    list.Add("Kayıt Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Kayıt Başarısız!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateCategory/{id}")]
        public async Task<ActionResult<string>> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
        {
            var list = new List<string>();
            var validator = new CategoryUpdateValidator();
            var validationResults = validator.Validate(updateCategoryDto);
            if (!validationResults.IsValid)
            {
                foreach (var error in validationResults.Errors)
                {
                    list.Add(error.ErrorMessage);
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
            }
            try
            {
                var result = await _categoryService.UpdateCategory(id, updateCategoryDto);
                if (result > 0)
                {
                    list.Add("Güncelleme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Kayıt Bulunamadı!");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
                else
                {
                    list.Add("Güncelleme İşlemi Başarısız!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<string>> DeleteCategory(int id)
        {
            var list = new List<string>();
            try
            {
                var result = await _categoryService.DeleteCategory(id);
                if (result > 0)
                {
                    list.Add("Silme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Silme İşlemi Başarısız!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
