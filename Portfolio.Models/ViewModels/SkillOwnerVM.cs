using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models.ViewModels
{
    public class SkillOwnerVM
    {
        public Skill Skill { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> OwnerList { get; set; }
    }
}
