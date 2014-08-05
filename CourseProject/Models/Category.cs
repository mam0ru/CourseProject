using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        public String Text { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}