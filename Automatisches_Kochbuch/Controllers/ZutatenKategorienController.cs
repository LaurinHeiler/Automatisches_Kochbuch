using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Automatisches_Kochbuch.Model;

namespace Automatisches_Kochbuch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZutatenKategorienController : ControllerBase
    {
        private readonly AutomatischesKochbuchContext _context;

        public ZutatenKategorienController(AutomatischesKochbuchContext context)
        {
            _context = context;
        }

        // ALLE ZUTATENKATEGORIEN ANZEIGEN
        // GET: api/ZutatenKategorien
        [HttpGet]
        public ActionResult<IEnumerable<TabZutatenKategorien>> GetTabZutatenKategorien()
        {
            IEnumerable<TabZutatenKategorien> tabZutatenKategoriens = _context.TabZutatenKategorien;
            return Ok(tabZutatenKategoriens);
        }

        // GET: api/ZutatenKategorien/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTabZutatenKategorien([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tabZutatenKategorien = await _context.TabZutatenKategorien.FindAsync(id);

            if (tabZutatenKategorien == null)
            {
                return NotFound();
            }

            return Ok(tabZutatenKategorien);
        }

        // PUT: api/ZutatenKategorien/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTabZutatenKategorien([FromRoute] int id, [FromBody] TabZutatenKategorien tabZutatenKategorien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tabZutatenKategorien.Id)
            {
                return BadRequest();
            }

            _context.Entry(tabZutatenKategorien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabZutatenKategorienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ZutatenKategorien
        [HttpPost]
        public async Task<IActionResult> PostTabZutatenKategorien([FromBody] TabZutatenKategorien tabZutatenKategorien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TabZutatenKategorien.Add(tabZutatenKategorien);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTabZutatenKategorien", new { id = tabZutatenKategorien.Id }, tabZutatenKategorien);
        }

        // DELETE: api/ZutatenKategorien/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTabZutatenKategorien([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tabZutatenKategorien = await _context.TabZutatenKategorien.FindAsync(id);
            if (tabZutatenKategorien == null)
            {
                return NotFound();
            }

            _context.TabZutatenKategorien.Remove(tabZutatenKategorien);
            await _context.SaveChangesAsync();

            return Ok(tabZutatenKategorien);
        }

        private bool TabZutatenKategorienExists(int id)
        {
            return _context.TabZutatenKategorien.Any(e => e.Id == id);
        }
    }
}