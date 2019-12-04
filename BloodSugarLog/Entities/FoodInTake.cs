using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodSugarLog.Entities
{
    public class FoodInTake
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string Name { get; set; }
    }
}
