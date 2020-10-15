using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Repository;

namespace WebApiAuthenticationToken.Controllers
{
    public class User_ImageController : ApiController
    {
        readonly IJPDBEntities db;
        readonly UserClaimsRepo userClaims;
        public User_ImageController()
        {
            userClaims = new UserClaimsRepo();
            db = new IJPDBEntities();
        }

        // HTTP Get method to get user's image for a given user id for User Role
        [HttpGet]
        [Authorize(Roles = "User")]
        public HttpResponseMessage GeUserImage()
        {
            var Userid = userClaims.GetUserClaims((ClaimsIdentity)User.Identity);
            //Get a list of all the images from the DB
            IList<User_ImageModel> list = null;
            list = db.User_Image.Select(x => new User_ImageModel()
            {
                Id = x.Id,
                User_id = x.UserId,
                User_image = x.UserImage
            }).ToList();

            // Find image for the given user id from the list, convert it to bytes array and attach it to response message.
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

        // HTTP Get method to get user's image for a given user id for HR Role
        [HttpGet]
        [Authorize(Roles = "HR")]
        public HttpResponseMessage GeUserImageById(int id)
        {
            //Get a list of all the images from the DB
            IList<User_ImageModel> list = null;
            list = db.User_Image.Select(x => new User_ImageModel()
            {
                Id = x.Id,
                User_id = x.UserId,
                User_image = x.UserImage
            }).ToList();

            // Find image for the given user id from the list, convert it to bytes array and attach it to response message.
            User_ImageModel imgModel = list.FirstOrDefault(x => x.User_id == id);
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

        // HTTP Post method to Add/Update user's image for a given user id for User Role
        [HttpPost]
        [Authorize(Roles = "User")]
        public IHttpActionResult PostUserImage()
        {
            var Userid = userClaims.GetUserClaims((ClaimsIdentity)User.Identity);
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
}
