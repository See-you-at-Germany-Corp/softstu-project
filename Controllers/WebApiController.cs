using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
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
}