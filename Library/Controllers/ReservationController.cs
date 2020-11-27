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
using Library.Services;
using Library.ViewModel;

namespace Library.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [HttpGet]
        public async Task<ActionResult> ReservationDetails(int id)
        {
            List<ReservationViewModel> reservation = await _reservationService.GetReservationDetail(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);           
        }
        [HttpPost]
        public async Task<ActionResult> BookReservation(int id)
        {
            if (!_reservationService.AnyReservation(id))
            {
                await _reservationService.SetReservation(id, Int32.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
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
