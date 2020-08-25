using DAL;
using HomeTrack.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeTrack.Controllers
{
    public class AuthController : Controller
    {
        /// <summary>
        /// Return login page
        /// </summary>
        public IActionResult Login(string returnUrl)
        {
            if (HttpContext.User?.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View(new AuthModel { ReturnUrl = returnUrl });
        }

        /// <summary>
        /// Check if user is valid and log user in
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login([FromServices] AppDbContextFactory factory, AuthModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var context = factory.CreateContext())
            {
                var IsUserValid = model.CheckUser(context, out var user);
                if (!IsUserValid)
                    ModelState.AddModelError("form", "Неправильный логин и/или пароль.");

                if (!ModelState.IsValid)
                    return View(model);

                await model.AuthAsync(HttpContext, user);
            }

            if (!String.IsNullOrWhiteSpace(model.ReturnUrl) && model.ReturnUrl != "/")
                return Redirect(model.ReturnUrl);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Log user out and redirect to login page
        /// </summary>
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.User = null;
            HttpContext.Session.Clear();
            Thread.CurrentPrincipal = null;

            return RedirectToAction("Login");
        }
    }
}