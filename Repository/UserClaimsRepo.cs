using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class UserClaimsRepo
    {
        private readonly TestDBEntities2 db;
        public UserClaimsRepo()
        {
            db = new TestDBEntities2(); // instantiate object of TestDBEntities2 inside the contructor
        }

        // this method to get the userid from claims identity: this function will return the user id
        public int GetUserClaims(ClaimsIdentity identity)
        {
            var username = identity.Name;
            var userDetails = db.Users.FirstOrDefault(x => x.UserName == username);
            var userId = userDetails.UserId;
            return userId;
        }

        // this method will return user details for a given user id
        public UserModel FindUserDetails(int userId)
        {
            var userDetails = db.Users.FirstOrDefault(x => x.UserId == userId); // Find the user details from users table in DB

            if (userDetails == null)
            {
                return null;
            }
            UserModel userModel = new UserModel()
            {
                UserEmail = userDetails.UserEmail,
                UserName = userDetails.UserName,
                UserId = userDetails.UserId,
                Fullname = userDetails.Fullname
            };
            return userModel;
        }
    }
}