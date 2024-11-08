using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class ProjectImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public int ProjectId { get; set; }
        [ValidateNever]
        public Project Project { get; set; }
    }
}
