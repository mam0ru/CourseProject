using CourseProject.Models;

namespace CourseProject.Repository.Implementation
{
    public class ExerciseRepository:GenericRepository<Exercise>,IExerciseRepository
    {
        public ExerciseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}