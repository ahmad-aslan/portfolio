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
    internal class EducationRepository : Repository<Education>, IEducationRepository
    {
        private readonly AppDbContext _db;
        public EducationRepository(AppDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Education education)
        {
            _db.Educations.Update(education);
        }
    }
}
