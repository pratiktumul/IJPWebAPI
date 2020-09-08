using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Mail
{
    interface IEmailMessage
    {
        bool SendEmail(string Name, RegisterUpdateModel model, string email);
    }
}
