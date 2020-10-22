using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo;

namespace WebApiAuthenticationToken.Controllers.HR_Reports_Dashboard
{
    public class GetCompanyVacancyController : ApiController
    {
        readonly private GetCompanyVacanyRepo GetCompanyVacany;
        private GetCompanyVacancyController()
        {
            GetCompanyVacany = new GetCompanyVacanyRepo();
        }
        [HttpGet]
        [Authorize(Roles = "HR")]
        [Route("api/GenerateCompanyVacancyReport/{company_name}")]
        public IHttpActionResult GenerateCompanyVacancyReport(string company_name)
        {
            var response = GetCompanyVacany.GetCompanyVacancies(company_name);
            return Ok(response);
        }
    }
}
