using System;

namespace CourseProject.Models
{
    public class Graph
    {
        public int Id { get; set; }

        public String Path { get; set; }

        public int TaskId { get; set; }

        public virtual Exercise Task { get; set; }
    }
}