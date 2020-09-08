using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    public class UserDetailsController : ApiController
    {
        readonly UserClaimsRepo userClaims;
        readonly ResumeDownloadRepo resumeDownload;
        private UserDetailsController()
        {
            resumeDownload = new ResumeDownloadRepo();
            userClaims = new UserClaimsRepo();
        }

        // HTTP Get method to get the resume for a given user id
        [Authorize]
        [HttpGet]
        [Route("api/UserDetails")]
        public HttpResponseMessage GetResume()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var userId = userClaims.GetUserClaims((ClaimsIdentity)User.Identity);
            ResumeModel resume = resumeDownload.DownloadResume(userId);

            if (resume != null)
            {
                response.Content = new StreamContent(resume.Ms);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(resume.MimeType);
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
