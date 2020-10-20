using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;
using WebApiAuthenticationToken.Repository;
using WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo;

namespace WebApiAuthenticationToken.Controllers.HR_Reports_Dashboard
{
    public class EmployeeSkillReportController : ApiController
    {
        readonly EmployeeSkillReportRepo EmployeeSkillReportRepo;
        readonly UserClaimsRepo userClaimsRepo;
        private EmployeeSkillReportController()
        {
            userClaimsRepo = new UserClaimsRepo();
            EmployeeSkillReportRepo = new EmployeeSkillReportRepo();
        }
        
        [HttpGet]
        [Authorize(Roles = "User")]
        public IHttpActionResult GetEmployeeSkillDetails()
        {
            var UserId = userClaimsRepo.GetUserClaims((ClaimsIdentity)User.Identity);
            List<EmployeeSkillReportModel> Employeedetails = EmployeeSkillReportRepo.EmployeeSkillReport(UserId);
            return Ok(Employeedetails);
        }
    }
}
