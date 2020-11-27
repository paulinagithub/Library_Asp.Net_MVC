using Library.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IBookService
    {
        Task AddBookAsync(BookViewModel bookViewModel);
        Task<List<BookViewModel>> GetAllBooksAsync();
        Task<BookViewModel> GetByIdAsync(int id);
        Task UpdateBookAsync(BookViewModel bookViewModel);
    }
}