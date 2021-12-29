using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlippiStats.Models
{
    public class Tournament
    {
        public int Id { get; set; }

        public int TournamentOrganizerId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
