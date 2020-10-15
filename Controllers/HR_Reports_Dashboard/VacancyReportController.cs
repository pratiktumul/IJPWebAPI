using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo;

namespace WebApiAuthenticationToken.Controllers.HR_Reports_Dashboard
{
    public class VacancyReportController : ApiController
    {
        readonly private VacancyReportRepo vacancyReport;
        private VacancyReportController()
        {
            vacancyReport = new VacancyReportRepo();
        }
        public IHttpActionResult GetVacancyByLocation()
        {
            var response = vacancyReport.GetVacancyByLocations();
            return Ok(response);
        }
    }
}
