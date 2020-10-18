using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;
using WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo;

namespace WebApiAuthenticationToken.Controllers.HR_Reports_Dashboard
{
    public class EmployeeSkillReportController : ApiController
    {
        readonly EmployeeSkillReportRepo EmployeeSkillReportRepo;
        private EmployeeSkillReportController()
        {
            EmployeeSkillReportRepo = new EmployeeSkillReportRepo();
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("api/GetEmployeeSkillDetails/{Empid}")]
        public IHttpActionResult GetEmployeeSkillDetails(int Empid)
        {
            List<EmployeSkillReportModel> Employeedetails = EmployeeSkillReportRepo.EmployeeSkillReport(Empid);
            return Ok(Employeedetails);
        }
    }
}
