using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace med_project_finalized.Models
{
    public class GroupedUserViewModel
    {
        public List<UserViewModel> Pacient { get; set; }
        public List<UserViewModel> Doctor { get; set; }
    }

    public class UserViewModel
    {
        public string Password { get; set; }
        public string Username { get; set; }
        public string Roles { get; set; }
    }
}