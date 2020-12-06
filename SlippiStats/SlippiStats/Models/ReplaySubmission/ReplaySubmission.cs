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
        public IFormFile File { get; set; }

        [FromForm]
        public int GameId { get; set; }

        [FromForm]
        public int UploaderId { get; set; }
    }
}