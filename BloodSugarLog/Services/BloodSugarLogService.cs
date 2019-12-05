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

        public async Task<bool> Create(CreateCommandModel model)
        {
            var   appUser = await _userManager.FindByEmailAsync(model.Name);
            if(appUser == null)  return  false;

            var today = DateTime.Now;
            var foodInTake = new FoodInTake { Name = model.FoodName, ApplicationUserId = appUser.Id, BloodValue = model.BloodValue, TakeTime = today };

           await   _context.FoodInTakes.AddAsync(foodInTake);

            var isCread = await _context.SaveChangesAsync();
            if (isCread > -1) return true; 
            return false;
        }

        public async Task<List<BloodSugarHistoryDTO>> GetBloodLogs(string email)
        {
           var userId = await _userManager.FindByEmailAsync(email);
           var  foodInTake  = _context.FoodInTakes.Where(x => x.ApplicationUserId == userId.Id).ToList();

            var bloods = new List<BloodSugarHistoryDTO>();
            foreach(var food in foodInTake)
            {
                var blood = new BloodSugarHistoryDTO
                { FoodName = food.Name,
                   Value = food.BloodValue,
                   DateToday = food.TakeTime
  
                };
                bloods.Add(blood);
            }
           
            return bloods;
        }
    }
}

