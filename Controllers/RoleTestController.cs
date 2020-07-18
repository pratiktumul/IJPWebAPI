using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiAuthenticationToken.Controllers
{
    public class RoleTestController : ApiController
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/ForAdminRole")]
        public string ForAdminRole()
        {
            return "for admin role";
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("api/ForAuthorRole")]
        public string ForAuthorRole()
        {
            return "For user role";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        [Route("api/ForAuthorOrReader")]
        public string ForAuthorOrReader()
        {
            return "For admin/user role";
        }
    }
}
