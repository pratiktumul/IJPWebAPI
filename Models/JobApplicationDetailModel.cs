using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class JobApplicationDetailModel
    {
        public int Id { get; set; }
        public string Ename { get; set; }
        public string Curloc { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public string About { get; set; }
        public string Project { get; set; }
        public string Skill { get; set; }
        public int? UserId { get; set; }
        public int? JobId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string JobType { get; set; }
        public string JobDescription { get; set; }
    }
}