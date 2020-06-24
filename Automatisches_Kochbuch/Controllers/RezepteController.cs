using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Automatisches_Kochbuch.Context;
using Automatisches_Kochbuch.Dtos;
using Automatisches_Kochbuch.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Automatisches_Kochbuch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezepteController : ControllerBase
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public RezepteController(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //api/rezepte?ZutatenVomUser=37&ZutatenVomUser=31&ZutatenVomUser=10

        /// <summary>
        /// Es werden jene Rezepte angezeigt, die der User kochen kann.
        /// </summary>
        /// <remarks>
        /// Geben Sie die ID's der Rezepte ein.
        /// </remarks>
        /// <param name="ZutatenVomUser">
        /// Die ID's der Zutat, die der User zu Hause hat
        /// </param>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RezeptReadDto>> GetRezepte(SortedSet<int> ZutatenVomUser)
        {
            var tabZutaten = _context.MoeglicheRezepte(ZutatenVomUser);
            if ((tabZutaten != null) && (!tabZutaten.Any()))
            {
                return NotFound("Es wurde leider kein Rezept für Sie gefunden!");
            }

            return Ok(_mapper.Map<IEnumerable<RezeptReadDto>>(tabZutaten));
        }

        /// <summary>
        /// Es werden jene die Zutaten und die dazugehörige Menge des ausgewählten Rezeptes angezeigt.
        /// </summary>
        /// <remarks>
        /// Geben Sie die ID's des ausgewählten Rezept ein.
        /// </remarks>
        /// <param name="id_rezept">ID des Rezepts</param>
        /// <param name="AnzahlPersonen">Anzahl Personen bzw. Portionen des Users</param>
        //GET api/rezepte/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id_rezept}/zutaten")]
        public ActionResult<string> GetMehrZumGericht(int id_rezept, int AnzahlPersonen)
        {
            var MengeVonRezept = _context.MehrZumGericht(id_rezept, AnzahlPersonen);
            if (MengeVonRezept == null)
            {
                return NotFound("Es wurde leider kein Rezept für Sie gefunden!");
            }

            return Ok(MengeVonRezept);
        }

        /// <summary>
        /// Es wird nur ein Rezept angezeigt
        /// </summary>
        /// <param name="id">
        /// Die ID des Rezepts
        /// </param>
        // GET: api/rezepte/5
        [Authorize(Roles = Role.ADMIN)] //nur authentifizierte Admins können alle Rezepte abrufen
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTabRezept([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tabRezepte = await _context.TabRezepte.FindAsync(id);

            if (tabRezepte == null)
            {
                return NotFound("Es wurden keine Zutaten gefunden!");
            }

            return Ok(_mapper.Map<RezeptReadDto>(tabRezepte));
        }
    }
}
