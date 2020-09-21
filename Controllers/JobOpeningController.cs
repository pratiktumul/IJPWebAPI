using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    // All HTTP methods inside the controller are decorated with AllowAnonymous filter because active jobs in the pipeline can be seen by anyone
    public class JobOpeningController : ApiController
    {
        readonly JobOpeningRepo jobOpening;
        private JobOpeningController()
        {
            jobOpening = new JobOpeningRepo();
        }
        
        // HttpGet method to get list of all active jobs
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAllJobs()
        {
            List<JobModel> ActiveJobs = jobOpening.FindAllActiveJobs();
            return Ok(ActiveJobs);
        }

        //HttpGet method to find a job based on keywords like job title and  company location
        [AllowAnonymous]
        [Route("api/JobOpening/search")]
        public IHttpActionResult GetJobBySearch(string title, string location)
        {
            if(title == null || location == null)
            {
                return NotFound();
            }
            List<JobModel> ActiveJobs = jobOpening.FindJobBySearch(title, location);
            return ActiveJobs.Count == 0 ? NotFound() : (IHttpActionResult)Ok(ActiveJobs);
        }

        // HTTPGet method to find the job details by passing job id in the method as parameter
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetJobById(int id)
        {
            JobViewModel jobViewModel = jobOpening.FindJobById(id);
            return jobViewModel == null ? NotFound() : (IHttpActionResult)Ok(jobViewModel);
        }
    }
}