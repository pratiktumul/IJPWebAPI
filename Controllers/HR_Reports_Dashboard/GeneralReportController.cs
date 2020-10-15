using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo;

namespace WebApiAuthenticationToken.Controllers.HR_Reports_Dashboard
{
    public class GeneralReportController : ApiController
    {
        readonly private GeneralReportRepo generalReport;
        private GeneralReportController()
        {
            generalReport = new GeneralReportRepo();
        }

        // This HTTPGet method will return general employee report
        [HttpGet]
        [Authorize(Roles = "HR")]
        public IHttpActionResult GeneralReport()
        {
            var response = generalReport.GeneralReport();
            return Ok(response);
        }
    }
}
