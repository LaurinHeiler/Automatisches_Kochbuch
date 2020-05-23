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

namespace Automatisches_Kochbuch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //Bei allen Action-Methoden wird die Authentifikation des Benutzers verlang.
    public class UserController : ControllerBase
    {
        private readonly AutomatischesKochbuchContext _context;

        public UserController(AutomatischesKochbuchContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<TabUser>>> GetAll()
        {
            // Querie verwenden, um alle User zu holen
            IEnumerable<TabUser> users = await _context.GetAll();

            //von der Querie erhaltene User zurückgeben
            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTabUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tabUser = await _context.TabUser.FindAsync(id);

            if (tabUser == null)
            {
                return NotFound();
            }

            return Ok(tabUser);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTabUser([FromRoute] int id, [FromBody] TabUser tabUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tabUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(tabUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TabUserExists(id))
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

        // POST: api/User/authenticate
        [AllowAnonymous] //geht immer, ohne Authentication
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TabUser>> AuthenticateUser([FromBody] TabUser userParam)
        {
            // Querie verwenden, um User zu authentifizieren
            TabUser user = await _context.Authenticate(userParam.Vorname, userParam.Passwort);

            //wenn die Querie keinen entsprechenden User zurückgibt, gibt es keinen mit dem entsprechden
            //Vornamen und PW
            if (user == null)
            {
                return BadRequest("Vorname oder Passwort ist falsch.");
            }

            //Von der Querie erhaltenen User zurückgeben
            return Ok(user);

        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTabUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tabUser = await _context.TabUser.FindAsync(id);
            if (tabUser == null)
            {
                return NotFound();
            }

            _context.TabUser.Remove(tabUser);
            await _context.SaveChangesAsync();

            return Ok(tabUser);
        }

        private bool TabUserExists(int id)
        {
            return _context.TabUser.Any(e => e.Id == id);
        }
    }
}