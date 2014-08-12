using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.Models
{
    public class Evaluation
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public virtual Exercise Target { get; set; }

        public Boolean Type { get; set; }
    }
}