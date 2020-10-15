using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebApiAuthenticationToken
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (UserMasterRepository _repo = new UserMasterRepository())
            {
                var user = _repo.ValidateUser(context.UserName, context.Password);
                var roleName = "";
                var LastLoggedIn = "";
                if (user == null || (user.Status == 1 || user.Status == 2))
                {
                    if (user == null)
                    {
                        context.SetError("invalid_grant", "Provided username and password is incorrect");
                        return;
                    }
                    else if (user.Status == 1)
                    {
                        context.SetError("invalid_grant", "Registration Request is not yet approved");
                        return;
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Registration Request is rejected");
                        return;
                    }
                }
                int updateLogin = _repo.UpdateLastlogin(user.UserId);
                using (var db = new IJPDBEntities())
                {
                    var role = db.Roles.FirstOrDefault(x => x.RoleId == user.RoleId);
                    roleName = role.Rolename;
                    var logDetails = db.User_Log.FirstOrDefault(x => x.UserId == user.UserId);
                    LastLoggedIn = logDetails.Last_Login_Date.ToString();
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim("LastLogg", LastLoggedIn));
                identity.AddClaim(new Claim(ClaimTypes.Role, roleName));

                var additionalData = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "role", Newtonsoft.Json.JsonConvert.SerializeObject(roleName)
                    },
                    {
                        "username", user.UserName
                    },
                    {
                        "LastLogg", LastLoggedIn
                    }
                });
                var token = new AuthenticationTicket(identity, additionalData);
                context.Validated(token);
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}