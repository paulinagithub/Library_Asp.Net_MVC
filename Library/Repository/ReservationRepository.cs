using Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryDBContext _dbContext;

        public ReservationRepository(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Reservation>> ListAllReservationAsync(int bookID)
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
        public bool AnyReservation(int bookID)
        {
            return _dbContext.Reservations.Any(a => a.BookId == bookID && a.ReservationDate == DateTime.Now.Date);
        }
        public async Task SetReservation(Reservation reservation)
        {          
            await _dbContext.AddAsync(reservation);
            await _dbContext.SaveChangesAsync();
        }
    }
}
