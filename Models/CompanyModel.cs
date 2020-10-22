using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class CompanyModel
    {
        public int id { get; set; }
        public string company_name { get; set; }
        public string profile_description { get; set; }
        public string establishment_date { get; set; }
        public string company_website_url { get; set; }
    }
}