using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken.Models
{
    public class User_ImageModel
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public byte[] User_image { get; set; }
    }
}