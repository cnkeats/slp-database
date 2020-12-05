using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SlippiStats.Configuration;
using SlippiStats.Models;
using SlippiStats.Models.ReplaySubmission;
using SlippiStats.ViewModels;
using System;
using System.Diagnostics;

namespace SlippiStats.Controllers
{
    public class UploadController : ApplicationController
    {
        public UploadController(ApplicationSettings settings, ApplicationDatabase database) : base(settings, database)
        {

        }
       
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public GameSubmitResponse SubmitGame([FromBody] SlpReplay slpReplay)
        {
            GameSubmitResponse response = new GameSubmitResponse();

            return response;
        }

        [HttpPost]
        public GameSubmitResponse SubmitReplayFile([FromForm] ReplaySubmission replaySubmission, [FromForm] string Testing)
        {

            GameSubmitResponse response = new GameSubmitResponse();
            response.Message = String.Format("Data recieved from {0}.", replaySubmission?.Submitter);
            response.Success = true;

            return response;
        }
    }
}