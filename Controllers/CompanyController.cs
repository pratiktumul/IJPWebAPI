using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiAuthenticationToken.Controllers
{
    public class CompanyController : ApiController
        
    {
        private IJPDBEntities db = new IJPDBEntities();
        [Route("api/GetCompanyNames")]
        public IEnumerable<company> GetCompanyNames()
        {
            return db.companies.ToList();
        }
    }
}
