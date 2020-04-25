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
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDataContext _context;

        public ValuesController(IDataContext context)
        {
            _context = context;
        }


        // ALLE ZUTATENKATEGORIEN ANZEIGEN --- GET api/values/zutatenkategorien
        [Route("api/[controller]/zutatenkategorien")]
        [HttpGet]
        public ActionResult<IEnumerable<TabZutatenKategorien>> zutatenkategorien()
        {
            //using (AutomatischesKochbuchContext db = new AutomatischesKochbuchContext())
            //{
            //    return db.TabZutatenKategorien.ToList();
            //}

            IEnumerable<TabZutatenKategorien> tabZutatenKategoriens = _context.AlleZutatenKategorien();
            return Ok(tabZutatenKategoriens);

        }

        [Route("api/[controller]/zutaten")]
        [HttpGet]
        public ActionResult<IEnumerable<TabZutaten>> zutaten()
        {
            IEnumerable<TabZutaten> tabZutaten = _context.AlleZutaten();
            return Ok(tabZutaten);
        }


        //api/values/rezepte?ZutatenVomUser=37&ZutatenVomUser=31&ZutatenVomUser=10
        [Route("api/[controller]/rezepte")]
        [HttpGet]
        public ActionResult<IEnumerable<TabRezepte>> rezepte(SortedSet<int> ZutatenVomUser)
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
