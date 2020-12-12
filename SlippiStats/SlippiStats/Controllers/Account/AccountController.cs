using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlippiStats.Authentication;
using SlippiStats.BusinessLogic;
using SlippiStats.Configuration;
using SlippiStats.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SlippiStats.Controllers
{
    public class AccountController : ApplicationController
    {
        private readonly AuthenticationBusinessLogic authenticationBusinessLogic;

        public AccountController(ApplicationSettings settings, ApplicationDatabase database)
            : base(settings, database)
        {
            authenticationBusinessLogic = new AuthenticationBusinessLogic(Database.Connection, Settings);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            AccountLoginViewModel viewModel = new AccountLoginViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AccountLoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            User user = authenticationBusinessLogic.GetUserByCredentials(viewModel.UserName, viewModel.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Credentials are incorrect.");

                return View(viewModel);
            }

            UserSession userSession = authenticationBusinessLogic.GetLatestUserSessionAndExtend(user.Id);
            ClaimsPrincipal claimsPrincipal = authenticationBusinessLogic.CreateClaimsPrincipal(userSession);
            AuthenticationProperties authenticationProperties = authenticationBusinessLogic.CreateAuthenticationProperties(userSession, viewModel.IsPersistent);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            if (viewModel.ReturnUrl != null)
            {
                return Redirect(viewModel.ReturnUrl);
            }

            return RedirectToAction(nameof(GameController.List), "Game");
        }

        public IActionResult Edit()
        {
            AccountEditViewModel viewModel = new AccountEditViewModel
            {
                Message = (string)TempData["Message"]
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(AccountEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (authenticationBusinessLogic.VerifyPassword(ApplicationUser.Password, viewModel.OldPassword))
            {
                ModelState.AddModelError(nameof(AccountEditViewModel.OldPassword), "Old password is incorrect.");

                return View(viewModel);
            }

            ApplicationUser.Password = authenticationBusinessLogic.HashPassword(viewModel.NewPassword);
            ApplicationUser.Save(Database.Connection);

            TempData["Message"] = "Account saved successfully.";

            return RedirectToAction(nameof(Edit));
        }

        public async Task<IActionResult> Logout()
        {
            Guid sessionId = new Guid(User.Claims.First(c => c.Type == ApplicationClaims.SessionId).Value);
            UserSession userSession = UserSession.GetById(Database.Connection, sessionId);
            userSession.IsDeleted = true;
            userSession.Save(Database.Connection);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}