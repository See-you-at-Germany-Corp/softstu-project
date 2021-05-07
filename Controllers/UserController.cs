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
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public UserController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            int id = 10;
            List<User> users = await UserDB.GetByIDAsync(id);
            ViewData["UserInfo"] = users[0];
            return View();
        }

        [HttpGet]
        [Route("User/{labID}/Booking")]
        public async Task<IActionResult> Booking(int labID)
        {
            Laboratory lab = LabDB.GetByID(labID);
            List<ItemDetail> items = await ItemDB.GetAllDetailByLabIDAsync(labID);
            List<Transaction> transactions = await TransactionDB.GetByLabIDAsync(labID);

            ViewData["LabInfo"] = lab;
            // ViewData["LabItems"] = items;
            ViewData["LabTransactions"] = transactions;

            List<int> itemSet = await ItemDB.GetItemSetByLabIDAsync(labID);
            List<string> itemSetNames = new List<string>();
            foreach (var item in itemSet)
            {
                itemSetNames.Add(((ItemTypes)item).ToString());
            }
            ViewData["LabItemSet"] = itemSet;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}