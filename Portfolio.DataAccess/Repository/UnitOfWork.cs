using Portfolio.DataAccess.Data;
using Portfolio.DataAccess.Repository.IRepository;

namespace Portfolio.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        
        public IEducationRepository Education { get; private set; }
        public IExperienceRepository Experience { get; private set; }
        public IOwnerRepository Owner { get; private set; }
        public IProjectRepository Project { get; private set; }
        public IProjectImageRepository ProjectImage { get; private set; }
        public ISkillRepository Skill { get; private set; }
        public ISocialRepository Social { get; private set; }

        public UnitOfWork(AppDbContext db)
        {
            _db = db;            
            Education = new EducationRepository(_db);
            Experience = new ExperienceRepository(_db);
            Owner = new OwnerRepository(_db);
            Project = new ProjectRepository(_db);
            ProjectImage = new ProjectImageRepository(_db);
            Skill = new SkillRepository(_db);
            Social = new SocialRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
