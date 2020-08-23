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
            DateTime TodaysDate = DateTime.Now;
            List<AdminJobs> jobs = new List<AdminJobs>();
            using (var db = new TestDBEntities2())
            {
                var list = db.JobOpenings.Where(x => x.IsExpired != true).ToList();
                foreach (var item in list)
                {
                    if (TodaysDate <= item.LastApplyDate && item.Vacancy != 0)
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
                        JobDescription = model.JobDescription,
                        IsExpired = false,
                        LastApplyDate = model.LastApplyDate,
                        Vacancy = model.Vacancy,
                        Salary = model.Salary
                    };
                    db.JobOpenings.Add(jobOpening);
                    db.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest("Check the model state");
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        //[Route("api/jobs")]
        public IHttpActionResult PutExpire(int id, IsExpired model)
        {
            using (var db = new TestDBEntities2())
            {
                var jobDetails = db.JobOpenings.FirstOrDefault(x => x.JobId == id);
                if (jobDetails != null)
                {
                    jobDetails.IsExpired = model.isExpired;
                    db.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}
