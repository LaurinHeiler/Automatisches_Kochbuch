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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TabZutatenKategorien>>> GetTabZutatenKategorien()
        {
            IEnumerable<TabZutatenKategorien> tabZutatenKategorien = await _context.TabZutatenKategorien.ToListAsync();
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutTabZutatenKategorien([FromRoute] int id, [FromBody] ZutatenKategorienUpdateDto Kategorie)
        {
            //überprüfen ob Daten und ID eingegeben worden sind.
            if (Kategorie == null || id != Kategorie.Id)
            {
                return BadRequest("Irgendetwas ist schief gelaufen! Geben Sie die Parameter erneut ein.");
            }

            //entsprechende Kategorie aus der DB holen
            TabZutatenKategorien kategorieDB = await _context.TabZutatenKategorien.SingleOrDefaultAsync(u =>
            u.Id == Kategorie.Id);

            //falls eine Zutat gefunden wurde, dessen Daten aktualisieren
            if (kategorieDB != null)
            {
                _mapper.Map(Kategorie, kategorieDB);

                kategorieDB.Kategorie = Kategorie.Kategorie;
                kategorieDB.Id = Kategorie.Id;

                await _context.SaveChangesAsynchron();

                return NoContent();
            }

            return NotFound("Die Kategorie existiert nicht!");
        }

        /// <summary>
        /// Es wird eine neue ZutatenKategorie hinzugefügt
        /// </summary>
        /// <remarks>
        /// Geben Sie eine neue ZutatenKategorie ein
        /// </remarks>
        // POST: api/ZutatenKategorien
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> PostTabZutatenKategorien([FromBody] ZutatenKategorienCreateDto tabZutatenKategorien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Kategorie = _mapper.Map<TabZutatenKategorien>(tabZutatenKategorien);

            _context.TabZutatenKategorien.Add(Kategorie);
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

            return NoContent();
        }

    }
}