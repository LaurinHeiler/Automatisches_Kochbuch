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
using Microsoft.AspNetCore.Rewrite.Internal;

namespace Automatisches_Kochbuch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZutatenController : ControllerBase
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public ZutatenController(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Es werden alle Zutaten angezeigt
        /// </summary>
        /// <returns>
        /// Alle Zutaten
        /// </returns>
        // GET: api/Zutaten
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TabZutaten>>> GetZutaten()
        {
            IEnumerable<TabZutaten> tabZutaten = await _context.TabZutaten.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ZutatenReadDto>>(tabZutaten));
        }

        /// <summary>
        /// Es wird nur eine Zutat angezeigt
        /// </summary>
        /// <param name="id">
        /// Die ID der Zutat
        /// </param>
        // GET: api/Zutaten/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTabZutaten([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tabZutaten = await _context.TabZutaten.FindAsync(id);

            if (tabZutaten == null)
            {
                return NotFound("Es wurden keine Zutaten gefunden!");
            }

            return Ok(_mapper.Map<ZutatenReadDto>(tabZutaten));
        }

        /// <summary>
        /// Es wird eine Zutat geändert
        /// </summary>
        /// <param name="id">
        /// Die ID der Zutat
        /// </param>
        // PUT: api/Zutaten/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTabZutaten([FromRoute] int id, [FromBody] TabZutaten tabZutaten)
        {
            //überprüfen ob Daten und ID eingegeben worden sind.
            if (tabZutaten == null || id != tabZutaten.Id)
            {
                return BadRequest("Irgendetwas ist schief gelaufen! Geben Sie die Parameter erneut ein.");
            }

            //entsprechende Zutat aus der DB holen
            TabZutaten zutatDB = await _context.TabZutaten.SingleOrDefaultAsync(u =>
            u.Id == tabZutaten.Id);

            //falls eine Zutat gefunden wurde, dessen Daten aktualisieren
            if (zutatDB != null)
            {
                zutatDB.IdZutatEinheit = tabZutaten.IdZutatEinheit;
                zutatDB.IdZutatKategorie = tabZutaten.IdZutatKategorie;
                zutatDB.Id = tabZutaten.Id;
                zutatDB.Vegan = tabZutaten.Vegan;
                zutatDB.Vegetarisch = tabZutaten.Vegetarisch;
                zutatDB.Glutenfrei = tabZutaten.Glutenfrei;

                await _context.SaveChangesAsynchron();

                return Ok(zutatDB);
            }

            return NotFound("Die Zutat existiert nicht!");
        }

        /// <summary>
        /// Es wird eine Zutat hinzugefügt
        /// </summary>
        /// <remarks>
        /// Geben Sie eine neue Zutat ein
        /// </remarks>
        // POST: api/Zutaten
        [HttpPost]
        public async Task<IActionResult> PostTabZutaten([FromBody] TabZutaten tabZutaten)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TabZutaten.Add(tabZutaten);
            await _context.SaveChangesAsynchron();

            return CreatedAtAction("GetTabZutaten", new { id = tabZutaten.Id }, tabZutaten);
        }

        /// <summary>
        /// Es wird eine Zutat gelöscht
        /// </summary>
        /// <param name="id">
        /// Die ID der Zutat
        /// </param>
        // DELETE: api/Zutaten/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTabZutaten([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tabZutaten = await _context.TabZutaten.FindAsync(id);
            if (tabZutaten == null)
            {
                return NotFound("Es konnten keine Zutaten gefunden werden!");
            }

            _context.TabZutaten.Remove(tabZutaten);
            await _context.SaveChangesAsynchron();

            return Ok(tabZutaten);
        }

    }
}