using Microsoft.Owin.Security.OAuth;
using MundoCompilado.MinhaMeta.Api.App_Start;
using MundoCompilado.MinhaMeta.Domain;
using MundoCompilado.MinhaMeta.Model.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MundoCompilado.MinhaMeta.Api.Providers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var domain = (IDomain<User>)Startup.Resolver.GetService(typeof(IDomain<User>));

            var usuario = domain.Get().SingleOrDefault(a => a.Email == context.UserName && a.Password == context.Password);
            if (usuario == null)
            {
                context.SetError("invalid_grant", "Usuário ou senha incorreto.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }
    }
}
