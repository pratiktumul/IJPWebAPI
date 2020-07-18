using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiAuthenticationToken.Controllers
{
    public class JobsController : ApiController
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/jobs/getalljobs")]
        public IHttpActionResult GetAllJobs()
        {
            using (var db = new TestDBEntities2())
            {
                return Ok(db.JobOpenings.ToList());
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
                        CreateDate = DateTime.Now
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
