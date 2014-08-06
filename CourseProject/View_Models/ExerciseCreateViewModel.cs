using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;

namespace CourseProject.View_Models
{
    public class ExerciseCreateViewModel
    {
        public String Text { get; set; }

        public String Name { get; set; }
        
        public ICollection<Video> Videos { get; set; }

        public ICollection<Picture> Pictures { get; set; }

        public ICollection<Formula> Formulas { get; set; }

        public ICollection<Graph> Graphs { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public String Category { get; set; }
    }
}