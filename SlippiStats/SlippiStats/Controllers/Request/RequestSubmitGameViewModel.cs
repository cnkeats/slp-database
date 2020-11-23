using Microsoft.AspNetCore.Mvc;
using SlippiStats.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Controllers.Request
{
    public class RequestSubmitGameViewModel
    {
        public string Message { get; set; }

        public DateTime Created { get; set; }

        public RequestSubmitGameViewModel([FromBody] SlpReplay game)
        {

        }
    }
}