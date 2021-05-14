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
                    case (int)Time_id_type.am:
                        amTimeSlot--;
                        break;
                    case (int)Time_id_type.pm:
                        pmTimeSlot--;
                        break;
                    case (int)Time_id_type.day:
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

            int user_id = int.Parse(Request.Form["user_id"]);
            int time_id = int.Parse(Request.Form["time_id"]);
            int quantity = int.Parse(Request.Form["quantity"]);
            int item_type = int.Parse(Request.Form["item_type"]);
            DateTime book_date = DateTime.ParseExact(Request.Form["book_date"], "M/d/yyyy", null);

            // db.items.FromSqlRaw()
            Console.WriteLine(book_date.ToString("yyyy-dd-MM"));

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
}