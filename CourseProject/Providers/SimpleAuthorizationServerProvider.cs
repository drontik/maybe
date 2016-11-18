using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CourseProject.Domain.Interfaces;
using Microsoft.Owin.Security.OAuth;

namespace CourseProject.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUnitOfWork repo;

        public SimpleAuthorizationServerProvider(IUnitOfWork db)
        {
            repo = db;
        }
      
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});

            var user = await repo.Users.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            if (user.Roles.Any(x => x.RoleId == "1"))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            }

            context.Validated(identity);
        }
    }
}