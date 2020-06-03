using ControlEquipos.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlEquipos.Web.Clase
{
    public class Utilities
    {
        readonly static ApplicationDbContext db = new ApplicationDbContext();

        public static void CheckRoles(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        internal static void CheckSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userAsp = userManager.FindByName("administrador@admin.com");

            if (userAsp == null)
            {
                CreateUserASP("Rolando Varguez","rolando30","administrador@admin.com", "Admin123", "Admin");
            }
        }

        internal static void CheckClientDefault()
        {
            var clientdb = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userclient = clientdb.FindByName("team_owner@owner.com");
            if (userclient == null)
            {
                CreateUserASP("Gerardo Leon","gerryligas","team_owner@owner.com", "TeamOwner123", "Owner");
                userclient = clientdb.FindByName("team_owner@owner.com");
                var owner = new Owner
                {
                    UserId = userclient.Id,
                };

                db.Owners.Add(owner);
                db.SaveChanges();
            }
        }

        public static void CreateUserASP(string name, string username, string email, string password, string rol)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userASP = new ApplicationUser()
            {
                UserName = email,
                Email = email,
            };

            userManager.Create(userASP, password);
            userManager.AddToRole(userASP.Id, rol);
        }




        public void Dispose()
        {
            db.Dispose();
        }
    }
}