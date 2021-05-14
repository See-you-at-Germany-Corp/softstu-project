using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ConsoleApp.PostgreSQL;
using softstu_project.Models;
using Npgsql;
using Newtonsoft.Json.Linq;

namespace WebApi.Controllers
{
    [Route("api/simple")]
    public class SimpleController : Controller
    {
        public SimpleController() { }

        // GET api/simple
        [HttpGet("")]
        public ActionResult<IEnumerable<string>> Gets()
        {
            return new string[] { "value1", "value2" };
        }
    }

    [Route("api/lab")]
    public class LabController : Controller
    {
        public LabController() { }

        [HttpGet("")]
        public async Task<ActionResult<List<Laboratory>>> Gets()
        {
            return await LabDB.GetAllAsync();
        }

        [HttpGet("{labID}")]
        public ActionResult<Laboratory> Gets(int labID)
        {
            return LabDB.GetByID(labID);
        }

        [HttpGet("quantity")]
        public async Task<ActionResult> GetQuantity(int labID, int itemType, int timestamp)
        {
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            date = date.AddSeconds(timestamp);

            List<Transaction> transactions = await TransactionDB.GetByLabIDTypeAndDateAsync(labID, itemType, date);

            int allItems = await LabItemDB.GetLabItemCountByLabIDAndType(1, 1);
            int amTimeSlot = allItems;
            int pmTimeSlot = allItems;

            foreach (var transaction in transactions)
            {
                switch (transaction.time_id)
                {
                    case (int)Time_id_type.none:
                        break;
                    case (int)Time_id_type.AM:
                        amTimeSlot--;
                        break;
                    case (int)Time_id_type.PM:
                        pmTimeSlot--;
                        break;
                    case (int)Time_id_type.Day:
                        amTimeSlot--;
                        pmTimeSlot--;
                        break;
                    default:
                        break;
                }
            }


            string jsonString = $@"{{
                am: {(date.Day == 1 ? 0 : amTimeSlot)},
                pm: {(date.Day == 1 ? 0 : pmTimeSlot)},
            }}";
            JObject result = JObject.Parse(jsonString);

            return Json(result);
        }
    }

    [Route("api/lab_item")]
    public class LabItemController : Controller
    {
        public LabItemController() { }

        [HttpGet("{labID}")]
        public async Task<ActionResult<List<Laboratory_item>>> Gets(int labID)
        {
            return await LabItemDB.GetAllByLabIDAsync(labID);
        }
    }

    [Route("api/item")]
    public class ItemController : Controller
    {
        public ItemController() { }

        [HttpPost("booking")]
        public async Task<ActionResult> Post()
        {
            var db = new SoftwareStudioContext();

            int lab_id = int.Parse(Request.Form["lab_id"]);
            int user_id = int.Parse(Request.Form["user_id"]);
            int time_id = int.Parse(Request.Form["time_id"]);
            int quantity = int.Parse(Request.Form["quantity"]);
            int item_type = int.Parse(Request.Form["item_type"]);
            DateTime book_date = DateTime.ParseExact(Request.Form["book_date"], "M/d/yyyy", null);

            List<List<ItemsLaboratoryTransaction>> time_slots = await ItemDB.GetAvailableItems(book_date);

            time_slots[0].RemoveAll(am => am.laboratory_id != lab_id);
            time_slots[1].RemoveAll(pm => pm.laboratory_id != lab_id);

            if (time_id == 3)
            {
                List<ItemsLaboratoryTransaction> day = new List<ItemsLaboratoryTransaction>();
                foreach (var item in time_slots[0])
                {
                    if (time_slots[1].Contains(item))
                    {
                        day.Add(item);
                    }
                }
            }

            for (var i = 0; i < quantity; i++)
            {
            }

            return Json("");
        }
    }

    [Route("api/transaction")]
    public class TransactionController : Controller
    {
        public TransactionController() { }

        [HttpGet("")]
        public async Task<ActionResult<List<Transaction>>> Gets()
        {
            return await TransactionDB.GetAllAsync();
        }

        [HttpPost("")]
        public ActionResult<HttpResponseMessage> Posts(HttpRequestMessage requestMessage)
        {
            /// create transaction from FORM DATA here.
            // Transaction transaction = new Transaction(1, 6, (int)Transaction_type.borrow, (int)Time_id_type.am, DateTime.Now, DateTime.Now);
            // TransactionDatabase.AddTransaction(transaction);
            return StatusCode(201);
        }
    }
    [Route("api/available_items")]
    public class AvailableItems : Controller
    {
        public AvailableItems() { }
        [HttpGet("{datetime_str}")]
        public async Task<ActionResult<List<List<ItemsLaboratoryTransaction>>>> Gets(string datetime_str)  // not sure about "Item"
        {
            DateTime booking_datetime = DateTime.ParseExact(datetime_str, "yyyy-MM-dd", null);
            return await ItemDB.GetAvailableItems(booking_datetime);
        }
    }

    [Route("api/booked_items")]
    public class BookedItems : Controller
    {
        public BookedItems() { }
        [HttpGet("{user_id}")]
        public async Task<ActionResult<List<Transaction>>> Gets(int user_id)  // not sure about "Item"
        {
            return await UserDB.GetBookedItems(user_id);
        }
        [HttpPost("{user_id}/{item_id}/{time_id}/{date}")]
        public ActionResult<HttpResponseMessage> Post(int user_id, int item_id, int time_id, string date)  // not sure about "Item"
        {
            int status = UserDB.BookItems(user_id, item_id, time_id, date);
            if (status == 0)
                return StatusCode(201);
            else
                return StatusCode(400);
        }
    }
}