﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Mail;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    public class SuperAdminController : ApiController
    {
        readonly SuperAdminRepo superAdmin;
        public SuperAdminController()
        {
            superAdmin = new SuperAdminRepo();
        }

        // HTTP Get method to get a list of admin registration requests
        [HttpGet]
        [Authorize(Roles = "Superadmin")]
        public IHttpActionResult GetAll()
        {
            List<UserApprovalViewModel> response = superAdmin.GetAllAdminRequest();
            return Ok(response);
        }

        // HTTP Put method to update admin's registration request
        [HttpPut]
        [Authorize(Roles = "Superadmin")]
        public IHttpActionResult PutRequest(int id, RegisterUpdateModel model)
        {
            bool IsSuccess = superAdmin.UpdateRegisterRequest(id, model);
            return IsSuccess ? Ok() : (IHttpActionResult)NotFound();
        }
    }
}
