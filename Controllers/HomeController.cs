using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using softstu_project.Models;
using ConsoleApp.PostgreSQL;

namespace softstu_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        

        public async Task<IActionResult> Index()
        {
            List<Laboratory> name = await LabDB.GetAllAsync();
            List<ItemDetail> items = await ItemDB.GetAllDetailAsync();
            List<List<ItemDetail>> labItem = new List<List<ItemDetail>>();
            for(int i=0;i<5;i++)
            {
                labItem.Add(new List<ItemDetail>());
            }
            for(int i=0;i<items.Count;i++)
            {
                Console.WriteLine(i);
                labItem[items[i].laboratory_id - 1].Add(items[i]);
                
            }
            ViewData["name"] = name;
            ViewData["labItem"] = labItem;
            return View();
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
