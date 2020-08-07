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
                    RoleId = user.RoleId,
                    Status = 1
                };

                db.Users.Add(model);
                db.SaveChanges();
            }
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult PutUpdatePassword([FromUri] int id, [FromBody] UserModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new TestDBEntities2())
                {
                    var userDetails = db.Users.FirstOrDefault(x => x.UserId == id);

                    userDetails.UserPassword = Utils.HashPassword(model.UserPassword);
                    db.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
