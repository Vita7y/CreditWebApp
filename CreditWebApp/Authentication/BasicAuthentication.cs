using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZNetCS.AspNetCore.Authentication.Basic.Events;

namespace CreditWebApp.Authentication
{
    public class AuthenticationEvents : BasicAuthenticationEvents
    {
        public override Task ValidatePrincipalAsync(ValidatePrincipalContext context)
        {
            if ((context.UserName != "userName") || (context.Password != "password")) 
                return Task.CompletedTask;
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, context.UserName, context.Options.ClaimsIssuer)
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
            context.Principal = principal;

            return Task.CompletedTask;
        }
    }
}