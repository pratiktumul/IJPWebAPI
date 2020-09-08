using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    public class HRPortalController : ApiController
    {
        readonly HRPortalRepo hRPortal;

        private HRPortalController()
        {
            hRPortal = new HRPortalRepo();
        }

        // HTTP Get method to get all job applications which are not yet approved/rejected
        [HttpGet]
        [Authorize(Roles = "HR")]
        public IHttpActionResult FindAllJobApplication()
        {
            List<JobApplicationViewModel> response = hRPortal.FindAllJobs();
            return Ok(response);
        }

        // HTTP Get method to get job application details for a given job application id
        [HttpGet]
        [Authorize(Roles = "HR")]
        public IHttpActionResult FindJobApplication(int id)
        {
            JobApplicationDetailModel applicationDetail = hRPortal.FindApplicationDetail(id);
            return applicationDetail == null ? (IHttpActionResult)NotFound() : Ok(applicationDetail);
        }

        // HTTP Get method to get the resume for a given user by job appliction id
        [HttpGet]
        [Authorize(Roles = "HR")]
        [Route("api/hrportal/findresume/{applicationId}")]
        public HttpResponseMessage FindResume(int applicationId)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ResumeModel resume = hRPortal.FindResume(applicationId);
            if (resume != null)
            {
                response.Content = new StreamContent(resume.Ms);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(resume.MimeType);
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // HTTP Put method to udpate the job status and interview date
        [HttpPut]
        [Authorize(Roles = "HR")]
        public IHttpActionResult UpdateJobStatus(int id, JobApplicationStatusModel model)
        {
            bool isSuccess = hRPortal.UpdateJobStatus(id, model);
            return isSuccess ? Ok() : (IHttpActionResult)NotFound();
        }
    }
}
