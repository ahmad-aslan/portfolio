using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models
{
    public class Owner : IdentityUser
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [DisplayName("Profile Image")]
        public string ProfileImage { get; set; }
        [DisplayName("About Image")]
        public string AboutImage { get; set; }


        [ValidateNever]
        public IEnumerable<Education> Educations { get; set; }
        [ValidateNever]
        public IEnumerable<Experience> Experiences { get; set; }
        [ValidateNever]
        public IEnumerable<Project> Projects { get; set; }
        [ValidateNever]
        public IEnumerable<Skill> Skills { get; set; }
        [ValidateNever]
        public IEnumerable<Social> Socials { get; set; }

    }
}
