using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.Models;
using Library.Services;
using Library.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly ILoginService _loginService;

        public HomeController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel user, string ReturnUrl)
        {
            if(ModelState.IsValid)
            {

                var userID = _loginService.GetUserID(user.Login, user.Password);
                if (userID != 0)
                {
                    var claimsIdentity = _loginService.CreateClaimsIdentity(userID);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return Redirect(ReturnUrl == null ? "/Book/BookList" : ReturnUrl);
                }
                else
                {
                    SetFlashMessage("Error", "Login lub hasło jest błędne.");
                }
            }
            return View();

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }

        private void SetFlashMessage(string resultOfAction, string message)
        {
            TempData[$"{resultOfAction}"] = message;
        }
    }
}
