using System.Collections.Generic;
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
        
        [HttpGet]
        [Authorize(Roles = "HR")]
        public IHttpActionResult GetEmployeeSkillDetails(int id)
        {
            List<EmployeeSkillReportModel> Employeedetails = EmployeeSkillReportRepo.EmployeeSkillReport(id);
            return Ok(Employeedetails);
        }
    }
}
