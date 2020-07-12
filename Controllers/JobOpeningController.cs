using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Controllers
{
    public class JobOpeningController : ApiController
    {
        public IHttpActionResult GetAllJobs()
        {
            using (var db = new TestDBEntities2())
            {
                List<JobModel> modelList = new List<JobModel>();
                var job_opening = db.JobOpenings.ToList();
                foreach (var list in job_opening)
                {
                    int timediff = FindTimeDiff(list.CreateDate);
                    modelList.Add(new JobModel
                    {
                        JobId = list.JobId,
                        JobTitle = list.JobTitle,
                        Location = list.Location,
                        CompanyName = list.CompanyName,
                        JobType = list.JobType,
                        TimeDiff = timediff
                    });
                }
                return Ok(modelList);
            }
        }
        [Route("api/JobOpening/search")]
        public IHttpActionResult GetJobBySearch(string title, string location)
        {
            List<JobModel> modelList = new List<JobModel>();
            using (var db = new TestDBEntities2())
            {
                var job_opening = db.JobOpenings.Where(x => x.JobTitle.Equals(title, StringComparison.OrdinalIgnoreCase) && x.Location == location).ToList();
                foreach (var list in job_opening)
                {
                    int timediff = FindTimeDiff(list.CreateDate);
                    modelList.Add(new JobModel
                    {
                        JobId = list.JobId,
                        JobTitle = list.JobTitle,
                        Location = list.Location,
                        CompanyName = list.CompanyName,
                        JobType = list.JobType,
                        TimeDiff = timediff
                    });
                }
                if (modelList.Count == 0)
                {
                    return NotFound();
                }
                return Ok(modelList);
            }
        }
        private int FindTimeDiff(DateTime? createDate)
        {
            DateTime date1 = DateTime.Now;
            TimeSpan ts = (TimeSpan)(date1 - createDate);
            return (int)ts.TotalHours;
        }
    }
}
