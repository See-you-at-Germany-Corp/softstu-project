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

        public async Task<IActionResult> Index()
        {
            /// get lab lists here. 
            IList<LabListModel> labLists = await LabDatabase.GetListAsync();

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

        public async Task<IActionResult> Tools(string? id)
        {

            int labID = Int16.Parse(id ?? "1");
            Laboratory lab = LabDatabase.GetByID(Int16.Parse(id ?? "1"));
            List<LabItem> items = new List<LabItem>();
            IList<ItemDetail> labItems = await ItemDatabase.GetAllDetailByLabIDAsync(Int16.Parse(id ?? "1"));
            List<Laboratory> labList = await LabDatabase.GetAllAsync();

            
            ViewData["LabItems"] = labItems;
            ViewData["Title"] = labList;
            ViewData["Description"] = lab.description;
            ViewData["LabID"] = id;
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string? id, string? date)
        {
            int labID = Int16.Parse(id ?? "1");
            DateTime datetime = DateTime.ParseExact(date ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm"),"yyyy-MM-dd HH:mm",CultureInfo.InvariantCulture);
            Laboratory lab = LabDatabase.GetByID(Int16.Parse(id ?? "1"));
            List<LabItem> items = new List<LabItem>();
            IList<ItemDetail> labItems = await ItemDatabase.GetAllDetailByLabIDAsync(Int16.Parse(id ?? "1"));
            List<List<int>> allQuantity = await LabItemDatabase.GetCurrentQuantityByDateAsync(datetime);
            List<Transaction> transactions = await TransactionDatabase.GetByLabIDAndDateAsync(labID,datetime );
            List<int> availableNumber = new List<int>{allQuantity[0][labID-1] , allQuantity[1][labID-1]};
            List<Laboratory> labList = await LabDatabase.GetAllAsync();

            for(int i = 0; i < labItems.Count; i++) {
                var available = 0;
                string am = "NO";
                var pm = "NO";
                for(int j = 0; j < transactions.Count; j++) {
                    if(labItems[i].uuid == transactions[j].item_id) {
                        available = transactions[j].time_id;
                    }
                }
                var pmm = 1 & (available >> 1);
                var amm = 1 & available;
                am = amm == 0 ? "YES" : "NO";
                pm = pmm == 0 ? "YES" : "NO";
                items.Add(new LabItem(labItems[i].uuid.ToString(),labItems[i].name,am,pm));
            }
            
            ViewData["LabItems"] = items;
            ViewData["Title"] = labList;
            ViewData["Description"] = lab.description;
            ViewData["LabID"] = id;
            ViewData["Date"] = datetime.ToString("yyyy-MM-dd");
            ViewData["Available"] = availableNumber  ;
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Update(string? id, string? date,[FromQuery] string[] itemnames,[FromQuery] string[] removeid){
            foreach(var name in itemnames) {
                if (id != null) {
                    var labID = Int16.Parse(id ?? "0");
                    int itemID = ItemDatabase.Add(new Item(name));
                    LabItemDatabase.AddItem(labID ,itemID);
                }
            }
            if (id != null) {
                foreach(var removeID in removeid) {
                    var labID = Int16.Parse(id ?? "0");
                    var itemID = Int16.Parse(removeID ?? "0");
                    var itemDB = await ItemDatabase.GetByIDAsync(itemID);
                    var labItemDB = await LabItemDatabase.GetAllByLabIDAsync(labID);
                    foreach(var ldb in labItemDB) {
                        if(ldb.item_id == itemID) {
                            
                            LabItemDatabase.RemoveItem(ldb);
                        }
                    }
                    ItemDatabase.Remove(itemDB[0]);
                    
                }
            }
            return RedirectToAction("Detail", new { id = id , date = date});
        }
    }
}
