using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BloodSugarLog.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public  string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<BloodSugarMeasurement> BloodSugarMeasurements { get; set; }
        public virtual ICollection<FoodInTake> FoodInTakes { get; set; }
    }
}
