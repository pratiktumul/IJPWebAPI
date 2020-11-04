using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class JobApplicationViewModel
    {
        public int ApplicationId { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public int UserId { get; set; }
        public int JobId { get; set; }
        public int? EmpId { get; set; }
        public string Skills { get; set; }

    }
}