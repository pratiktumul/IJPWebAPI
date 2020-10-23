using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo;

namespace WebApiAuthenticationToken.Controllers.HR_Reports_Dashboard
{
    public class VacancyDetailController : ApiController
    {
        private readonly VacancyDetailRepo vacancyDetail;
        private VacancyDetailController()
        {
            vacancyDetail = new VacancyDetailRepo();
        }

        [HttpGet]
        [Authorize(Roles = "HR")]
        public IHttpActionResult JobTitleWiseCompanyVacancy(int id)
        {
            var response = vacancyDetail.CompanyWiseVacancy(id);
            return Ok(response);
        }

        [HttpGet]
        //[Authorize(Roles = "HR")]
        public IHttpActionResult CompanyList()
        {
            var response = vacancyDetail.getAllCompanies();
            return Ok(response);
        }
    }
}
