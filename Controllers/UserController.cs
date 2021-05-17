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
            ViewData["UserInfo"] = new User(User_role.user, "", "", "", "", 0, "", "", "", 1);
            ViewData["TransactionItems"] = new List<TransactionItem>();

            var userID = HttpContext.Request.Cookies["userID"];
            if (userID != null)
                if (int.Parse(userID) > 0)
                {
                    List<User> users = await UserDB.GetByIDAsync(int.Parse(userID));
                    List<TransactionItem> transactionItems = await TransactionDB.GetWithItemByUserIDAsync(int.Parse(userID));

                    ViewData["UserInfo"] = users[0];
                    ViewData["TransactionItems"] = transactionItems;
                }

            return View();
        }

        [Route("user/cancel-transaction")]
        public async Task<ActionResult> CancelTransaction(int transaction_uuid)
        {
            List<Transaction> transaction = await TransactionDB.GetAsync(transaction_uuid);

            var result = (transaction[0].book_date - DateTime.Now).TotalHours;

            if (result <= 0)
            {
                TempData["CancelSucceed"] = "1";
                return RedirectToAction("Index", "User");
            }

            if (result <= 1)
            {
                TempData["CancelSucceed"] = "2";
                return RedirectToAction("Index", "User");
            }


            TransactionDB.Cancel(transaction[0]);
            TempData["CancelSucceed"] = "0";
            return RedirectToAction("Index", "User");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}