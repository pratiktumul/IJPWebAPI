using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class EmployeeApplicationTrackModel
    {
        public string EmpName { get; set; }
        public string CurrentLocation { get; set; }
        public string LastProject { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
    }
}