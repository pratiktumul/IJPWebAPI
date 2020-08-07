using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Controllers
{
    public class JobsController : ApiController
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/jobs/getalljobs")]
        public IHttpActionResult GetAllJobs()
        {
            List<AdminJobs> jobs = new List<AdminJobs>();
            using (var db = new TestDBEntities2())
            {
                var list = db.JobOpenings.ToList();
                foreach(var item in list)
                {
                    jobs.Add(new AdminJobs
                    {
                        JobId = item.JobId,
                        JobTitle = item.JobTitle,
                        Location = item.Location,
                        CompanyName = item.CompanyName,
                        JobType = item.JobType,
                        CreateDate = (DateTime)item.CreateDate
                    });
                }
                return Ok(jobs);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/jobs/postjob")]
        public IHttpActionResult PostJob(JobOpening model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new TestDBEntities2())
                {
                    JobOpening jobOpening = new JobOpening()
                    {
                        JobTitle = model.JobTitle,
                        CompanyName = model.CompanyName,
                        Location = model.Location,
                        JobType = model.JobType,
                        CreateDate = DateTime.Now,
                        JobDescription = model.JobDescription
                    };
                    db.JobOpenings.Add(jobOpening);
                    db.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest("Check the model state");
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        //[Route("api/jobs/deletejob")]
        public IHttpActionResult DeleteJob(int id)
        {
            using (var db = new TestDBEntities2())
            {
                var jobDetails = db.JobOpenings.FirstOrDefault(x => x.JobId == id);
                if (jobDetails != null)
                {
                    db.JobOpenings.Remove(jobDetails);
                    db.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}
