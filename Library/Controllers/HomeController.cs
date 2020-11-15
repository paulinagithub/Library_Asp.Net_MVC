using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        public const int UserID = 1;
        public const string LoginName = "Admin";
        public const string Passward = "Admin";
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user, string ReturnUrl)
        {
            if ((user.Login == LoginName) && (user.Passward == Passward))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, UserID.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/Book/BookList" : ReturnUrl);
            }
            else
                return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }
    }
}
