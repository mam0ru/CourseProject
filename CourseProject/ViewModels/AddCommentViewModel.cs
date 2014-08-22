using System;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.ViewModels
{
    public class AddCommentViewModel
    {
        public String UserName { get; set; }

        public String UserId { get; set; }

        public String ImagePath { get; set; }

        [Required]
        public String Text { get; set; }

        public int ExerciseId { get; set; }

    }
}