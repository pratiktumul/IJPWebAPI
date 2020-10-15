using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class RegisterRepo
    {
        private readonly IJPDBEntities db;
        public RegisterRepo()
        {
            db = new IJPDBEntities(); // create instance of DBContext class
        }

        // This method checks if the username already exists in the system; if yes then return false else add the user in system
        public bool RegisterNewUser(UserModel newUser)
        {
            // username check is not case sensitive
            var userDetail = db.Users.FirstOrDefault(x => x.UserName.Equals(newUser.UserName, StringComparison.OrdinalIgnoreCase));

            if (userDetail != null)
            {
                return false;
            }

            User user = new User()
            {
                UserName = newUser.UserName,
                UserEmail = newUser.UserEmail,
                UserPassword = Utils.HashPassword(newUser.UserPassword),
                Fullname = newUser.Fullname,
                RoleId = newUser.RoleId,
                Status = 1
            };

            db.Users.Add(user);
            db.SaveChanges();
            return true;
        }
    }
}