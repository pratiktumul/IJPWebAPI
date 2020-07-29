using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Controllers
{
    public class UserDashboardController : ApiController
    {
        readonly TestDBEntities2 db;
        public UserDashboardController()
        {
            db = new TestDBEntities2();
        }
        private int UserClaims()
        {
            var user = (ClaimsIdentity)User.Identity;
            var username = user.Name;
            var userDetails = db.Users.FirstOrDefault(x => x.UserName == username);
            var userId = userDetails.UserId;
            return userId;
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public IHttpActionResult GetUserDashboard()
        {
            var userId = UserClaims();

            var totalJobs = db.Demoes.Where(x => x.UserId == userId).Count();
            UserDashboardViewModel model = new UserDashboardViewModel();
            model.totalJobs = totalJobs;
            return Ok(model);
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("api/userdashboard/jobsapplied/{name}")]
        public IHttpActionResult GetJobsApplied(string name)
        {
            var userDetails = db.Users.FirstOrDefault(x => x.UserName == name);
            if (userDetails != null)
            {
                var userId = userDetails.UserId;

                var allJobs = db.Demoes.Where(x => x.UserId == userId).Select(x => new AppliedJobsViewModel()
                {
                    Id = x.Id,
                    Ename = x.Ename,
                    Curloc = x.Curloc,
                    Experience = x.Year + " Years " + x.Month + " Months",
                    About = x.About,
                    Project = x.Project,
                    Skill = x.Skill
                }).ToList();
                return Ok(allJobs);
            }
            return NotFound();
        }
    }
}
