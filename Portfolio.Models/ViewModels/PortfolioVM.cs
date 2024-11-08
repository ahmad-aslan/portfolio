using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models.ViewModels
{
    public class PortfolioVM
    {
        public Owner Owner { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Experience> Experiences { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Social> Socials { get; set; }
    }
}
