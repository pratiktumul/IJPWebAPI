using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    public class AddSkillLevelController : ApiController
    {
        readonly AddSkillLevelRepo AddSkillLevel;
        public AddSkillLevelController()
        {
            AddSkillLevel = new AddSkillLevelRepo(); // Create instance of RegisterRepo class
        }

        [HttpPost]
        [Route("api/PostEmployeeSkills")]
        public IHttpActionResult PostEmployeeSkillLevel(EmployeeSkillLevelModel newSkillLevel)
        {
            if (ModelState.IsValid) // checking the modelstate to make sure the data added in the table is correct
            {
                bool isSuccess = AddSkillLevel.AddSkillLevel(newSkillLevel); // AddSkillLevel of AddSkillLevel class will add new skills to employee skill table in DB
                return isSuccess ? (IHttpActionResult)Ok() : BadRequest("Internal Server Error");
            }
            return BadRequest("Check the model state");
        }

    }
}
