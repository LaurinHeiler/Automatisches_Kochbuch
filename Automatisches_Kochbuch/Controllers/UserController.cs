﻿using System;
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
using Automatisches_Kochbuch.Dtos;
using AutoMapper;

namespace Automatisches_Kochbuch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] //Bei allen Action-Methoden wird die Authentication des Benutzers verlangt, außer bei [AllowAnonymous].
    public class UserController : ControllerBase
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public UserController(IDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Es werden alle User angezeigt.
        /// </summary>
        /// <returns>
        /// Alle User
        /// </returns>
        // GET: api/User
        [HttpGet]
        [Authorize(Roles = Role.ADMIN)] //nur authentifizierte Admins können alle User abrufen
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAllUsersAsync()
        {
            // Query verwenden, um alle User zu holen
            IEnumerable<TabUser> users = await _context.GetAllUsersAsync();

            //von der Query erhaltene User zurückgeben
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        /// <summary>
        /// Der User wird authentifiziert.
        /// </summary>
        /// <remarks>
        /// Geben Sie Ihre Zugangsdaten ein.
        /// </remarks>
        //Als Beispiel diese Daten in Body bei Postman eingeben:
                	//"vorname": "Daniel",
                	//"nachname": "Romen",
                	//"passwort": "123",
                	//"Role": "admin",
                	//"Username": "DaRo"

        // POST: api/User/authenticate
        [AllowAnonymous] //geht immer, ohne Authentication
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TabUser>> AuthenticateUserAsync([FromBody] UserCreateDto userParam)
        {
            // Query verwenden, um User zu authentifizieren
            //TabUser user = await _context.AuthenticateAsync(userParam.Username, userParam.Passwort);

            //entsprechenden User aus der DB holen.
            TabUser userDB = await
            _context.TabUser.SingleOrDefaultAsync(x => x.Username == userParam.Username &&
                                                 x.Passwort == userParam.Passwort);

            var User = _mapper.Map<TabUser>(userParam);

            //falls ein User gefunden wurde, Passwort schwärzen bzw. unkenntlich machen.
            if (User != null)
                User.Passwort = null;


            //wenn die Query keinen entsprechenden User zurückgibt, gibt es keinen mit dem entsprechenden
            //Username und PW.
            if (User == null)
            {
                return BadRequest("Username oder Passwort ist falsch.");
            }

            //Von der Query erhaltenen User zurückgeben.
            return Ok(User);

        }
        /// <summary>
        /// Es wird nur ein User angezeigt.
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
        public async Task<ActionResult<UserReadDto>> GetUserAsync(int id)
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
                return NotFound("Der User existiert nicht.");
            }

            return Ok(_mapper.Map<UserReadDto>(user));
        }
        /// <summary>
        /// Es wird ein User hinzugefügt
        /// </summary>
        /// <remarks>
        /// Geben Sie Ihre gewünschten Userdaten ein
        /// </remarks>
        //POST: api/user
        [AllowAnonymous] //Damit jeder User seinen eigenen Account selber erstellen kann.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<TabUser>> RegisterUserAsync([FromBody] UserCreateDto userParam)
        {
            if (userParam == null)
            {
                return BadRequest("Irgendetwas ist schief gelaufen! Geben Sie die Parameter erneut ein.");
            }

            //TabUser user = await _context.RegisterUserAsync(User);

            //prüfen, ob der Username schon vergeben ist
            bool usernameVorhanden = await _context.TabUser.AnyAsync(x =>
                x.Username == userParam.Username);
            if (!usernameVorhanden)
            {
                //falls keine gültige Rolle angegeben wurde,
                //bekommt der neue User die Rolle "user"
                if (!Role.IsValid(userParam.Role))
                {
                    userParam.Role = Role.USER;
                }

                var User = _mapper.Map<TabUser>(userParam);

                await _context.TabUser.AddAsync(User);
                await _context.SaveChangesAsynchron();

                return CreatedAtAction("GetUserAsync", new { id = userParam.Id }, userParam); //Wenn Registrierung erfolgreich
                                                                                              //wird der eben erstellte User angezeigt
            }
            else
            {
                return BadRequest("Der Username ist schon vorhanden.");
            }

        }
        /// <summary>
        /// Es wird ein User geändert.
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
        public async Task<ActionResult<TabUser>> UpdateUserAsync(int id, [FromBody] UserUpdateDto userParam)
        {
            if (userParam == null || id != userParam.Id)
            {
                return BadRequest("Irgendetwas ist schief gelaufen! Geben Sie die Parameter erneut ein.");
            }

            //Claims in der erzeugten Identity können hier verwendet werden
            //-> User können nur ihre eigenen Daten abrufen
            if (id != Convert.ToInt16(User.FindFirst(ClaimTypes.NameIdentifier).Value) &&
            !User.IsInRole(Role.ADMIN)) // Admins können jeden User ändern
            {
                return Forbid("Das ist Ihnen nicht erlaubt!");
            }

            //TabUser user = await _context.UpdateUserdataAsync(userParam);



            //entsprechenden User aus der DB holen.
            TabUser userDB = await _context.TabUser.SingleOrDefaultAsync(u =>
            u.Id == userParam.Id);

            //falls ein User gefunden wurde, dessen Daten aktualisieren.
            if (userDB != null)
            {
                //prüfen, ob der Username geändert wurde.
                if (userDB.Username != userParam.Username)
                {
                    //prüfen, ob der neue Username noch frei ist.
                    bool usernameVorhanden = await _context.TabUser.AnyAsync(x =>
                        x.Username == userParam.Username);

                    if (usernameVorhanden)
                    {
                        return null;
                    }
                    //neuen Usernamen übernehmen
                    userDB.Username = userParam.Username;
                }

                //prüfen, ob die Rolle geändert wurde
                if (userDB.Role != userParam.Role)
                {

                    //falls keine gültige Rolle angegeben wurde,
                    //bekommt der neue User die Rolle "user"
                    string userParamRole = userParam.Role.Trim().ToLower();
                    if (!Role.IsValid(userParam.Role))
                    {
                        userParam.Role = Role.USER;
                    }
                }

                _mapper.Map(userParam, userDB);

                //Daten werden aktualisiert
                userDB.Vorname = userParam.Vorname;
                userDB.Nachname = userParam.Nachname;
                userDB.Id = userParam.Id;
                userDB.Passwort = userParam.Passwort;
                userDB.Username = userParam.Username;
                userDB.Vegetarier = userParam.Vegetarier;
                userDB.Veganer = userParam.Veganer;
                userDB.Glutenfrei = userParam.Glutenfrei;

                await _context.SaveChangesAsynchron();

                return NoContent();

            }

            return NotFound("Der User existiert nicht!");

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
                return Forbid("Das ist Ihnen nicht erlaubt!");
            }

            bool result = await _context.DeleteUserAsync(id);
            await _context.SaveChangesAsynchron();

            if (result)
            {
                return NoContent(); // Daten erfolgreich gelöscht
            }

            return NotFound("User doesn't exist.");

        }
    }
}