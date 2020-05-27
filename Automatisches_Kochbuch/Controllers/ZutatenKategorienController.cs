using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Automatisches_Kochbuch.Model;
using Automatisches_Kochbuch.Context;
using AutoMapper;
using Automatisches_Kochbuch.Dtos;

namespace Automatisches_Kochbuch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZutatenKategorienController : ControllerBase
    {
        private readonly AutomatischesKochbuchContext _context;
        private readonly IMapper _mapper;

        public ZutatenKategorienController(AutomatischesKochbuchContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Es werden alle ZutatenKategorien angezeigt
        /// </summary>
        /// <returns>
        /// Alle ZutatenKategorien
        /// </returns>

        // GET: api/ZutatenKategorien
        [HttpGet]
        public ActionResult<IEnumerable<TabZutatenKategorien>> GetTabZutatenKategorien()
        {
            IEnumerable<TabZutatenKategorien> tabZutatenKategorien = _context.TabZutatenKategorien;
            return Ok(_mapper.Map<IEnumerable<ZutatenKategorienReadDto>>(tabZutatenKategorien));
        }

        /// <summary>
        /// Es wird nur eine ZutatenKategorien angezeigt
        /// </summary>
        /// <param name="id">
        /// Die ID der ZutatenKategorie
        /// </param>
        /// <returns>
        /// Gibt eine ZutatenKategorie zurück.
        /// </returns>
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
                return NotFound("Es konnte keine Kategorie für die Zutat gefunden werden!");
            }

            return Ok(_mapper.Map<ZutatenKategorienReadDto>(tabZutatenKategorien));
        }

        /// <summary>
        /// Es wird eine ZutatenKategorie geändert
        /// </summary>
        /// <param name="id">
        /// Die ID der ZutatenKategorie
        /// </param>
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
                return BadRequest("Es konnte keine Kategorie für die Zutat gefunden werden!");
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
                    return NotFound("Es konnte keine Kategorie für die Zutat gefunden werden!");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Es wird eine neue ZutatenKategorie hinzugefügt
        /// </summary>
        /// <remarks>
        /// Geben Sie eine neue ZutatenKategorie ein
        /// </remarks>
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

        /// <summary>
        /// Es wird eine ZutatenKategorie gelöscht
        /// </summary>
        /// <param name="id">
        /// Die ID der ZutatenKatgorie
        /// </param>
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
                return NotFound("Es konnte keine Kategorie für die Zutat gefunden werden!");
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