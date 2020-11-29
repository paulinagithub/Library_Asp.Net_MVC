using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryDBContext _dbContext;
        private readonly ILogger<ReservationRepository> _logger;

        public ReservationRepository(LibraryDBContext dbContext, ILogger<ReservationRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<List<Reservation>> ListAllReservationAsync(int bookID)
        {
            try
            {
                return await _dbContext.Books
                     .Join(_dbContext.Reservations,
                          book => book,
                          reservation => reservation.Book,
                          (book, reservation) =>
                          new Reservation
                          {
                              BookId = reservation.BookId,
                              ReservationDate = reservation.ReservationDate,
                              Book = book
                          })
                     .Where(w => w.BookId == bookID)
                     .ToListAsync();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError($"Listing reservation failed: {ex.Message}");
                throw ex;
            }
        }
        public bool AnyReservation(int bookID)
        {
            return _dbContext.Reservations.Any(a => a.BookId == bookID && a.ReservationDate == DateTime.Now.Date);
        }
        public async Task SetReservationAsync(Reservation reservation)
        {          
            await _dbContext.AddAsync(reservation);
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Adding reservation failed: {ex.Message}");
                throw ex;
            }
        }
    }
}
