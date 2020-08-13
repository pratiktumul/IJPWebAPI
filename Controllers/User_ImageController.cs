using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Controllers
{
    public class User_ImageController : ApiController
    {
        readonly TestDBEntities2 db;
        public User_ImageController()
        {
            db = new TestDBEntities2();
        }
        private int UserClaims()
        {
            var user = (ClaimsIdentity)User.Identity;
            var username = user.Name;
            var userDetails = db.Users.FirstOrDefault(x => x.UserName == username);
            var userId = userDetails.UserId;
            return userId;
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public HttpResponseMessage GetCompanyImage()
        {
            var Userid = UserClaims();
            IList<User_ImageModel> list = null;
            using (var db = new TestDBEntities2())
            {
                list = db.User_Image.Select(x => new User_ImageModel()
                {
                    Id = x.Id,
                    User_id = x.UserId,
                    User_image = x.UserImage
                }).ToList();

                User_ImageModel imgModel = list.FirstOrDefault(x => x.User_id == Userid);
                if (imgModel != null)
                {
                    byte[] imgData = imgModel.User_image;
                    MemoryStream ms = new MemoryStream(imgData);
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StreamContent(ms);
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                    return response;
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Image Not Found");
            }
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public IHttpActionResult PostCompanyImage()
        {
            using (var db = new TestDBEntities2())
            {
                var Userid = UserClaims();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

                //Check if Request contains File.
                if (HttpContext.Current.Request.Files.Count == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                //Read the File data from Request.Form collection.
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

                //Convert the File data to Byte Array.
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }

                var Check = db.User_Image.FirstOrDefault(x => x.UserId == Userid);
                if (Check != null)
                {
                    Check.UserImage = bytes;
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    User_Image user_Image = new User_Image()
                    {
                        UserId = Userid,
                        UserImage = bytes
                    };
                    db.User_Image.Add(user_Image);
                    db.SaveChanges();
                    return Ok();
                }
            }
        }
        [HttpPut]
        [Authorize(Roles = "User")]
        public IHttpActionResult Put(int id)
        {
            using (var db = new TestDBEntities2())
            {
                var model = db.User_Image.FirstOrDefault(x => x.Id == id);
                if (HttpContext.Current.Request.Files.Count == 0)
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                //Read the File data from Request.Form collection.
                HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

                //Convert the File data to Byte Array.
                byte[] bytes;
                using (BinaryReader br = new BinaryReader(postedFile.InputStream))
                {
                    bytes = br.ReadBytes(postedFile.ContentLength);
                }
                if (model != null)
                {
                    model.UserImage = bytes;
                    db.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}
