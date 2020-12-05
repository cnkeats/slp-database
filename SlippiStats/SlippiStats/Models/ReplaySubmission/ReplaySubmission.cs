using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlippiStats.Models.ReplaySubmission
{
    public class ReplaySubmission
    {
        [FromForm]
        public string Submitter { get; set; }

        [FromForm]
        public SlpReplay SlpReplay { get; set; }

        [FromForm]
        public IFormFile File { get; set; }
    }
}