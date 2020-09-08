using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class JobModel
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string JobType { get; set; }
        public int? TimeDiff { get; set; }
        public string JobDescription { get; set; }
        public int? Vacancy { get; set; }
        public int? Salary { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}