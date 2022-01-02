using Microsoft.AspNetCore.Http;
using SlippiStats.GameDataEnums;
using SlippiStats.Models;
using System.Collections.Generic;

namespace SlippiStats.Controllers
{
    public class TournamentUploadViewModel : SlippiStatsViewModel
    {
        public string TournamentName { get; set; }

        public List<IFormFile> Files { get; set; }

        public TournamentUploadViewModel() : base()
        {
            Files = new List<IFormFile>();
        }
    }
}