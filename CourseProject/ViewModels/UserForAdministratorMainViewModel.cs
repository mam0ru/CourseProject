using System;
using System.Collections.Generic;
using CourseProject.Models;

namespace CourseProject.View_Models
{
    public class UserForAdministratorMainViewModel
    {
        public String Name { get; set; }

        public String Id { get; set; }

        public String Email { get; set; }

        public IEnumerable<Exercise> UsersExercises { get; set; }

        public IEnumerable<Exercise> SolvedExercises { get; set; }

        public Boolean Admin { get; set; }

        public Boolean DroppedPassword { get; set; }

        public Boolean Blocked { get; set; }

        public Boolean Deleted { get; set; }
    }
}