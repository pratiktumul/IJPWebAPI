using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace WebApiAuthenticationToken.Controllers
{
    public class UserDetailsController : ApiController
    {
        private string UserClaims()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var username = identity.Name;
            return username;
        }
        [Authorize]
        [HttpGet]
        [Route("api/UserDetails")]
        public HttpResponseMessage GetResume()
        {

            using (var db = new TestDBEntities2())
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var username = UserClaims();

                var userDetails = db.Users.FirstOrDefault(x => x.UserName == username);

                var userId = userDetails.UserId;

                var application = db.Demoes.FirstOrDefault(x => x.UserId == userId);

                if (application != null)
                {
                    var fullpath = application.Resume;
                    var mimeType = System.Web.MimeMapping.GetMimeMapping(fullpath);

                    byte[] file = File.ReadAllBytes(fullpath);
                    MemoryStream ms = new MemoryStream(file);
                    response.Content = new StreamContent(ms);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mimeType);
                    return response;
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}
