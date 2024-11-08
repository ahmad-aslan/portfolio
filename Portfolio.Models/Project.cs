using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        [DisplayName("Project Link")]
        public string ProjectLink { get; set; }

        [DisplayName("Owner")]
        public string OwnerId { get; set; }
        [ValidateNever]
        public Owner Owner { get; set; }

        [ValidateNever]
        public List<ProjectImage> ProjectImages { get; set; }

    }
}
