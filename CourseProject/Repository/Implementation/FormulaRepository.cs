using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CourseProject.Models;
using CourseProject.Repository.Interfaces;

namespace CourseProject.Repository.Implementation
{
    public class EquationRepository : GenericRepository<Equation>, IEquationRepository
    {
        public EquationRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}