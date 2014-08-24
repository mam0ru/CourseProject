using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CourseProject.Models
{
    public class Evaluation
    {
        public int Id { get; set; }

        [Required]
        public String UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int TargetId { get; set; }

        public virtual Exercise Target { get; set; }

        public Boolean Type { get; set; }
    }
}