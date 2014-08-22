using CourseProject.Models;
using CourseProject.Repository.Interfaces;

namespace CourseProject.Repository.Implementation
{
    public class TagRepository:GenericRepository<Tag>,ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}