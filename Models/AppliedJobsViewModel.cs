using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class AppliedJobsViewModel
    {
        public int Id { get; set; }
        public string Ename { get; set; }
        public string Curloc { get; set; }
        public string Experience { get; set; }
        public string About { get; set; }
        public string Project { get; set; }
        public string Skill { get; set; }
        public string CompanyName { get; set; }
    }
}