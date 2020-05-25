using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RoleBasedSecurityApp.Models;

namespace RoleBasedSecurityApp.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Submitter,Demo_Submitter")]
        public IActionResult Counter()
        {
            Counter counter = new Counter
            {
                Count = CounterDB.GetCounter()
            };

            return View(counter);
        }
        
        [HttpPost]
        [Authorize(Roles = "Submitter,Demo_Submitter")]
        public IActionResult Counter(int value)
        {
            int count = CounterDB.GetCounter() + 1;
            bool result = CounterDB.UpdateCounter(count);

            Counter counter = new Counter();
            if (result)
            {
                counter.Count = count;
            }
            else
            {
                counter.Count = count - 1;
            }

            return View(counter);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
