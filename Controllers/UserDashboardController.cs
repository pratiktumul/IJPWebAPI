using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    public class UserDashboardController : ApiController
    {
        private readonly UserClaimsRepo userClaims;
        private readonly UserDashboardRepo userDashboard;
        public UserDashboardController()
        {
            userClaims = new UserClaimsRepo();
            userDashboard = new UserDashboardRepo();
        }

        // HTTP Get method to get userdashboard details
        [HttpGet]
        [Authorize(Roles = "User")]
        public IHttpActionResult GetUserDashboard()
        {
            var userId = userClaims.GetUserClaims((ClaimsIdentity)User.Identity);
            UserDashboardViewModel response = userDashboard.UserDashboardSummary(userId);
            return Ok(response);
        }

        // HTTP Get method to get a list of all jobs applied by user for a given user
        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("api/userdashboard/jobsapplied/{name}")]
        public IHttpActionResult GetJobsApplied(string name)
        {
            List<AppliedJobsViewModel> response = userDashboard.AppiedJobs(name);
            return response != null ? Ok(response) : (IHttpActionResult)NotFound();
        }
    }
}