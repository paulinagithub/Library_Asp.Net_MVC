using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Library.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly LibraryDBContext context;

        public ReservationController(LibraryDBContext context)
        {
            this.context = context;
        }

        // GET: ReservationController/ReservationDetails/5
        public async Task<ActionResult> ReservationDetails(int id)
        {           
            List<Reservation> reservation = await context.Books
                .Join(context.Reservations,
                        book => book,
                        reservation => reservation.Book,
                        (book, reservation) =>
                        new Reservation
                        {
                            BookId = reservation.BookId,
                            ReservationDate = reservation.ReservationDate,
                            Book = book
                        })
                .Where(w => w.BookId == id)
                .ToListAsync();

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);           
        }
        // GET: ReservationController/BookReservation/5
        public async Task<ActionResult> BookReservation(int id)
        {
            if (!context.Reservations.Any(a => a.BookId == id && a.ReservationDate == DateTime.Now.Date))
            {
                Reservation reservation = new Reservation();
                reservation.ReservationDate = DateTime.Now;
                reservation.UserId = Int32.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                reservation.BookId = id;
              
                context.Add(reservation);
                await context.SaveChangesAsync();
                SetFlashMessage("Success", "Książka została zarezerwowana.");              
            }
            else
            {
                SetFlashMessage("Error", "Książka została zarezerwowana przez innego użytkownika.");
            }
            return RedirectToAction("BookList", "Book");
        }

        private void SetFlashMessage(string resultOfAction, string message)
        {
            TempData[$"{resultOfAction}"] = message;
        }
    }
}
