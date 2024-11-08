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
    public class Education
    {
        public int Id { get; set; }

        [DisplayName("Start Education")]
        [DataType(DataType.Date)]
        public DateOnly StartEducation { get; set; }

        [DisplayName("End Education")]
        [DataType(DataType.Date)]
        public DateOnly EndEducation { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string Degree { get; set; }
        public string Description { get; set; }

        [DisplayName("Owner")]
        public string OwnerId { get; set; }
        [ValidateNever]
        public Owner Owner { get; set; }
    }
}
