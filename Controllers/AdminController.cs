using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using soft_stu_project.Models;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
