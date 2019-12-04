using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BloodSugarLog.DL;
using BloodSugarLog.Entities;
using BloodSugarLog.Models;
using Microsoft.AspNetCore.Identity;

namespace BloodSugarLog.Services
{
    public class BloodSugarLogService : IBloodSugarLogService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BloodSugarDbContext _context;


        public BloodSugarLogService(UserManager<ApplicationUser> userManger, 
                                    SignInManager<ApplicationUser> signInManager, 
                                    BloodSugarDbContext context)
        {
            _userManager = userManger;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<bool> Register(RegisterCommandModel model)
        {
            var storeUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            var isCreated = await _userManager.CreateAsync(storeUser, model.Password);

            if (isCreated.Succeeded)
            {
                     await _signInManager.SignInAsync(storeUser, false, null);

                    var claims = new List<Claim>
                    {
                        new Claim("FullName", model.FirstName + ", " + model.LastName),
            
                    };
                    await _userManager.AddClaimsAsync(storeUser, claims);
             
                return true;
            }
            return false;
        }

        public async Task<bool> Login(LoginCommandModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return false;

            var isLogin = await  _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (isLogin.Succeeded) return true;
            return false;
        }

        public async Task<bool>  Logout()
        {
           await  _signInManager.SignOutAsync();
            return true;
        }

        public async Task<bool> Create(CreateCommandModel model,string email)
        {
            var   appUser = await _userManager.FindByEmailAsync(email);
            if(appUser == null)  return  false;

            var foodInTake = new FoodInTake { Name = model.FoodName, ApplicationUserId = appUser.Id};
            var bloodValues = new BloodSugarMeasurement  { MeasurementValue = model.BloodValue, ApplicationUserId = appUser.Id}; 

           await   _context.BloodSugarMeasurements.AddAsync(bloodValues);
           await   _context.FoodInTakes.AddAsync(foodInTake);

            var isCread = await _context.SaveChangesAsync();
        
            return true;
        }

        public async Task<List<BloodSugarHistoryDTO>> GetBloodLogs(string email)
        {
           var userId = await _userManager.FindByEmailAsync(email);
           var  foodInTake  = _context.FoodInTakes.Where(x => x.ApplicationUserId == userId.Id).ToList();
           var  bloodValue = _context.BloodSugarMeasurements.Where(x => x.ApplicationUserId == userId.Id).ToList();

            var bloods = new List<BloodSugarHistoryDTO>();
             var blood = new BloodSugarHistoryDTO();
            foreach(var food in foodInTake)
            {
                 blood.FoodName = food.Name;

                bloods.Add(blood);
            }

            foreach(var val in bloodValue)
            {
                blood.Value = val.MeasurementValue;
                bloods.Add(blood);
            }

            return bloods;
        }
    }
}
