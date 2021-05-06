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
        public async Task<IActionResult> Booking()
        {
            int id = 1;
            Laboratory lab = LabDB.GetByID(id);
            ViewData["LabInfo"] = lab;
            List<ItemDetail> items = await ItemDB.GetAllDetailByLabIDAsync(id);
            ViewData["LabItems"] = items;
            List<Transaction> transactions = await TransactionDB.GetByLabIDAsync(id);
            ViewData["LabTransactions"] = transactions;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}