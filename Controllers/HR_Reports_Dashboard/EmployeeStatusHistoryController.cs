using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo;

namespace WebApiAuthenticationToken.Controllers.HR_Reports_Dashboard
{
    public class EmployeeStatusHistoryController : ApiController
    {
        readonly private EmployeeStatusHistoryRepo statusHistoryRepo;
        private EmployeeStatusHistoryController()
        {
            statusHistoryRepo = new EmployeeStatusHistoryRepo();
        }

        [HttpGet]
        [Route("api/statushistory/loginhistory")]
        public IHttpActionResult EmployeeLoginHistory()
        {
            var response = statusHistoryRepo.LoginHistories();
            return Ok(response);
        }

        [HttpGet]
        [Route("api/statushistory/applicationhistory")]
        public IHttpActionResult EmployeeApplicationHistory()
        {
            var response = statusHistoryRepo.ApplicationHistory();
            return Ok(response);
        }
    }
}
