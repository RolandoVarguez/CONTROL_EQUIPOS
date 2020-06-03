using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlEquipos.Web.Models
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RolName { get; set; }

    }
}