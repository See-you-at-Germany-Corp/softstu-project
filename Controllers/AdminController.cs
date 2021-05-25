using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using softstu_project.Models;
using ConsoleApp.PostgreSQL;
using System.Globalization;
#nullable enable

namespace softstu_project.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public AdminController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Admin/")]
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

        public async Task<IActionResult> Tools()
        {

            // int labID = Int16.Parse(id ?? "1");
            // Laboratory lab = LabDB.GetByID(Int16.Parse(id ?? "1"));
            List<Laboratory> lab = await LabDB.GetAllAsync();
            // List<LabItem> items = new List<LabItem>();
            // IList<ItemDetail> labItems = await ItemDB.GetAllDetailByLabIDAsync(Int16.Parse(id ?? "1"));
            IList<Item> labItems = await ItemDB.GetAllAsync();
            List<Laboratory> labList = await LabDB.GetAllAsync();


            ViewData["LabItems"] = labItems;
            ViewData["Title"] = labList;
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

        public async Task<IActionResult> OnCancelTransaction(int cancelTransactionID)
        {
            if (cancelTransactionID > 0)
            {
                List<Transaction> transactions = await TransactionDB.GetAsync(cancelTransactionID);
                TransactionDB.Cancel(transactions[0]);
            }

            return RedirectToAction("Blacklist");
        }

        public IActionResult OnHomeLogDateChange(string homedate)
        {
            return RedirectToAction("Blacklist");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("Admin/Detail/")]
        public async Task<IActionResult> Detail(string? id, string? date)
        {
            int labID = Int16.Parse(id ?? "1");
            DateTime datetime = DateTime.ParseExact(date ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm"), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            Laboratory lab = LabDB.GetByID(Int16.Parse(id ?? "1"));
            List<LabItem> items = new List<LabItem>();
            IList<ItemDetail> labItems = await ItemDB.GetAllDetailByLabIDAsync(Int16.Parse(id ?? "1"));
            List<List<int>> allQuantity = await LabItemDB.GetCurrentQuantityByDateAsync(datetime);
            List<Transaction> transactions = await TransactionDB.GetByLabIDAndDateAsync(labID, datetime);
            List<int> availableNumber = new List<int> { allQuantity[0][labID - 1], allQuantity[1][labID - 1] };
            List<Laboratory> labList = await LabDB.GetAllAsync();

            for (int i = 0; i < labItems.Count; i++)
            {
                var available = 0;
                string am = "NO";
                var pm = "NO";
                for (int j = 0; j < transactions.Count; j++)
                {
                    if (labItems[i].uuid == transactions[j].item_id)
                    {
                        available = transactions[j].time_id;
                    }
                }
                var pmm = 1 & (available >> 1);
                var amm = 1 & available;
                am = amm == 0 ? "YES" : "NO";
                pm = pmm == 0 ? "YES" : "NO";
                items.Add(new LabItem(labItems[i].uuid.ToString(), labItems[i].name, am, pm));
            }

            ViewData["LabItems"] = items;
            ViewData["Title"] = labList;
            ViewData["Description"] = lab.description;
            ViewData["LabID"] = id;
            ViewData["Date"] = datetime.ToString("yyyy-MM-dd");
            ViewData["Available"] = availableNumber;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Update(string? id, string? date, [FromQuery] string[] itemnames, [FromQuery] string[] removeid)
        {
            foreach (var name in itemnames)
            {
                if (id != null)
                {
                    var labID = Int16.Parse(id ?? "0");
                    int itemID = ItemDB.Add(new Item(name, (ItemTypes)labID));
                    LabItemDB.AddItem(labID, itemID);
                }
            }
            if (id != null)
            {
                foreach (var removeID in removeid)
                {
                    var labID = Int16.Parse(id ?? "0");
                    var itemID = Int16.Parse(removeID ?? "0");
                    var itemDB = await ItemDB.GetByIDAsync(itemID);
                    var labItemDB = await LabItemDB.GetAllByLabIDAsync(labID);
                    foreach (var ldb in labItemDB)
                    {
                        if (ldb.item_id == itemID)
                        {

                            LabItemDB.RemoveItem(ldb.item_id);
                        }
                    }
                    ItemDB.Remove(itemDB[0]);

                }
            }
            return RedirectToAction("Detail", new { id = id, date = date });
        }
    }
}
