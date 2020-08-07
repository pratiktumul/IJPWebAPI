using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Web.Http;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Controllers
{
    public class JobSuggestionController : ApiController
    {
        readonly TestDBEntities2 db;
        public JobSuggestionController()
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
        private int FindTimeDiff(DateTime? createDate)
        {
            DateTime date1 = DateTime.Now;
            TimeSpan ts = (TimeSpan)(date1 - createDate);
            return (int)ts.TotalHours;
        }
        private List<JobModel> Find(List<int> list)
        {
            List<JobModel> JobList = new List<JobModel>();
            foreach (var item in list)
            {
                var TotalJobs = db.job_posts_skill_sets.Where(x => x.skill_set_id == item).Select(x => x.job_post_id).ToList();
                foreach (var job in TotalJobs)
                {
                    var JobDetail = db.JobOpenings.FirstOrDefault(x => x.JobId == job);
                    int timediff = FindTimeDiff(JobDetail.CreateDate);
                    JobList.Add(new JobModel
                    {
                        JobId = JobDetail.JobId,
                        JobTitle = JobDetail.JobTitle,
                        Location = JobDetail.Location,
                        CompanyName = JobDetail.CompanyName,
                        JobType = JobDetail.JobType,
                        TimeDiff = timediff
                    });
                }
            }
            return JobList;
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("api/JobSuggestion")]
        public IHttpActionResult GetSuggestedSkills()
        {
            List<JobModel> JobList = new List<JobModel>();

            var UserId = UserClaims();
            var userApplication = db.Demoes.FirstOrDefault(x => x.UserId == UserId);
            if (userApplication != null)
            {
                string[] SkillSet = userApplication.Skill.Split(',');
                List<int> SkillId = new List<int>();
                foreach (var skill in SkillSet)
                {
                    var SkillDetails = db.tbl_Skill.FirstOrDefault(x => x.SkillName.Equals(skill, StringComparison.OrdinalIgnoreCase));
                    SkillId.Add(SkillDetails.SkillId);
                }
                JobList = Find(SkillId);
                List<JobModel> distinct = JobList.DistinctBy(x => x.JobId).ToList();
                return Ok(distinct);
            }
            return NotFound();
        }
    }
}