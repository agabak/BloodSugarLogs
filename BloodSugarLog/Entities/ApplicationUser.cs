using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodSugarLog.Entities
{
    public class ApplicationUser
    {
        public  string FirstName { get; set; }
        public string LastName { get; set; }
        public int FoodId { get; set; }
        public int BloodSugarId { get; set; }
    }
}
