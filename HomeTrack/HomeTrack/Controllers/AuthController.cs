﻿using DAL;
using DAL.Entities;
using HomeTrack.Helpers;
using HomeTrack.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeTrack.Controllers
{
    public class AuthController : Controller
    {
        private readonly IStringLocalizer _localizer;
        public AuthController(IStringLocalizer<AuthController> localizer)
        {
            _localizer = localizer;
        }
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
                    ModelState.AddModelError("form", _localizer["AuthController_LoginOrPasswordNotValid"]);

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

        /// <summary>
        /// Return registration page
        /// </summary>
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        /// <summary>
        /// Register user
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register([FromServices] AppDbContextFactory factory, RegisterModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("RegisterFormContent", model);

            using (var context = factory.CreateContext())
            {
                var phoneExists = model.CheckPhone(context);
                if (phoneExists)
                    ModelState.AddModelError("form", _localizer["AuthController_UserWithPhoneExists"]);

                var emailExists = model.CheckEmail(context);
                if (emailExists)
                    ModelState.AddModelError("form", _localizer["AuthController_UserWithEmailExists"]);

                if (!ModelState.IsValid)
                    return PartialView("RegisterFormContent", model);

                var entity = new UserEntity();
                entity.Name = model.Name;
                entity.Email = model.Email;
                entity.Phone = model.Phone;
                entity.PasswordHash = model.Password.ComputeHashMD5();

                context.Users.Add(entity);
                await context.SaveChangesAsync();
            }
            ViewBag.RedirectUrl = Url.Action("Login", "Auth");
            return PartialView("RegisterFormContent");
        }
    }
}