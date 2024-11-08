using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {        
        IEducationRepository Education { get; }
        IExperienceRepository Experience { get; }
        IOwnerRepository Owner { get; }
        IProjectRepository Project { get; }
        IProjectImageRepository ProjectImage { get; }
        ISkillRepository Skill { get; }
        ISocialRepository Social { get; }

        void Save();
    }
}
