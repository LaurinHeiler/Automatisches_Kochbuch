using Automatisches_Kochbuch.Context;
using Automatisches_Kochbuch.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Automatisches_Kochbuch.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IDataContext _context;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock, IDataContext userQuer)
            : base(options, logger, encoder, clock)
        {
            _context = userQuer;
        }

        //Jedes Mal, wenn eine Action-Methode nach Authentication verlangt,
        //wird diese Methode ausgeführt.
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Überprüfen, ob der Authorization-Header vorhanden ist.
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            TabUser user;

            try
            {
                //Wert des Authentiaction-Header holen
                AuthenticationHeaderValue authHeader =
                    AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                //Parameter im Authorization-Header (liefert die Credentials)
                //dekodieren, in einen string umwandeln und diesen beim ":" teilen.
                // -> Credentials haben die Form "username:password"
                byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);

                string username = credentials[0];
                string password = credentials[1];

                //Query verwenden, um User zu authentifizieren.
                user = await _context.AuthenticateAsync(username, password);

            }
            //Sollte etwas mit dem Authentication-Header nicht klappen.
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            //Wenn die Query keinen entsprechenden User zurückgibt,
            //gibt es keinen mit dem entsprechenden Benutzernamen und PW.
            if (user == null)
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            //Claims erzeugen und in einem Array sammeln.
            //Claims ... Informationen über den authentifizierten Benutzer,
            //           die in der Action-Methode dann verwendet werden können.
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            //Aus den Claims eine Identity und ein AuthenticationTicket
            //für die Rückgabe erzeugen.
            ClaimsIdentity identity = new ClaimsIdentity(claims, Scheme.Name);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

            //Authentifizierung erfolgreich -> erzeugtes Ticket zurückgeben.
            return AuthenticateResult.Success(ticket);
        }
    }
}
