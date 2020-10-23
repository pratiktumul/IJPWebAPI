using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using WebApiAuthenticationToken.Mail;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class SuperAdminRepo
    {
        private readonly SuperAdminMail superAdmin;
        private readonly IJPDBEntities db;
        public SuperAdminRepo()
        {
            superAdmin = new SuperAdminMail();
            db = new IJPDBEntities(); // Create instance of DBContext Class
        }

        // This method will return a list of all pending admin registration requests
        public List<UserApprovalViewModel> GetAllAdminRequest()
        {
            List<UserApprovalViewModel> list = (from user in db.Users
                                                join role in db.Roles on user.RoleId equals role.RoleId
                                                where user.RoleId == 1 && user.Status == 1
                                                select new UserApprovalViewModel
                                                {
                                                    UserId = user.UserId,
                                                    UserName = user.UserName,
                                                    UserEmail = user.UserEmail,
                                                    Fullname = user.Fullname,
                                                    RoleName = role.Rolename
                                                }).ToList();
            return list;
        }

        // This method will return true if status is updated else false if no user if found for the given user id
        public bool UpdateRegisterRequest(int id, RegisterUpdateModel model)
        {
            var UserDetails = db.Users.FirstOrDefault(x => x.UserId == id);
            if (UserDetails != null)
            {
                UserDetails.Status = Convert.ToInt32(model.status);
                db.SaveChanges();
                superAdmin.SendEmail(UserDetails.Fullname, model, UserDetails.UserEmail); // this will send auto-generated email to the admin
                return true;
            }
            return false;
        }
        public bool CheckDomain(string email)
        {
            string domain = "gyansys.com";
            int indexOfAt = email.IndexOf("@");
            string extractDomain = email.Substring(indexOfAt + 1);
            return domain.Equals(extractDomain);
        }
        public bool RegisterNewUser(UserModel newUser)
        {
            // username check is not case sensitive
            var userDetail = db.Users.FirstOrDefault(x => x.UserName.Equals(newUser.UserName, StringComparison.OrdinalIgnoreCase));

            if (userDetail != null)
            {
                return false;
            }

            var isSuccess = CheckDomain(newUser.UserEmail);

            User user = new User()
            {
                UserName = newUser.UserName,
                UserEmail = newUser.UserEmail,
                UserPassword = Utils.HashPassword(newUser.UserPassword),
                Fullname = newUser.Fullname,
                EmpId = newUser.EmpId,
                RoleId = newUser.RoleId,
                Status = isSuccess ? 3 : 1,
            };

            db.Users.Add(user);
            db.SaveChanges();
            return true;
        }

    }
}