using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiAuthenticationToken
{
    public class UserMasterRepository : IDisposable
    {
        TestDBEntities2 context = new TestDBEntities2();
        public User ValidateUser(string username, string password)
        {
            return context.Users.FirstOrDefault(user =>
                        user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                        && user.UserPassword == password);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}