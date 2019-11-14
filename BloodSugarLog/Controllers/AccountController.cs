using BloodSugarLog.Entities;
using BloodSugarLog.Models;
using BloodSugarLog.Services;
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
        private readonly IBloodSugarLogService _service;
       
        public AccountController(IBloodSugarLogService service)
        {
            _service = service;

        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Register(RegisterCommandModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var isRegistered = await _service.Register(model);
            if(isRegistered)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ModelState.TryAddModelError("", "Fail to register"); 
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }


        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
               await  _service.Logout();
            }

            return RedirectToAction("index", "Home");
        }

    }
}
