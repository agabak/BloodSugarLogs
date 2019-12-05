using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }
    }
}
