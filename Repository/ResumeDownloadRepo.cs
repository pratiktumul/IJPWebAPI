using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class ResumeDownloadRepo
    {
        private readonly TestDBEntities2 db;

        public ResumeDownloadRepo()
        {
            db = new TestDBEntities2(); // instantiate object of TestDBEntities2 class
        }

        // This method returns resume with the mime type binded in an object and takes userid as parameter
        public ResumeModel DownloadResume(int id)
        {
            // Find if user has uploaded any resume or not
            var application = db.JobApplications.FirstOrDefault(x => x.UserId == id);

            if (application != null)
            {
                var fullpath = application.Resume;
                var mimeType = MimeMapping.GetMimeMapping(fullpath);

                // convert file into byte array
                byte[] file = File.ReadAllBytes(fullpath);
                MemoryStream ms = new MemoryStream(file);
                ResumeModel resumeModel = new ResumeModel()
                {
                    Ms = ms,
                    MimeType = mimeType
                };
                return resumeModel;
            }
            return null;
        }
    }
}