using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodSugarLog.Models
{
    public class BloodSugarHistoryDTO
    {
        public string FoodName { get; set; }
        public int Value { get; set; }
        public DateTime DateToday { get; set; }
    }
}
