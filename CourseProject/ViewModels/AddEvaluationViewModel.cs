using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseProject.ViewModels
{
    public class AddEvaluationViewModel
    {
        public int ExerciseId { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}