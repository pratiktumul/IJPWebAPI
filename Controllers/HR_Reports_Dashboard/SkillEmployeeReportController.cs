using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo;

namespace WebApiAuthenticationToken.Controllers.HR_Reports_Dashboard
{
    public class SkillEmployeeReportController : ApiController
    {
        private readonly SkillEmployeeRepo skillEmployee;
        private SkillEmployeeReportController()
        {
            skillEmployee = new SkillEmployeeRepo();
        }

        [HttpGet]
        public IHttpActionResult SKillEmployeeReport()
        {
            var res = skillEmployee.Skill_Employee_Report();
            return Ok(res);
        }
    }
}
