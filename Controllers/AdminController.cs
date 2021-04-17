using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using soft_stu_project.Models;
#nullable enable

namespace soft_stu_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AdminController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        { 
            IList<LabListModel> labLists = new List<LabListModel>();
            for (int i = 0; i < 5; i++)
            {
                labLists.Add(new LabListModel() { id = i + 1, name = "Lab_" + (i + 1), current_tool = i, total_tool = 5 });
            } 

            IList<LogModel> logLists = new List<LogModel>();
            for (int i = 0; i < 10; i++)
            {
                logLists.Add(new LogModel() { id = i, item_id = (i + 1), user_name = "Me " + (i + 1), transaction_type = 0, created = DateTime.Now });
            }

            ViewData["LabLists"] = labLists;
            ViewData["LogLists"] = logLists;

            return View();
        }

        public IActionResult Tools()
        {
            return View();
        }

        public IActionResult Blacklist()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Detail(string? id)
        {
            List<LabItem> items = new List<LabItem>();
            items.Add(new LabItem("1","Vibrator",19,2));
            items.Add(new LabItem("2","Pornography",19,3));
            items.Add(new LabItem("3","Silicone Vagina",19,4));
            items.Add(new LabItem("4","Red Bull",19,11));
            ViewData["LabItems"] = items;
            ViewData["Title"] = "test " + id?.ToString() ?? "";
            return View();
        }
    }
}
