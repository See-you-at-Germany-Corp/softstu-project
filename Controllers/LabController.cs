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

        public IActionResult Index()
        {
            return View();
        }
 
        [HttpGet]
        [Route("Lab/{labID}")]
        public async Task<IActionResult> Detail(int labID)
        {
            Laboratory labDetail = LabDB.GetByID(labID);
            List<ItemDetail> itemDetails = await ItemDB.GetAllDetailByLabIDAsync(labID);
            List<int> itemSet = new List<int>();
            List<int> itemQuantity = await ItemDB.GetAllQuantityByLabIDAsync(labID);

            itemDetails.ForEach(item => {
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