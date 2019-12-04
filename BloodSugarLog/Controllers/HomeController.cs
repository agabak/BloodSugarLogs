using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodSugarLog.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodSugarLog.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBloodSugarLogService _service;
       
        public HomeController(IBloodSugarLogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var bloodHistory = await _service.GetBloodLogs(this.User.Identity.Name);
            return View(bloodHistory);
        }
    }
}