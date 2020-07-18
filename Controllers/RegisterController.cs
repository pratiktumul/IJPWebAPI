using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Controllers
{
    public class RegisterController : ApiController
    {
        public IHttpActionResult Post(UserModel user)
        {
            using (var db = new TestDBEntities2())
            {

                var userDetail = db.Users.FirstOrDefault(x => x.UserName.Equals(user.UserName, StringComparison.OrdinalIgnoreCase));

                if (userDetail != null)
                {
                    return BadRequest("Username Taken, Please try another username");
                }

                User model = new User()
                {
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserPassword = Utils.HashPassword(user.UserPassword),
                    Fullname = user.Fullname,
                    RoleId = user.RoleId
                };

                db.Users.Add(model);
                db.SaveChanges();
            }
            return Ok();
        }
    }
}
