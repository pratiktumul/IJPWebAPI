using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models
{
    public class LoginHistoryModel
    {
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public DateTime LastLogin { get; set; }
    }
}