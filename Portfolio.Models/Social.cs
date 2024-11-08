using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class Social
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }

        [DisplayName("Owner")]
        public string OwnerId { get; set; }
        [ValidateNever]
        public Owner Owner { get; set; }
    }
}
