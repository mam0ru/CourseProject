using System;

namespace CourseProject.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public virtual Exercise Task { get; set; }

        public String Text { get; set; }
    }
}