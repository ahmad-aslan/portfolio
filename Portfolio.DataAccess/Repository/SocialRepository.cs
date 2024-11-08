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
    public class SocialRepository : Repository<Social>, ISocialRepository
    {
        private readonly AppDbContext _db;
        public SocialRepository(AppDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Social social)
        {
            _db.Socials.Update(social);
        }
    }
}
