using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BloodSugarLog.Models
{
    public class CreateCommandModel
    {
        [Required]
        [Display(Name = "Food")]
        public string FoodName { get; set; }
        [Required]
        [Display(Name ="Blood Value")]
        public int BloodValue { get; set; }
    }
}
