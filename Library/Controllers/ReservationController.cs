using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
            try
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
            catch (Exception)
            {
                TempData["Error"] = "Nie udało się otworzyć listy rezerwacji";
                return RedirectToAction("BookList", "Book");
            }
        }
        // GET: ReservationController/BookReservation/5
        public async Task<ActionResult> BookReservation(int id)
        {
            if (!context.Reservations.Any(a => a.BookId == id && a.ReservationDate == DateTime.Now.Date))
            {
                Reservation reservation = new Reservation();
                reservation.ReservationDate = DateTime.Now;
                reservation.UserId = 1;
                reservation.BookId = id;

                try
                {
                    context.Add(reservation);
                    await context.SaveChangesAsync();
                    ShowInformationAboutAction("Success", "Książka została zarezerwowana.");
                }
                catch (Exception)
                {
                    ShowInformationAboutAction("Error", "Nie jest możliwe dokonanie rezerwacji.");
                }
            }
            else
            {
                ShowInformationAboutAction("Error", "Książka została zarezerwowana przez innego użytkownika.");
            }
            return RedirectToAction("BookList", "Book");
        }

        private void ShowInformationAboutAction(string resultOfAction, string message)
        {
            TempData[$"{resultOfAction}"] = message;
        }
    }
}
