using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly LibraryDBContext context;

        public BookController(LibraryDBContext context)
        {
            this.context = context;
        }
        // GET: BookController
        public async Task<ActionResult> BookList()
        {
            List<Book> bookList = await context.Books.ToListAsync();
            return View(bookList);
        }

        // GET: BookController/Create
        public ActionResult CreateNewBook()
        {
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateNewBook(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Add(book);
                    await context.SaveChangesAsync();

                    ShowInformationAboutAction("Success", "Książka została dodana.");
                    return RedirectToAction("BookList");
                }
            }
            catch(Exception)
            {
                ShowInformationAboutAction("Error", "Nie udało się stworzyć nowego elementu");
                return RedirectToAction("BookList");
            }

            return View();
        }

        // GET: BookController/Edit/5
        public async Task<ActionResult> EditBook(int id)
        { 
            try
            {
                Book book = await context.Books.Where(w => w.BookId == id).FirstAsync();

                if (book == null)
                {
                    return NotFound();
                }

                return View(book);
            } 
            catch(Exception)
            {
                ShowInformationAboutAction("Error", "Wystąpił błąd w trakcie pobierania książki do edycji.");
                return RedirectToAction("BookList");
            }
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBook(Book book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Update(book);
                    await context.SaveChangesAsync();

                    ShowInformationAboutAction("Success", "Książka została zaktualizowana.");
                }              
            }
            catch(Exception)
            {
                ShowInformationAboutAction("Error", "Wystąpił błąd w trakcie edycji elementu.");
            }
            return RedirectToAction("BookList");
        }

        private void ShowInformationAboutAction(string resultOfAction, string message)
        {
            TempData[$"{resultOfAction}"] = message;
        }
    }
}
