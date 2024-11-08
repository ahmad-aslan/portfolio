using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models.ViewModels
{
    public class SocialOwnerVM
    {
        public Social Social { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> OwnerList { get; set; }
    }
}
