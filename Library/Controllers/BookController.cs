using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services;
using Library.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult> BookList()
        {
            List<BookViewModel> bookList = await _bookService.GetAllBooksAsync();
            return View(bookList);
        }

        [HttpGet]
        public ActionResult CreateNewBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewBook(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBookAsync(bookViewModel);
                SetFlashMessage("Success", "Książka została dodana.");
                return RedirectToAction("BookList");
            }
            else
            {
                SetFlashMessage("Error", "Nie udało się stworzyć nowego elementu");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> EditBook(int id)
        {
            var bookViewModel = await _bookService.GetByIdAsync(id);

            if (bookViewModel == null)
            {
                return NotFound();
            }

            return View(bookViewModel);          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBook(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.UpdateBookAsync(book);
                SetFlashMessage("Success", "Książka została zaktualizowana.");
                return RedirectToAction("BookList");
            }
            else
            {
                SetFlashMessage("Error", "Edycja nie powiodła się");
            }
            return View();
        }

        private void SetFlashMessage(string messageType, string message)
        {
            TempData[$"{messageType}"] = message;
        }
    }
}
