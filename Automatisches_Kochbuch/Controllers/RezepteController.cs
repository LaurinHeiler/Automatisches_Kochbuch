using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Automatisches_Kochbuch.Context;
using Automatisches_Kochbuch.Dtos;
using Automatisches_Kochbuch.Model;
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
    }
}
