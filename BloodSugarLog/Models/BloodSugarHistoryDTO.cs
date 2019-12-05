using System;

namespace BloodSugarLog.Models
{
    public class BloodSugarHistoryDTO
    {
        public string FoodName { get; set; }
        public int Value { get; set; }
        public DateTime DateToday { get; set; }
    }
}
