﻿using System;
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
            // Query verwenden, um alle User zu holen
            IEnumerable<TabZutaten> users = await _context.GetAllZutatenAsync();

            //von der Query erhaltene User zurückgeben
            return Ok(users);
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
            if (tabZutaten == null || id != tabZutaten.Id)
            {
                return BadRequest("Irgendetwas ist schief gelaufen! Geben Sie die Parameter erneut ein.");
            }

            //TabZutaten Zutat = await _context.UpdateUserdataAsync(userParam);

            //_context.Entry(tabZutaten).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsynchron();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabZutatenExists(id))
                {
                    return NotFound("Es wurden keine Zutaten gefunden, versuchen Sie es nocheinmal!");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

        private bool TabZutatenExists(int id)
        {
            return _context.TabZutaten.Any(e => e.Id == id);
        }
    }
}