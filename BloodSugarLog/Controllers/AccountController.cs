using BloodSugarLog.Models;
using BloodSugarLog.Services;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommandModel model)
        {
            if(ModelState.IsValid)
            {
               if(await _service.Login(model))  return RedirectToAction("Index","Home");
            }
            ModelState.AddModelError("", "Fail to login");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
               await  _service.Logout();    
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
