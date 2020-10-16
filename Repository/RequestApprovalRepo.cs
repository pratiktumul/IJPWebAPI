using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Mail;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class RequestApprovalRepo
    {
        readonly dbEntities1 db;
        readonly EmailMessages emailMessages;
        public RequestApprovalRepo()
        {
            db = new dbEntities1();
            emailMessages = new EmailMessages();
        }

        // This method returns list of all registration request where status is applied and role is user
        public List<UserApprovalViewModel> GetAllRegistrationRequest()
        {
            var RequestList = db.Users.Where(x => x.Status == 1 && (x.RoleId == 2 || x.RoleId == 4)).ToList();

            List<UserApprovalViewModel> registationList = new List<UserApprovalViewModel>();

            foreach (var items in RequestList)
            {
                var Role = db.Roles.FirstOrDefault(x => x.RoleId == items.RoleId);
                var Rolename = Role.Rolename;
                registationList.Add(new UserApprovalViewModel
                {
                    UserId = items.UserId,
                    UserName = items.UserName,
                    Fullname = items.Fullname,
                    UserEmail = items.UserEmail,
                    RoleName = Rolename
                });
            }
            return registationList;
        }

        // This method changes the approval status from applied to approved or rejected
        public bool ChangeStatus(int id, RegisterUpdateModel status)
        {
            var UserDetails = db.Users.FirstOrDefault(x => x.UserId == id);
            if (UserDetails == null) // udpate the registration request if user is found in the system
            {
                return false;
            }
            UserDetails.Status = Convert.ToInt32(status.status);
            db.SaveChanges();
            emailMessages.SendEmail(UserDetails.Fullname, status, UserDetails.UserEmail); // this will send auto-generated email to the user
            return true;
        }
    }
}