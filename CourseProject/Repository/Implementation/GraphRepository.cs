using CourseProject.Models;
using CourseProject.Repository.Interfaces;

namespace CourseProject.Repository.Implementation
{
    public class GraphRepository:GenericRepository<Graph>,IGraphRepository
    {
        public GraphRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}