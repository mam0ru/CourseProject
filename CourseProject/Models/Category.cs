using System;
using System.Collections.Generic;

namespace CourseProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        public String Text { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}