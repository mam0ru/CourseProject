using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public Exercise Task { get; set; }

        public String Text { get; set; }
    }
}