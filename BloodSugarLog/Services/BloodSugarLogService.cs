using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodSugarLog.Entities;
using BloodSugarLog.Models;
using Microsoft.AspNetCore.Identity;

namespace BloodSugarLog.Services
{
    public class BloodSugarLogService : IBloodSugarLogService
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public BloodSugarLogService(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;
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
            var isCreated = await _userManger.CreateAsync(storeUser, model.Password);

            if (isCreated.Succeeded)
            {
                await _signInManager.SignInAsync(storeUser, false, null);
                return true;
        
            }
            return false;
        }

        public async Task<bool> Login(LoginCommandModel model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);
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
    }
}
