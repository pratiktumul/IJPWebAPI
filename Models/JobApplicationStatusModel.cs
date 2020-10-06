using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class JobApplicationStatusModel
    {
        public string Status { get; set; }
        public DateTime InterviewDate { get; set; }
        public string RejectReason { get; set; }
    }
}