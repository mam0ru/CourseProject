using System.Linq;
using CourseProject.Repository;
using CourseProject.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CourseProject.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        private readonly IApplicationUserRepository applicationUserRepository;

        private readonly ICategoryRepository categoryRepository;

        public AppDbInitializer(IApplicationUserRepository applicationUserRepository, ICategoryRepository categoryRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
            this.categoryRepository = categoryRepository;
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
                    ImagePath = "http://localhost:50048/Content/user.jpg"
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
            categoryRepository.Insert(new Category() { Text = "Culture" });
            categoryRepository.Insert(new Category() { Text = "Math" });
            categoryRepository.Insert(new Category() { Text = "Art" });
            categoryRepository.Insert(new Category() { Text = "Physics" });
            categoryRepository.Insert(new Category() { Text = "People" });
            categoryRepository.Insert(new Category() { Text = "World" });
            categoryRepository.Insert(new Category() { Text = "Science" });
            base.Seed(context);
        }
    }
}