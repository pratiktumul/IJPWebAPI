using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAuthenticationToken.Mail;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Controllers
{
    public class RequestApprovalController : ApiController
    {
        readonly TestDBEntities2 db;
        EmailMessages emailMessages;
        public RequestApprovalController()
        {
            db = new TestDBEntities2();
            emailMessages = new EmailMessages();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetAll()
        {
            var RequestList = db.Users.Where(x => x.Status == 1).ToList();

            List<UserApprovalViewModel> list = new List<UserApprovalViewModel>();

            foreach (var items in RequestList)
            {
                var Role = db.Roles.FirstOrDefault(x => x.RoleId == items.RoleId);
                var Rolename = Role.Rolename;
                list.Add(new UserApprovalViewModel
                {
                    UserId = items.UserId,
                    UserName = items.UserName,
                    Fullname = items.Fullname,
                    UserEmail = items.UserEmail,
                    RoleName = Rolename
                });
            }
            return Ok(list);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult PutRequest(int id, RegisterUpdateModel model)
        {
            var UserDetails = db.Users.FirstOrDefault(x => x.UserId == id);
            if (UserDetails != null)
            {
                UserDetails.Status = Convert.ToInt32(model.status);
                db.SaveChanges();
                emailMessages.SendEmail(UserDetails.Fullname, model.status);
                return Ok();
            }
            return NotFound();
        }
    }
}
