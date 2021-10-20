using Library.Business.Abstract;
using Library.Business.Validation.Book;
using Library.DAL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("GetBookList")]
        public async Task<ActionResult<List<GetListBookDto>>> GetBookList()
        {
            try
            {
                return Ok(await _bookService.GetBookList());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetBookById/{id}")]
        public async Task<ActionResult<GetBookDto>> GetBookById(int id)
        {
            var list = new List<string>();
            if (id <= 0)
            {
                list.Add("Geçersiz ID");
                return Ok(new { code = StatusCode(1002), message = list, type = "error" });
            }
            try
            {
                var currentBook = await _bookService.GetBookById(id);
                if (currentBook == null)
                {
                    list.Add("Kitap Bulunamadı!");
                    return Ok(new { code = StatusCode(1001), message = list, type = "error" });
                }
                else
                {
                    return currentBook;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddBook")]
        public async Task<ActionResult<string>> AddBook(AddBookDto addBookDto)
        {
            var list = new List<string>();
            var validator = new BookAddValidator();
            var validationResults = validator.Validate(addBookDto);
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
                var result = await _bookService.AddBook(addBookDto);
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
        [HttpPut("UpdateBook/{id}")]
        public async Task<ActionResult<string>> UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            var list = new List<string>();
            var validator = new BookUpdateValidator();
            var validationResults = validator.Validate(updateBookDto);
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
                var result = await _bookService.UpdateBook(id, updateBookDto);
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
        [HttpDelete("DeleteBook/{id}")]
        public async Task<ActionResult<string>> DeleteBook(int id)
        {
            var list = new List<string>();
            try
            {
                var result = await _bookService.DeleteBook(id);
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
