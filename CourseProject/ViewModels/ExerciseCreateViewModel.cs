﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.View_Models
{
    public class ExerciseCreateViewModel
    {
        [Required]
        public String Text { get; set; }
        
        [Required]
        public String Name { get; set; }
        
        public String Videos { get; set; }

        public String Pictures { get; set; }

        public String Formulas { get; set; }

        public String Graphs { get; set; }

        [Required]
        public String Tags { get; set; }

        public String Answers { get; set; }
        
        [Required]
        public String Category { get; set; }
    }
}