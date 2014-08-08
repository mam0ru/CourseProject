using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;
using CourseProject.Repository.Interfaces;

namespace CourseProject.Repository.Implementation
{
    public class PictureRepository:GenericRepository<Picture>,IPictureRepository
    {
        public PictureRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}