using System;

namespace CourseProject.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public String Path { get; set; }

        public int TaskId { get; set; }

        public virtual Exercise Task { get; set; }

        public String Name { get; set; }
    }
}