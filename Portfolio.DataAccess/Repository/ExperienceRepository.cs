using Portfolio.DataAccess.Data;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Repository
{
    internal class ExperienceRepository : Repository<Experience>, IExperienceRepository
    {
        private readonly AppDbContext _db;
        public ExperienceRepository(AppDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Experience experience)
        {
            _db.Experiences.Update(experience);
        }
    }
}
