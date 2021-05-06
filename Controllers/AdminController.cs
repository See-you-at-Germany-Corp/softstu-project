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
            List<LabListModel> labLists = await LabDB.GetListAsync();

            /// get item transaction log here.
            List<Log> logLists = await LogDB.GetAllAsync(); 
            logLists.Sort((x, y) => DateTime.Compare(y.created, x.created));

            ViewData["LabLists"] = labLists;
            ViewData["LogLists"] = logLists;

            return View();
        }

        public IActionResult Tools()
        {
            return View();
        }

        public async Task<IActionResult> Blacklist()
        {
            /// get item transaction log here.
            List<Log> logLists = await LogDB.GetAllAsync();
            logLists.Sort((x, y) => DateTime.Compare(y.created, x.created));

            /// get all borrowed items list.
            List<Transaction> blacklistLists = await TransactionDB.GetAllAsync();
            blacklistLists.Sort((x, y) => DateTime.Compare(x.book_date, y.book_date));

            ViewData["LogLists"] = logLists;
            ViewData["BlacklistLists"] = blacklistLists;

            return View();
        }
 
        public async Task<IActionResult> OnDeleteTransaction(int transactionID) 
        {   
            if (transactionID > 0)
            {
                List<Transaction> transactions = await TransactionDB.GetAsync(transactionID);
                TransactionDB.Delete(transactions[0]);  
            }

            return RedirectToAction("Blacklist");
        }
 
        public IActionResult OnHomeLogDateChange(string homedate) 
        {   
            Console.WriteLine("homedate " + homedate);

            return RedirectToAction("Blacklist");
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
