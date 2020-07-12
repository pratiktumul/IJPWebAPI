using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Controllers
{
    public class EmployeeJobApplicationController : ApiController
    {

        private string UserClaims()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var username = identity.Name;
            return username;
        }
        [Authorize]
        [HttpPost]
        [Route("api/EmployeeJobApplication")]
        public IHttpActionResult PostDemo()
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["Resume"];
            string fileName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Resume/" + fileName);
            postedFile.SaveAs(filePath);

            using (var db = new TestDBEntities2())
            {
                var username = UserClaims();

                var userDetails = db.Users.FirstOrDefault(x => x.UserName == username);

                var userId = userDetails.UserId;

                var y = httpRequest["Year"];
                int year = Convert.ToInt32(y);
                var m = httpRequest["Month"];
                int month = Convert.ToInt32(m);
                Demo demo = new Demo()
                {
                    Ename = httpRequest["Ename"],
                    Curloc = httpRequest["Curloc"],
                    Skill = httpRequest["Skill"],
                    Year = year,
                    Month = month,
                    About = httpRequest["About"],
                    Project = httpRequest["Project"],
                    Resume = filePath,
                    UserId = userId
                };
                db.Demoes.Add(demo);
                db.SaveChanges();
            }
            return Ok();
        }
        [Authorize]
        [HttpGet]
        [Route("api/EmployeeJobApplication")]
        public IHttpActionResult GetUserDetails()
        {
            using (var db = new TestDBEntities2())
            {
                var username = UserClaims();

                var userDetails = db.Users.FirstOrDefault(x => x.UserName == username);

                UserModel model = new UserModel()
                {
                    UserEmail = userDetails.UserEmail,
                    UserName = userDetails.UserName,
                    UserId = userDetails.UserId,
                    Fullname = userDetails.Fullname
                };
                return Ok(model);
            }
        }
    }
}
