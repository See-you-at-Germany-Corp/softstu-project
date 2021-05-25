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

            int allItems = await LabItemDB.GetLabItemCountByLabIDAndType(labID, itemType);
            int amTimeSlot = allItems;
            int pmTimeSlot = allItems;

            foreach (var transaction in transactions)
            {
                switch (transaction.time_id)
                {
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
                    case (int)Time_id_type.none:
                        break;
                    default:
                        break;
                }
            }

            string jsonString = $@"{{
                am: {amTimeSlot},
                pm: {pmTimeSlot},
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
        public async Task<ActionResult<List<ItemsLaboratoryTransaction>>> Gets(string datetime_str)  // not sure about "Item"
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

        [HttpPost("")]
        public async Task<ActionResult<HttpResponseMessage>> Post([FromBody] Book body)  // not sure about "Item"
        {
            int status = await UserDB.BookItems(body.user_id, body.item_id, body.time_id, body.date);
            if (status == 0)
                return StatusCode(201);
            else
                return StatusCode(400);
        }

        [HttpDelete("")]
        public async Task<ActionResult<HttpResponseMessage>> Delete(int transaction_id)  // not sure about "Item"
        {
            if (transaction_id > 0)
            {
                List<Transaction> transactions = await TransactionDB.GetAsync(transaction_id);
                TransactionDB.Cancel(transactions[0]);

                return StatusCode(204);
            }
            else
                return StatusCode(400);
        }
    }

    [Route("api/account/register")]
    public class Account : Controller
    {
        public Account() { }

        [HttpPost("")]
        public ActionResult<HttpResponseMessage> Post([FromBody] UserRegister body)
        {
            User user = new User((User_role)body.role_id, body.username, body.password, body.fname, body.lname, body.student_id, body.faculty, body.department, body.email, body.gender);
            UserDB.Register(user);

            return StatusCode(201);
        }
    }
}