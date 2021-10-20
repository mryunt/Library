using Library.Business.Abstract;
using Library.Business.Validation.SubCategory;
using Library.DAL.Dtos.SubCategory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;
        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }
        [HttpGet("GetSubCategoryList")]
        public async Task<ActionResult<List<GetListSubCategoryDto>>> GetSubCategoryList()
        {
            try
            {
                return Ok(await _subCategoryService.GetListSubCategory());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetSubCategoryById/{id}")]
        public async Task<ActionResult<GetSubCategoryDto>> GetSubCategoryById(int id)
        {
            var list = new List<string>();
            if (id <= 0)
            {
                list.Add("Geçersiz ID!");
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var currentSubCategory = await _subCategoryService.GetSubCategoryById(id);
                if (currentSubCategory == null)
                {
                    list.Add("Kayıt Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return currentSubCategory;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddSubCategory")]
        public async Task<ActionResult<string>> AddSubCategory(AddSubCategoryDto addSubCategoryDto)
        {
            var list = new List<string>();
            var validator = new SubCategoryAddValidator();
            var validationResults = validator.Validate(addSubCategoryDto);
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
                var result = await _subCategoryService.AddSubCategory(addSubCategoryDto);
                if (result > 0)
                {
                    list.Add("Kayıt İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else
                {
                    list.Add("Kayıt İşlemi Başarısız!");
                    return Ok(new { code = StatusCode(1001), meesage = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPut("UpdateSubCategory/{id}")]
        public async Task<ActionResult<string>> UpdateSubCategory(int id, UpdateSubCategoryDto updateSubCategoryDto)
        {
            var list = new List<string>();
            var validator = new SubCategoryUpdateValidator();
            var validationResults = validator.Validate(updateSubCategoryDto);
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
                var result = await _subCategoryService.UpdateSubCategory(id, updateSubCategoryDto);
                if (result > 0)
                {
                    list.Add("Güncelleme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Kayıt Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    list.Add("Güncelleme Başarısız!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteSubCategory/{id}")]
        public async Task<ActionResult<string>> DeleteSubCategory(int id)
        {
            var list = new List<string>();
            try
            {
                var result = await _subCategoryService.DeleteSubCategory(id);
                if (result > 0)
                {
                    list.Add("Silme İşlemi Başarılı!");
                    return Ok(new { code = StatusCode(1000), message = list, type = "success" });
                }
                else if (result == -1)
                {
                    list.Add("Kayıt Bulunamadı!");
                    return Ok(new { code = StatusCode(1002), message = list, type = "error" });
                }
                else
                {
                    list.Add("Silme işlemi Başarısız!");
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
