using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken
{
    public class UserMasterRepository : IDisposable
    {
        IJPDBEntities db = new IJPDBEntities();
        public User ValidateUser(int empid, string password)
        {
            var hash_password = Utils.HashPassword(password);
            return db.Users.FirstOrDefault(user => user.EmpId == empid && user.UserPassword == hash_password);
        }
        public int UpdateLastlogin(int userid)
        {
            User_Log user_Log = db.User_Log.FirstOrDefault(x => x.UserId == userid);
            if (user_Log == null)
            {
                User_Log user = new User_Log()
                {
                    UserId = userid,
                    Last_Login_Date = DateTime.Now.Date,
                    Last_Job_Apply_Date = null
                };
                db.User_Log.Add(user);
                db.SaveChanges();
                return 1;
            }
            else
            {
                user_Log.Last_Login_Date = DateTime.Now.Date;
                db.SaveChanges();
                return 1;
            }
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}