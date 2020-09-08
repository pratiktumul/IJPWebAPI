using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class ResumeModel
    {
        public MemoryStream Ms { get; set; }
        public string MimeType { get; set; }
    }
}