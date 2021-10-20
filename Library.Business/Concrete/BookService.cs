using Library.Business.Abstract;
using Library.DAL.Context;
using Library.DAL.Dtos;
using Library.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Business.Concrete
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _libraryDbContext;
        public BookService(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public async Task<List<GetListBookDto>> GetBookList()
        {
            return await _libraryDbContext.Books.Where(p => !p.isDeleted).Select(p => new GetListBookDto
            {
                Id = p.Id,
                Name = p.Name,
                Desc = p.Desc,
                SubCategoryId = p.SubCategoryId
            }).ToListAsync();
        }
        public async Task<GetBookDto> GetBookById(int id)
        {
            return await _libraryDbContext.Books.Where(p => !p.isDeleted && p.Id == id).Select(p => new GetBookDto
            {
                Name = p.Name,
                Desc = p.Desc,
                SubCategoryId = p.SubCategoryId
            }).FirstOrDefaultAsync();
        }
        public async Task<int> AddBook(AddBookDto addBookDto)
        {
            var addBook = new Book
            {
                Name = addBookDto.Name,
                Desc = addBookDto.Desc,
                SubCategoryId = addBookDto.SubCategoryId
            };
            await _libraryDbContext.Books.AddAsync(addBook);
            return await _libraryDbContext.SaveChangesAsync();

        }
        public async Task<int> UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            var currentBook = await _libraryDbContext.Books.FirstOrDefaultAsync(p => !p.isDeleted && p.Id == id);
            if (currentBook != null)
            {
                currentBook.Name = updateBookDto.Name;
                currentBook.SubCategoryId = updateBookDto.SubCategoryId;
                currentBook.Desc = updateBookDto.Desc;
                currentBook.MDate = DateTime.Now;
                _libraryDbContext.Books.Update(currentBook);
                return await _libraryDbContext.SaveChangesAsync();
            }
            return -1;
        }
        public async Task<int> DeleteBook(int id)
        {
            var currentBook = await _libraryDbContext.Books.FirstOrDefaultAsync(p => !p.isDeleted && p.Id == id);
            if (currentBook != null)
            {
                currentBook.isDeleted = true;
                return await _libraryDbContext.SaveChangesAsync();
            }
            return -1;
        }
    }
}
