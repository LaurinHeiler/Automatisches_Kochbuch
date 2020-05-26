using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Automatisches_Kochbuch.Model;
using Automatisches_Kochbuch.Context;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Automatisches_Kochbuch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //Bei allen Action-Methoden wird die Authentifikation des Benutzers verlang, außer bei [AllowAnonymous].
    public class UserController : ControllerBase
    {
        private readonly AutomatischesKochbuchContext _context;

        public UserController(AutomatischesKochbuchContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Es werden alle User angezeigt
        /// </summary>
        /// <returns>
        /// Alle User
        /// </returns>
        // GET: api/User
        [HttpGet]
        [Authorize(Roles = Role.ADMIN)] //nur authentifizierte Admins können alle User abrufen
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<TabUser>>> GetAllAsync()
        {
            // Querie verwenden, um alle User zu holen
            IEnumerable<TabUser> users = await _context.GetAllAsync();

            //von der Querie erhaltene User zurückgeben
            return Ok(users);
        }

        /// <summary>
        /// Der User wird authentifiziert
        /// </summary>
        /// <remarks>
        /// Geben Sie Ihre Zugangsdaten ein.
        /// </remarks>
        // POST: api/User/authenticate
        [AllowAnonymous] //geht immer, ohne Authentication
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TabUser>> AuthenticateUserAsync([FromBody] TabUser userParam)
        {
            // Querie verwenden, um User zu authentifizieren
            TabUser user = await _context.AuthenticateAsync(userParam.Vorname, userParam.Passwort);

            //wenn die Querie keinen entsprechenden User zurückgibt, gibt es keinen mit dem entsprechden
            //Vornamen und PW
            if (user == null)
            {
                return BadRequest("Vorname oder Passwort ist falsch.");
            }

            //Von der Querie erhaltenen User zurückgeben
            return Ok(user);

        }
        /// <summary>
        /// Es wird nur ein User angezeigt
        /// </summary>
        /// <param name="id">
        /// Die ID des User
        /// </param>
        // GET: api/Zutaten/5
        //GET: api/users/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TabUser>> GetUserAsync(int id)
        {
            //Claims in der erzeugten Identity können hier verwendet werden
            //-> User können nur ihre eigenen Daten abrufen
            if (id != Convert.ToInt16(User.FindFirst(ClaimTypes.NameIdentifier).Value) &&
                !User.IsInRole(Role.ADMIN)) // Admins können jeden User abrufen
            {
                return Forbid();
            }

            TabUser user = await _context.GetUserAsync(id);

            if (user == null)
            {
                return NotFound("User doesn't exist.");
            }

            return Ok(user);
        }
        /// <summary>
        /// Es wird ein User hinzugefügt
        /// </summary>
        /// <remarks>
        /// Geben Sie Ihre gewünschten Userdaten ein
        /// </remarks>
        //POST: api/user/register
        [AllowAnonymous] //Damit jeder User seinen eigenen Account selber erstellen kann.
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TabUser>> RegisterUserAsync([FromBody] TabUser userParam)
        {
            if (userParam == null)
            {
                return BadRequest();
            }

            TabUser user = await _context.RegisterUserAsync(userParam);


            return CreatedAtAction("GetUserAsync", new { id = user.Id }, user); //Wenn registrierung erfolgreich
            //wird der Gerade erstellte User angezeigt
        }
        /// <summary>
        /// Es wird ein User geändert
        /// </summary>
        /// <param name="id">
        /// Die ID des User
        /// </param>
        //PUT: api/users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TabUser>> UpdateUserAsync(int id, [FromBody] TabUser userParam)
        {
            if (userParam == null || id != userParam.Id)
            {
                return BadRequest();
            }

            //Claims in der erzeugten Identity können hier verwendet werden
            //-> User können nur ihre eigenen Daten abrufen
            if (id != Convert.ToInt16(User.FindFirst(ClaimTypes.NameIdentifier).Value) &&
            !User.IsInRole(Role.ADMIN)) // Admins können jeden User ändern
            {
                return Forbid();
            }

            TabUser user = await _context.UpdateUserdataAsync(userParam);

            if (user == null)
            {
                return NotFound("User doesn't exist.");
            }

            return Ok(user);

        }
        /// <summary>
        /// Es wird ein User gelöscht
        /// </summary>
        /// <param name="id">
        /// Die ID des User
        /// </param>
        //DELETE: api/users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TabUser>> DeleteUserAsync(int id)
        {
            //Claims in der erzeugten Identity können hier verwendet werden
            //-> User können nur ihre eigenen Daten abrufen
            if (id != Convert.ToInt16(User.FindFirst(ClaimTypes.NameIdentifier).Value) &&
                !User.IsInRole(Role.ADMIN)) // Admins können jeden User löschen
            {
                return Forbid();
            }

            bool result = await _context.DeleteUserAsync(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound("User doesn't exist.");

        }
    }
}