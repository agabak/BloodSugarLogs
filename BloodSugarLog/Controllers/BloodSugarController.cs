using BloodSugarLog.Models;
using BloodSugarLog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BloodSugarLog.Controllers
{
    [Authorize]
    public class BloodSugarController : Controller
    {

         private readonly IBloodSugarLogService _service;
         private readonly ILogger _logger;
       
       
        public BloodSugarController(IBloodSugarLogService service,ILogger<BloodSugarController> logger)
        {
            _service = service;
            _logger = logger;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommandModel model)
        {
            if(ModelState.IsValid)
            {
              model.Name = User.Identity.Name;
              if(await _service.Create(model))
               {
                    _logger.LogInformation("User Created with {1}, {2} and {3}", model.FoodName, model.BloodValue, model.Name);
                    return RedirectToAction("Index", "Home");
               } 

            }

            ModelState.AddModelError("", "Something went wromg to create database");
            _logger.LogError("Unable to create blood logs", model);
            return View();
        }
    }
}