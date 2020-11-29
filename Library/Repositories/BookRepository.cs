using Library.Models;
using Library.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDBContext _dbContext;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(LibraryDBContext dbContext, ILogger<BookRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<Book>> ListAllBookAsync()
        {
            return await _dbContext.Books.ToListAsync();           
        }
        public async Task AddBookAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError($"Adding book failed: {ex.Message}");
                throw ex;
            }
        }
        public async Task<Book> GetByIdAsync(int id)
        {
            return await _dbContext.Books.FindAsync(id);           
        }
        public async Task UpdateBookAsync(Book book)
        {
            _dbContext.Books.Update(book);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Adding book failed: {ex.Message}");
                throw ex;
            }
        }
    }
}
