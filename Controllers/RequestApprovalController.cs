using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using WebApiAuthenticationToken.Mail;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    // All methods in the controller is accessible by only Admin Role
    public class RequestApprovalController : ApiController
    {
        readonly RequestApprovalRepo requestApprovalRepo;
        public RequestApprovalController()
        {
            requestApprovalRepo = new RequestApprovalRepo();
        }

        //This HTTP Get method return a list of all registration request
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetAll()
        {
            List<UserApprovalViewModel> response = requestApprovalRepo.GetAllRegistrationRequest();
            return Ok(response);
        }

        // This HTTP Put method changes the registration status for a given user id
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutRequest(int id, RegisterUpdateModel status)
        {
            bool IsSuccess = requestApprovalRepo.ChangeStatus(id, status);
            return IsSuccess ? (IHttpActionResult)Ok() : (IHttpActionResult)NotFound();
        }
    }
}