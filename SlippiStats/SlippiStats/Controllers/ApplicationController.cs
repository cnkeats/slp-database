﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SlippiStats.Authentication;
using SlippiStats.Configuration;
using System;

namespace SlippiStats.Controllers
{
    public class ApplicationController : Controller
    {
        protected ApplicationController(ApplicationSettings settings, ApplicationDatabase database)
        {
            Settings = settings;
            Database = database;
        }

        protected ApplicationSettings Settings { get; private set; }

        protected ApplicationDatabase Database { get; private set; }

        protected TimeZoneInfo UserTimeZone { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //UserTimeZone = TimeZoneBusinessLogic.GetTimeZoneFromRequest(Request, Settings.DefaultTimeZone);
        }
    }
}