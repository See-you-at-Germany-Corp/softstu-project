using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ConsoleApp.PostgreSQL;
using softstu_project.Models;
using Npgsql;

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
        public async Task<ActionResult<List<List<AvailableItemsModel>>>> Gets(string datetime_str)  // not sure about "Item"
        {
            DateTime booking_datetime = DateTime.ParseExact(datetime_str, "yyyy-MM-dd", null);
            return await ItemDB.GetAvailableItems(booking_datetime);
        }
    }
}