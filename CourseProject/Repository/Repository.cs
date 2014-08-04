using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CourseProject.Models;
using Ninject;

namespace CourseProject.Repository
{
    public class Repository
    {
        public interface IRepositoriable
        {
            IEnumerable<Exercise> GetExercises();

            IEnumerable<Comment> GetComments();

            Exercise GetById(int id);

            void Insert(Exercise exercise);

            void Delete(int id);

            void Update(Exercise exercise);
        }

        public class ProjectDAO : IRepositoriable
        {
            private ApplicationDbContext context = MvcApplication.appKernel.Get<ApplicationDbContext>();

            public IEnumerable<Exercise> GetExercises()
            {
                return context.Exercises.Select(exercise => exercise);
            }

            public IEnumerable<Comment> GetComments()
            {
                return context.Comments.Select(comment => comment);
            }

            public Exercise GetById(int id)
            {
                return context.Exercises.Where(exercise => exercise.Id == id).FirstOrDefault();
            }

            public void Insert(Exercise exercise)
            {
                context.Exercises.Add(exercise);
                context.Entry(exercise).State = EntityState.Added;
                context.SaveChangesAsync();
            }

            public void Delete(int id)
            {
                Exercise exercise = GetById(id);
                context.Exercises.Remove(exercise);
                context.Entry(exercise).State = EntityState.Deleted;
                context.SaveChangesAsync();
            }

            public void Update(Exercise exercise)
            {
                context.Entry(exercise).State = EntityState.Modified;
                context.SaveChangesAsync();
            }

        }
    }
}