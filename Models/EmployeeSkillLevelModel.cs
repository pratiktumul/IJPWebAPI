using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class EmployeeSkillLevelModel
    {
        public int? EmpId { get; set; }
        public int? skill_set_id { get; set; }
        public int? skill_level { get; set; }
    }
}