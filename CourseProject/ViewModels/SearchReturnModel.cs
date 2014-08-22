using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;

namespace CourseProject.ViewModels
{
    public class SearchReturnModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }

        public IEnumerable<Exercise> Exercises { get; set; }
    }
}