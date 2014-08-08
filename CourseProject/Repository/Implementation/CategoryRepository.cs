using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;

namespace CourseProject.Repository.Implementation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}