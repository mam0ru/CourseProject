using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public String Path { get; set; }

        public Exercise Task { get; set; }

        public String Name { get; set; }
    }
}