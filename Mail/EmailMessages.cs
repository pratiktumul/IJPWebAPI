using System.Net;
using System.Net.Mail;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Mail
{
    public class EmailMessages : IEmailMessage
    {
        public bool SendEmail(string Name, RegisterUpdateModel model, string email)
        {
            if (model.status == "3")
            {
                string subject = "Registration Request Approved";
                string body = "Dear " + Name + ",<br/><br/>Your registration request has been approved. You can now login and apply for jobs.<br/><br/>Regards,<br/>GyanSys";
                string to = email;

                MailMessage mm = new MailMessage
                {
                    From = new MailAddress("medicure.clinic247@gmail.com")
                };
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    UseDefaultCredentials = false,
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("medicure.clinic247@gmail.com", "s/HD123gs")
                };
                client.Send(mm);

                return true;
            }
            else
            {
                string subject = "Registration Request Rejected";
                string body = "Dear " + Name + ",<br/><br/>Your registration request has been rejected. Please contact admin for help.<br/><br/>Regards,<br/>GyanSys";
                string to = "pratiktumul24@gmail.com";

                MailMessage mm = new MailMessage
                {
                    From = new MailAddress("medicure.clinic247@gmail.com")
                };
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;

                SmtpClient client = new SmtpClient("smtp.gmail.com")
                {
                    UseDefaultCredentials = false,
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("medicure.clinic247@gmail.com", "s/HD123gs")
                };
                client.Send(mm);

                return true;
            }
        }
    }
}