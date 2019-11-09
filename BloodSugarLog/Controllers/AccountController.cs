using BloodSugarLog.Entities;
using BloodSugarLog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodSugarLog.Controllers
{
    public class AccountController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManger, 
                                 SignInManager<ApplicationUser> signInManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;

        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Register(RegisterCommandModel model)
        {
            if (!ModelState.IsValid) return View(model);
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
                return RedirectToAction("Register", "Account");
            }
            ModelState.TryAddModelError("", "Fail to register"); 
            return View(model);
        }


    }
}
