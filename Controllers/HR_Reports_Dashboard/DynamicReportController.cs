using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo;

namespace WebApiAuthenticationToken.Controllers.HR_Reports_Dashboard
{
    public class DynamicReportController : ApiController
    {
        readonly private DynamicReportRepo reportRepo;
        private DynamicReportController()
        {
            reportRepo = new DynamicReportRepo();
        }

        [HttpGet]
        public IHttpActionResult TableNames()
        {
            var res = reportRepo.GetTableNames();
            return Ok(res);
        }

        [HttpGet]
        [Route("api/dynamicreport/columns")]
        public IHttpActionResult ColumnNames(string tblName)
        {
            var res = reportRepo.GetColumnNames(tblName);
            return Ok(res);
        }

        /*[HttpGet]
        [Route("api/dynamicreport/dynmaicreport")]
        public IHttpActionResult DynamicReport(string selectedCols, string tblName)
        {
            var res = reportRepo.GenerateDynamicReport(selectedCols,tblName);
            return Ok(res);
        }*/
    }
}
