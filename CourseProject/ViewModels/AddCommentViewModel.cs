using System;
using CourseProject.Models;

namespace CourseProject.ViewModels
{
    public class AddCommentViewModel
    {
        public String UserName { get; set; }

        public String UserId { get; set; }

        public String ImagePath { get; set; }

        public String Text { get; set; }

        public int ExerciseId { get; set; }

    }
}