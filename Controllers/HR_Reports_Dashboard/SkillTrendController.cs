using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo;

namespace WebApiAuthenticationToken.Controllers.HR_Reports_Dashboard
{
    public class SkillTrendController : ApiController
    {
        readonly private SkillTrendRepo skillTrend;
        private SkillTrendController()
        {
            skillTrend = new SkillTrendRepo();
        }

        [HttpGet]
        public IHttpActionResult SkillTrendChart()
        {
            var result = skillTrend.getTrendingSkills();
            return Ok(result);
        }
    }
}
