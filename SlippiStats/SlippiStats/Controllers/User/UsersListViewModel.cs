using SlippiStats.Models;
using System;
using System.Collections.Generic;

namespace SlippiStats.Controllers
{
    public class UsersListViewModel
    {
        public string Message { get; set; }

        public string UserName { get; set; }

        public List<Tuple<SlippiStats.Models.User, Role>> Users { get; set; }
    }
}