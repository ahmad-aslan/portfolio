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
    public class ProjectImageRepository : Repository<ProjectImage>, IProjectImageRepository
    {
        private readonly AppDbContext _db;

        public ProjectImageRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProjectImage projectImage)
        {
            _db.ProjectImages.Update(projectImage);
        }
    }
}
