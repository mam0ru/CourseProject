using CourseProject.Models;

namespace CourseProject.Repository.Implementation
{
    public class EvaluationRepository:GenericRepository<Evaluation>,IEvaluationRepository
    {
        public EvaluationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}