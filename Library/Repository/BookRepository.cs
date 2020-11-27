using Library.Models;
using Library.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository
{

    public class BookRepository : IBookRepository
    {
        private readonly LibraryDBContext _dbContext;

        public BookRepository(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Book>> ListAllBookAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }
        public async Task AddBookAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Book> GetByIdAsync(int id)
        {
            return await _dbContext.Books.FindAsync(id);
        }
        public async Task UpdateBookAsync(Book book)
        {
            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
