using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class Equation
    {
        public int Id { get; set; }

        public String Path { get; set; }

        public virtual Exercise Task { get; set; }
    }
}