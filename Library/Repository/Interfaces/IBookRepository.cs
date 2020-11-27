using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface IBookRepository
    {
        Task AddBookAsync(Book book);
        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> ListAllBookAsync();
        Task UpdateBookAsync(Book book);
    }
}