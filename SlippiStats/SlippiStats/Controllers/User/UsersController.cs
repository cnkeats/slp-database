﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlippiStats.Authentication;
using SlippiStats.BusinessLogic;
using SlippiStats.Configuration;
using SlippiStats.Models;
using System;
using System.Collections.Generic;

namespace SlippiStats.Controllers
{
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class UsersController : ApplicationController
    {
        private readonly AuthenticationBusinessLogic authenticationBusinessLogic;

        public UsersController(ApplicationSettings settings, ApplicationDatabase database)
            : base(settings, database)
        {
            authenticationBusinessLogic = new AuthenticationBusinessLogic(Database.Connection, Settings);
        }

        [Route("/{username}")]
        public IActionResult Profile(string username)
        {
            UsersProfileViewModel viewModel = new UsersProfileViewModel();
            viewModel.User = Models.User.GetByUserName(Database.Connection, username);

            if (viewModel.User == null)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(viewModel);
        }

        public IActionResult List()
        {
            UsersListViewModel viewModel = new UsersListViewModel();
            viewModel.Message = (string)TempData["Message"];
            viewModel.Users = new List<Tuple<User, Role>>();

            List<User> users = Models.User.GetList(Database.Connection);
            foreach (User user in users)
            {
                Role role = Role.GetByUserId(Database.Connection, user.Id);
                viewModel.Users.Add(new Tuple<User, Role>(user, role));
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult List(UsersListViewModel viewModel)
        {
            viewModel.Message = (string)TempData["Message"];
            viewModel.Users = new List<Tuple<User, Role>>();

            List<User> users = Models.User.GetList(Database.Connection);
            foreach (User user in users)
            {
                Role role = Role.GetByUserId(Database.Connection, user.Id);
                viewModel.Users.Add(new Tuple<User, Role>(user, role));
            }

            return View(viewModel);
        }

        public IActionResult Create()
        {
            UsersCreateViewModel viewModel = new UsersCreateViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(UsersCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (Models.User.IsUserNameInUse(Database.Connection, viewModel.UserName))
            {
                ModelState.AddModelError(nameof(UsersCreateViewModel.UserName), "Username is already in use.");

                return View(viewModel);
            }

            string hashedPassword = authenticationBusinessLogic.HashPassword(viewModel.Password);
            User user = new User(viewModel.UserName, hashedPassword);
            user.Save(Database.Connection);

            TempData["Message"] = "User saved successfully.";

            return RedirectToAction(nameof(Edit), new { id = user.Id });
        }

        public IActionResult Edit(int id)
        {
            User user = Models.User.GetById(Database.Connection, id);
            if (user == null || user.IsDeleted)
            {
                return NotFound();
            }

            UsersEditViewModel viewModel = new UsersEditViewModel
            {
                Message = (string)TempData["Message"],
                CanDelete = user.Id != ApplicationUser.Id,
                Id = user.Id,
                UserName = user.UserName
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(UsersEditViewModel viewModel)
        {
            viewModel.CanDelete = viewModel.Id != ApplicationUser.Id;

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            User user = Models.User.GetById(Database.Connection, viewModel.Id);
            if (user == null || user.IsDeleted)
            {
                return NotFound();
            }

            if (Models.User.IsUserNameInUse(Database.Connection, viewModel.UserName, viewModel.Id))
            {
                ModelState.AddModelError(nameof(UsersEditViewModel.UserName), "Username is already in use.");

                return View(viewModel);
            }

            if (!string.IsNullOrWhiteSpace(viewModel.NewPassword))
            {
                user.Password = authenticationBusinessLogic.HashPassword(viewModel.NewPassword);
            }

            user.UserName = viewModel.UserName;
            user.Save(Database.Connection);

            TempData["Message"] = "User saved successfully.";

            return RedirectToAction(nameof(Edit), new { id = user.Id });
        }

        public IActionResult Delete(int id)
        {
            User user = Models.User.GetById(Database.Connection, id);

            if (user == null || user.IsDeleted)
            {
                return NotFound();
            }

            user.IsDeleted = true;
            user.Save(Database.Connection);

            TempData["Message"] = "User deleted successfully.";

            return RedirectToAction(nameof(List));
        }
    }
}