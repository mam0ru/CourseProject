using CourseProject.Models;

namespace CourseProject.Repository
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}