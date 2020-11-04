using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Mail
{
    interface IEmailMessage
    {
        bool SendEmail(string Name, RegisterUpdateModel model, string email);
    }
}
