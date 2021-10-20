using System.Collections.Generic;
using System.Threading.Tasks;
using Library.DAL.Dtos;

namespace Library.Business.Abstract
{
    public interface IBookService
    {
        public Task<List<GetListBookDto>> GetBookList();
        public Task<GetBookDto> GetBookById(int id);
        public Task<int> AddBook(AddBookDto addBookDto);
        public Task<int> UpdateBook(int id, UpdateBookDto updateBookDto);
        public Task<int> DeleteBook(int id);
    }
}
