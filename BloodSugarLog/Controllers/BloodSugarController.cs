using BloodSugarLog.DL;
using BloodSugarLog.Models;
using BloodSugarLog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BloodSugarLog.Controllers
{
    [Authorize]
    public class BloodSugarController : Controller
    {

         private readonly IBloodSugarLogService _service;
       
       
        public BloodSugarController(IBloodSugarLogService service)
        {
            _service = service;
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
              if(await _service.Create(model)) return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Something went wromg to create database");
            return View();
        }
    }
}