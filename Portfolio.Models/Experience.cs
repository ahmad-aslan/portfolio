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
    public class Experience
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateOnly StartJob { get; set; }

        [DataType(DataType.Date)]
        public DateOnly EndJob { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [DisplayName("Owner")]
        public string OwnerId { get; set; }
        [ValidateNever]
        public Owner Owner { get; set; }
    }
}
