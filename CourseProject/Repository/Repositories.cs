using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;

namespace CourseProject.Repository
{
    public class Repositories: IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private GenericRepository<Exercise> exerciseRepository;

        private GenericRepository<Answer> answerRepository;

        private GenericRepository<Comment> commentRepository;

        private GenericRepository<ApplicationUser> userRepository;

        private GenericRepository<Category> categoryRepository;

        private GenericRepository<Picture> pictureRepository;

        public GenericRepository<Exercise> ExerciseRepository {
            get
            {
                if (this.exerciseRepository == null)
                {
                    this.exerciseRepository = new GenericRepository<Exercise>(context);
                }
                return exerciseRepository;
            }
        }

        public GenericRepository<Picture> PictureRepository
        {
            get
            {
                if (this.pictureRepository == null)
                {
                    this.pictureRepository = new GenericRepository<Picture>(context);
                }
                return pictureRepository;
            }
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(context);
                }
                return categoryRepository;
            }
        }

        public GenericRepository<Comment> CommentRepository
        {
            get
            {
                if (this.commentRepository == null)
                {
                    this.commentRepository = new GenericRepository<Comment>(context);
                }
                return commentRepository;
            }
        }

        public GenericRepository<Answer> AnswerRepository
        {
            get
            {
                if (this.answerRepository == null)
                {
                    this.answerRepository = new GenericRepository<Answer>(context);
                }
                return answerRepository;
            }
        }

        public GenericRepository<ApplicationUser> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<ApplicationUser>(context);
                }
                return userRepository;
            }
        }

        private Boolean disposed = false;

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}