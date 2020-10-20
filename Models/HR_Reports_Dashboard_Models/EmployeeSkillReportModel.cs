using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models
{
    public class EmployeeSkillReportModel
    {
        public string SkillSetName { get; set; }
        public int? SkillLevel { get; set; }
        public int EmpId { get; set; }
    }
}