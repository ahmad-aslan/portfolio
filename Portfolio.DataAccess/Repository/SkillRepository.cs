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
    internal class SkillRepository : Repository<Skill>, ISkillRepository
    {
        private readonly AppDbContext _db;
        public SkillRepository(AppDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Skill skill)
        {
            _db.Skills.Update(skill);
        }
    }
}
