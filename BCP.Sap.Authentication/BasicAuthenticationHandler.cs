using BCP.Sap.Models.Autorizacion;
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

namespace BCP.Sap.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IManagerUserService _userService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IManagerUserService userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //if (!Request.Headers.ContainsKey("Authorization"))
            //    return AuthenticateResult.Fail("Missing Authorization Header");
            //if (!Request.Headers.ContainsKey("channel") || string.IsNullOrEmpty(Request.Headers.ContainsKey("channel").ToString()))
            //    return AuthenticateResult.Fail("Missing channel Header");
            //if (!Request.Headers.ContainsKey("publicToken") || string.IsNullOrEmpty(Request.Headers.ContainsKey("publicToken").ToString()))
            //    return AuthenticateResult.Fail("Missing publicToken Header");

            BasicAutorizacionModel user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                string channel = Request.Headers["channel"];
                string publicToken = Request.Headers["publicToken"];
                string appUserId = Request.Headers["appUserId"];
                user = await _userService.Authenticate(username, password, channel, publicToken, appUserId);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user == null)
            {
                //return AuthenticateResult.Fail("Invalid Username or Password");

                return AuthenticateResult.Fail("Invalid Username or Password");
            }


            try
            {
                var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                    new Claim(ClaimTypes.Name, user.userName)
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

        }
    }
}