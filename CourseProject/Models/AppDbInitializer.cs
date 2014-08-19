using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CourseProject.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        private readonly IApplicationUserRepository applicationUserRepository;

        public AppDbInitializer(IApplicationUserRepository applicationUserRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var current = applicationUserRepository.Get();
            if (current.Count() == 0)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var role1 = new IdentityRole {Name = "admin"};
                var role2 = new IdentityRole {Name = "user"};
                roleManager.Create(role1);
                roleManager.Create(role2);
                var admin = new ApplicationUser
                {
                    UserName = "administrator",
                    Email = "administrator@admin.com",
                    ImagePath = "~/Content/user.jpg"
                };
                string password = "123456789";
                var result = userManager.Create(admin, password);
                if (result.Succeeded)
                {
                    userManager.AddToRole(admin.Id, role1.Name);
                }
            }
            else
            {
            }
            base.Seed(context);
        }
    }
}