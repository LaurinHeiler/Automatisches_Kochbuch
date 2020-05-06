using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automatisches_Kochbuch.Context;
using Automatisches_Kochbuch.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Automatisches_Kochbuch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezepteController : ControllerBase
    {
        private readonly IDataContext _context;

        public RezepteController(IDataContext context)
        {
            _context = context;
        }


        //api/rezepte?ZutatenVomUser=37&ZutatenVomUser=31&ZutatenVomUser=10

        [HttpGet]
        public ActionResult<IEnumerable<TabRezepte>> GetRezepte(SortedSet<int> ZutatenVomUser)
        {
            IEnumerable<TabRezepte> tabZutaten = _context.MoeglicheRezepte(ZutatenVomUser);
            return Ok(tabZutaten);
        }


        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
