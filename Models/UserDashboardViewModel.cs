using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class UserDashboardViewModel
    {
        public int TotalJobs { get; set; }
        public int TotalApproved { get; set; }
        public int TotalRejected { get; set; }
    }
}