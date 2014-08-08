using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;

namespace CourseProject.Repository.Implementation
{
    public class CommentRepository: GenericRepository<Comment>,ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}