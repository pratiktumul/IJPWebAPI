using System.Net;
using System.Net.Mail;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Mail
{
    public class InterviewEmail
    {
        public bool SendEmail(UserModel userModel, JobApplicationStatusModel model, JobOpening jobOpening)
        {
            if (model.Status == "3")
            {
                string subject = "Job Application Approved";
                string body = "Dear " + userModel.Fullname + ",<br/><br/>Your Job Application for " + jobOpening.JobTitle + ", " + jobOpening.CompanyName + " has been approved. Your interview has been scheduled on " + model.InterviewDate.ToString("MM/dd/yyyy")+". Further details will be communicated to your by the HR soon.<br/><br/>Regards,<br/>GyanSys";
                string to = userModel.UserEmail;

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
                    Credentials = new NetworkCredential("medicure.clinic247@gmail.com", "***")
                };
                client.Send(mm);

                return true;
            }
            else
            {
                string subject = "Job Application Rejected";
                string body = "Dear " + userModel.Fullname + ",<br/><br/>Your Job Application for " + jobOpening.JobTitle + ", " + jobOpening.CompanyName + " has been rejected. Please contact HR for help.<br/><br/>Regards,<br/>GyanSys";
                string to = userModel.UserEmail;

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
                    Credentials = new NetworkCredential("medicure.clinic247@gmail.com", "***")
                };
                client.Send(mm);

                return true;
            }
        }
    }
}