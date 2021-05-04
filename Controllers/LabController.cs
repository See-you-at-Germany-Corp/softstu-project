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
    public class LabController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public LabController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<Laboratory> name = await LabDB.GetAllAsync();
            List<ItemDetail> items = await ItemDB.GetAllDetailAsync();
            List<List<ItemDetail>> labItem = new List<List<ItemDetail>>();
            List<List<ItemDetail>> myType = new List<List<ItemDetail>>();
            List<Int64> checkList = new List<Int64>();
            for (int i = 0; i < 5; i++)
            {
                labItem.Add(new List<ItemDetail>());
                myType.Add(new List<ItemDetail>());
            }
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine(i);
                labItem[items[i].laboratory_id - 1].Add(items[i]);

            }
            Console.WriteLine(labItem.Count);

            for (int i = 0; i < labItem.Count; i++)
            {
                for (int j = 0; j < labItem[i].Count; j++)
                {
                    if (!checkList.Contains(labItem[i][j].type))
                    {
                        myType[i].Add(labItem[i][j]);
                        checkList.Add(labItem[i][j].type);
                    }
                }
                checkList.Clear();

            }
            Console.WriteLine(labItem.Count);
            ViewData["name"] = name;
            ViewData["myType"] = myType;
            ViewData["labItem"] = labItem;
            return View();
        }

        [HttpGet]
        [Route("Lab/{labID}")]
        public async Task<IActionResult> Detail(int labID)
        {
            Laboratory labDetail = LabDB.GetByID(labID);
            List<ItemDetail> itemDetails = await ItemDB.GetAllDetailByLabIDAsync(labID);
            List<int> itemSet = await ItemDB.GetItemSetByLabIDAsync(labID);
            List<int> itemQuantity = await ItemDB.GetAllQuantityByLabIDAsync(labID);

            itemDetails.ForEach(item =>
            {
                if (!itemSet.Contains(item.type)) itemSet.Add(item.type);
            });

            ViewData["LabDetail"] = labDetail;
            ViewData["ItemSet"] = itemSet;
            ViewData["ItemQuantity"] = itemQuantity;
            ViewData["LabID"] = labID;

            return View();
        }

        [HttpGet]
        [Route("Lab/{labID}/Booking")]
        public IActionResult Booking(int labID)
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
