using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models
{
    public class CompanyJobVacancyModel
    {
        public string CompanyName { get; set; }
        public int? Vacancy { get; set; }
        public string JobTitle { get; set; }
    }
}