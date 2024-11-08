using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portfolio.DataAccess.Data;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Portfolio.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
          RoleManager<IdentityRole> roleManager,
            AppDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initilaizer()
        {
            // migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            // create roles if the are not created
            if (!_roleManager.RoleExistsAsync(AppConstant.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(AppConstant.Role_Admin)).GetAwaiter().GetResult();

                // if roles are not created, then we will create admin as well
                _userManager.CreateAsync(new Owner
                {
                    UserName = "ahmedaslan12798@gmail.com",
                    Email = "ahmedaslan12798@gmail.com",                    
                    PhoneNumber = "+963-947681808",
                    Name = "Ahmad Aslan",
                    Address = "Damascus, Syria",
                    BirthDate = new DateOnly(1998,7,12),
                    Description = "Description",
                    JobTitle = "Backend Develober, .NET Develober",
                    ProfileImage = "#",
                    AboutImage = "#",

                }, "P@$$w0rd"
                ).GetAwaiter().GetResult();

                Owner user = _db.Owners.FirstOrDefault(u => u.Email == "ahmedaslan12798@gmail.com");
                _userManager.AddToRoleAsync(user, AppConstant.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
