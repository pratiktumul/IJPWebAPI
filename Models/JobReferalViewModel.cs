using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class JobReferalViewModel
    {
        public string pEmailId { get; set; }
        public string pPhoneNo { get; set; }
        public string pJobName { get; set; }
        public string pLocation { get; set; }
        public string pSkillSet { get; set; }
        public string pName { get; set; }
        public int JobId { get; set; }
    }
}