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
    public class AppDbInitializer: DropCreateDatabaseAlways<ApplicationDbContext>
    {
        private readonly IApplicationUserRepository applicationUserRepository;

        public AppDbInitializer(IApplicationUserRepository applicationUserRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
 
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
 
            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
 
            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
 
            // создаем пользователей
            var admin = new ApplicationUser { UserName = "Administrator", Email = "administrator@admin.com" };
            string password = "123456789";
            var result = userManager.Create(admin, password);
 
            // если создание пользователя прошло успешно
            if(result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
            }
 
            base.Seed(context);
            applicationUserRepository.Insert(admin);
        }
    }
}