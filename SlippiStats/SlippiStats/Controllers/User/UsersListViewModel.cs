using SlippiStats.Models;
using System;
using System.Collections.Generic;

namespace SlippiStats.Controllers
{
    public class UsersProfileViewModel : SlippiStatsViewModel
    {
        public User User { get; set; }

        public UsersProfileViewModel()
        {

        }
    }
}