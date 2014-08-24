using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CourseProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public double Rating{ get; set; }

        public String ImagePath { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Exercise> RightAnswers { get; set; }

        public virtual ICollection<Evaluation> Evaluations { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Evaluation> Evaluations { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Video> Videos { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Graph> Graphs { get; set; }

        public DbSet<Equation> Formulas { get; set; }

        public DbSet<Category> Categories { get; set; }
        
        public ApplicationDbContext()
            : base("DefaultConnection")//, throwIfV1Schema: false
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}