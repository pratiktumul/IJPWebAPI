using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    public class JobSuggestionController : ApiController
    {
        readonly UserClaimsRepo userClaimsRepo;
        readonly JobSuggestionRepo jobSuggestion;
        public JobSuggestionController()
        {
            userClaimsRepo = new UserClaimsRepo();
            jobSuggestion = new JobSuggestionRepo();
        }

        // HTTPGet request to get job suggestions based on user's skill set
        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("api/JobSuggestion")]
        public IHttpActionResult GetSuggestedSkills()
        {
            var UserId = userClaimsRepo.GetUserClaims((ClaimsIdentity)User.Identity);
            List<JobModel> response = jobSuggestion.FindSuggestedSkills(UserId);
            return response != null ? (IHttpActionResult)Ok(response) : NotFound();
        }
    }
}