using Portfolio.DataAccess.Data;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly AppDbContext _db;

        public ProjectRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Project project)
        {
            _db.projects.Update(project);
        }
    }
}
