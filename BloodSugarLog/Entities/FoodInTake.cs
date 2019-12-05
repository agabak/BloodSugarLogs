using System;

namespace BloodSugarLog.Entities
{
    public class FoodInTake
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int BloodValue { get; set; }
        public string Name { get; set; }
        public DateTime TakeTime { get; set; }
    }
}
