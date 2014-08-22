using System;
using System.Collections.Generic;

namespace CourseProject.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public String Text { get; set; }

        public virtual ICollection<Exercise> Task { get; set; }
    }
}