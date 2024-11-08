using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Skill
    {
        public int Id { get; set; }
        [DisplayName("Skill Name")]
        public string SkillName { get; set; }
        [Range(1,100)]
        public int Rate { get; set; }

        [DisplayName("Owner")]
        public string OwnerId { get; set; }
        [ValidateNever]
        public Owner Owner { get; set; }
    }
}
