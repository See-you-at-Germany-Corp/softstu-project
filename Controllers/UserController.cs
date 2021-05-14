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

        [HttpGet]
        [Route("User/")]
        public async Task<IActionResult> Index()
        {
            var userID = HttpContext.Request.Cookies["userID"];
            List<User> users = await UserDB.GetByIDAsync(int.Parse(userID));
            List<TransactionItem> transactionItems = await TransactionDB.GetWithItemByUserIDAsync(int.Parse(userID));

            ViewData["UserInfo"] = users[0];
            ViewData["TransactionItems"] = transactionItems;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}