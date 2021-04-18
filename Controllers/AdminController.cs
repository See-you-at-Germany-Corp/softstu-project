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
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AdminController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            /// get lab lists here. 
            IList<LabListModel> labLists = await LabDatabase.GetLabList();

            /// get item transaction log here.
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
            /// get item transaction log here.
            IList<LogModel> logLists = new List<LogModel>();
            for (int i = 0; i < 20; i++)
            {
                logLists.Add(new LogModel() { id = i, item_id = (i + 1), user_name = "Me " + (i + 1), transaction_type = i % 2, created = DateTime.Now });
            }

            /// get all borrowed items list.
            IList<LogModel> blacklistLists = new List<LogModel>(
                logLists.Where(log => log.transaction_type == 0).ToList()
            );

            ViewData["LogLists"] = logLists;
            ViewData["BlacklistLists"] = blacklistLists;

            return View();
        }

        public IActionResult Detail()
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
