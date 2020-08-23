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
            List<AppliedJobsViewModel> list = new List<AppliedJobsViewModel>();
            if (userDetails != null)
            {
                var userId = userDetails.UserId;

                var allJobs = db.Demoes.Where(x => x.UserId == userId).ToList();
                foreach(var item in allJobs)
                {
                    var jobid = item.JobId;
                    var jobOpening = db.JobOpenings.FirstOrDefault(x=>x.JobId == jobid);
                    var companyName = jobOpening.CompanyName;
                    list.Add(new AppliedJobsViewModel
                    {
                        Id = item.Id,
                        Ename = item.Ename,
                        Curloc = item.Curloc,
                        Experience = item.Year + " Years " + item.Month + " Months",
                        About = item.About,
                        Project = item.Project,
                        Skill = item.Skill,
                        CompanyName = companyName
                    });
                }
                return Ok(list);
            }
            return NotFound();
        }
    }
}