using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    public class RegisterController : ApiController
    {
        readonly RegisterRepo register;
        public RegisterController()
        {
            register = new RegisterRepo(); // Create instance of RegisterRepo class
        }

        // HTTP Post method to add a new user into the system
        public IHttpActionResult Post(UserModel newUser)
        {
            if (ModelState.IsValid)
            {
                bool userDetail = register.RegisterNewUser(newUser);
                if (userDetail == false) // if the username is already taken by someone then return bad request
                {
                    return BadRequest("Username Taken, Please try another username");
                }
                return Ok();
            }
            return BadRequest();
        }

        /*[HttpPut]
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
        }*/
    }
}
