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

        [HttpGet]
        [Route("Lab")]
        public async Task<IActionResult> Index()
        {
            List<Laboratory> nameLab = await LabDB.GetAllAsync();
            List<ItemDetail> items = await ItemDB.GetAllDetailAsync();
            List<List<ItemDetail>> labItem = new List<List<ItemDetail>>();
            List<List<ItemDetail>> myType = new List<List<ItemDetail>>();
            List<Int64> checkList = new List<Int64>();

            for (int i = 0; i < nameLab.Count; i++)
            {
                labItem.Add(new List<ItemDetail>());
                myType.Add(new List<ItemDetail>());
            }

            for (int i = 0; i < items.Count; i++)
                labItem[items[i].laboratory_id - 1].Add(items[i]);

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

            ViewData["nameLab"] = nameLab;
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
        public async Task<IActionResult> Booking(int labID)
        {
            Laboratory lab = LabDB.GetByID(labID);
            ViewData["LabInfo"] = lab;

            List<int> itemSet = await ItemDB.GetItemSetByLabIDAsync(labID);
            List<string> itemSetNames = new List<string>();
            foreach (var item in itemSet)
            {
                itemSetNames.Add(((ItemTypes)item).ToString());
            }

            TempData["LabID"] = lab.uuid;
            ViewData["LabItemSet"] = itemSet;

            return View();
        }

        public async Task<ActionResult> SubmitBooking(BookingFormModel formModel)
        {
            var lab_id = int.Parse(TempData["LabID"].ToString());
            var user_id = int.Parse(HttpContext.Request.Cookies["userID"]);

            var time_id = formModel.time_am + formModel.time_pm;

            var items = await ItemDB.GetAvailableItems(formModel.book_date);

            items.RemoveAll(item => item.type != formModel.item_type);

            switch (time_id)
            {
                case 1:
                    items.RemoveAll(item => item.time_am == false);
                    break;
                case 2:
                    items.RemoveAll(item => item.time_pm == false);
                    break;
                case 3:
                    items.RemoveAll(item => item.time_am == false || item.time_pm == false);
                    break;
                default:
                    break;
            }

            if (items.Count > 0)
            {
                for (var i = 0; i < formModel.quantity; ++i)
                {
                    var temp = TransactionDB.Add(new Transaction(user_id, items[i].uuid, (int)Transaction_type.borrow, time_id, formModel.book_date)).Result;
                    if (temp == 1)
                    {
                        TempData["BookingSucceed"] = false;
                        return RedirectToAction("Booking", new { labID = lab_id });
                    }
                }
            }

            return RedirectToAction("Index", "User");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
