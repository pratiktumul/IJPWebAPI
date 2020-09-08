using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    // All the methods in the JobsController is accessible only to Admin Role
    public class JobsController : ApiController
    {
        readonly JobsRepo jobs;
        private JobsController()
        {
            jobs = new JobsRepo();
        }

        // HTTP Get list of all active jobs for admin panel
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/jobs/getalljobs")]
        public IHttpActionResult GetAllJobs()
        {
            List<AdminJobs> ActiveJobs = jobs.AllActiveJobs(); // AllActiveJobs of Jobs class will retrive list of all active job from Job Opening table in DB
            return Ok(ActiveJobs);
        }

        // HTTP Post method to add new job in the Job Opening table
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("api/jobs/postjob")]
        public IHttpActionResult PostJob(JobOpening job)
        {
            if (ModelState.IsValid) // checking the modelstate to make sure the data added in the table is correct
            {
                bool isSuccess = jobs.AddNewJob(job); // AddNewJob of Jobs class will add new job in Job Opening table in DB
                return isSuccess ? (IHttpActionResult)Ok() : BadRequest("Internal Server Error");
            }
            return BadRequest("Check the model state");
        }

        // HTTP Put method to update job expiry that takes job id and expiry value as parameter
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutExpire(int id, IsExpired isExpiredValue)
        {
            bool isSuccess = jobs.ExpireActiveJob(id, isExpiredValue);
            return isSuccess ? (IHttpActionResult)Ok() : (IHttpActionResult)NotFound(); // return not found if no job is found for the given job id
        }
    }
}