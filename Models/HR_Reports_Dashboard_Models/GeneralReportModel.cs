using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models
{
    public class GeneralReportModel
    {
        public int TotalEmployee { get; set; }
        public int TotalVacancy { get; set; }
        public int TotalApplied { get; set; }
    }
}