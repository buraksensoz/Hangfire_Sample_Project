using HangFireWork.App.Models;
using HangFireWork.App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HangFireWork.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult AddSingleWork()
        {
            HangfireService.AddOnceWork();
            return RedirectToAction("index");
        }

        public IActionResult AddScheduleWork()
        {
            HangfireService.AddScheduleWork();
            return RedirectToAction("index");
        }

        public IActionResult AddRecurringWork()
        {
            HangfireService.AddRecurringWork();
            return RedirectToAction("index");
        }

        public IActionResult ClearLogs()
        {
            HangfireService.ClearLogs();
            return RedirectToAction("index");
        }

        public async Task<String> GetLogs()
        {
            var Logs= await HangfireService.ReadLogFileAsync();
            var TableHtml = $"<table class='table table-striped'>{Logs}</table>";
            return TableHtml;


        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
