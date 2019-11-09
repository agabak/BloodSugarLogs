using BloodSugarLog.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodSugarLog.DL
{
    public class BloodSugarDbContext: IdentityUserContext<ApplicationUser>
    {
        public BloodSugarDbContext(DbContextOptions<BloodSugarDbContext> options): base(options)
        {}
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<BloodSugarMeasurement> BloodSugarMeasurements { get; set; }
        public DbSet<FoodInTake> FoodInTakes { get; set; }

    }
}
