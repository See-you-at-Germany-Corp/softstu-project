using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ConsoleApp.PostgreSQL;
using softstu_project.Models;
using Npgsql;

namespace WebApi.Controllers
{
    [Route("api/simple")]
    public class Simplecontroller : Controller
    {
        public Simplecontroller() { }

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
            return await LabDatabase.GetAllLab();
        }

        [HttpGet("{labID}")]
        public ActionResult<Laboratory> Gets(int labID)
        {
            return LabDatabase.GetLabByID(labID);
        }
    }

    [Route("api/lab_item")]
    public class LabItem : Controller
    {
        public LabItem() { }

        [HttpGet("")]
        public ActionResult<IEnumerable<string>> Gets()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{type}/{labID}")]
        public async Task<ActionResult<List<Laboratory_item>>> Gets(string type, int labID)
        {
            if (type == "all") 
                return await LabItemDatabase.GetAllItemByLabID(labID);
            else if (type == "current") 
                return await LabItemDatabase.GetCurrentItemByLabID(labID);
            else 
                return null;
        }  
    }
}