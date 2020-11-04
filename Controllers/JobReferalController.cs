using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    public class JobReferalController : ApiController
    {
        readonly private JobReferalRepo referalRepo;
        private JobReferalController()
        {
            referalRepo = new JobReferalRepo();
        }
        //[Authorize(Roles="User")]
        [HttpPost]
        public IHttpActionResult AddReferal(JobReferalViewModel jobRef)
        {
            var isSuccess = referalRepo.AddReferal(jobRef);
            return isSuccess ? Ok() : (IHttpActionResult)BadRequest();
        }

        [HttpGet]
        public IHttpActionResult GetJobDetails(int id)
        {
            var result = referalRepo.GetJobDetails(id);
            return Ok(result);
        }
    }
}
