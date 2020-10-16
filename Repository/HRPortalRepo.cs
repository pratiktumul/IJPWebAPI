using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using WebApiAuthenticationToken.Mail;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class HRPortalRepo
    {
        private readonly dbEntities1 db;
        private readonly InterviewEmail interview;
        public HRPortalRepo()
        {
            interview = new InterviewEmail();
            db = new dbEntities1(); // Create instance of DBContext Class
        }

        // This methos updates job opening table when HR approves a job application
        private bool UpdateVacancy(int JobId)
        {
            var JobDetails = db.JobOpenings.FirstOrDefault(x => x.JobId == JobId);
            if(JobDetails.Vacancy > 0)
            {
                JobDetails.Vacancy -= 1; // job vacancy for the given job id will be decremented by 1
                db.SaveChanges();
                return true;
            }
            return true;
        }

        // This method will return a list of all applied jobs
        public List<JobApplicationViewModel> FindAllJobs()
        {
            List<JobApplicationViewModel> jobApplications = new List<JobApplicationViewModel>();
            // Join JobApplications table and JobOpening table to get a list of all applied jobs
            var queryList = (from ja in db.JobApplications
                             join jo in db.JobOpenings on ja.JobId equals jo.JobId
                             where ja.Status == 1
                             select new
                             {
                                 ja.Id,
                                 ja.Ename,
                                 ja.UserId,
                                 jo.CompanyName,
                                 jo.JobTitle,
                                 jo.JobId
                             }).ToList();

            foreach (var item in queryList)
            {
                jobApplications.Add(new JobApplicationViewModel
                {
                    ApplicationId = item.Id,
                    EmployeeName = item.Ename,
                    UserId = (int)item.UserId,
                    CompanyName = item.CompanyName,
                    JobTitle = item.JobTitle,
                    JobId = item.JobId
                });
            }
            return jobApplications;
        }

        // This method takes application id as parameter and return memorystream of file for the given application id
        public ResumeModel FindResume(int applicationId)
        {
            var application = db.JobApplications.FirstOrDefault(x => x.Id == applicationId);

            if (application != null)
            {
                var fullpath = application.Resume;
                var mimeType = MimeMapping.GetMimeMapping(fullpath); // fetch the path of resume from jobApplication table

                byte[] file = File.ReadAllBytes(fullpath);
                MemoryStream ms = new MemoryStream(file); // convert the byte array to memorystream and return it along with the mime type
                ResumeModel resumeModel = new ResumeModel()
                {
                    Ms = ms,
                    MimeType = mimeType
                };
                return resumeModel;
            }
            return null;
        }

        // This method return the job application details for a given job application id
        public JobApplicationDetailModel FindApplicationDetail(int id)
        {
            //Join JobApplications table and JobOpening table to get the details of job application
            var jobApplicationDetail = (from ja in db.JobApplications
                                        join jo in db.JobOpenings on ja.JobId equals jo.JobId
                                        where ja.Id == id
                                        select new JobApplicationDetailModel
                                        {
                                            Id = ja.Id,
                                            Ename = ja.Ename,
                                            Curloc = ja.Curloc,
                                            Year = ja.Year,
                                            Month = ja.Month,
                                            About = ja.About,
                                            Project = ja.Project,
                                            Skill = ja.Skill,
                                            UserId = ja.UserId,
                                            JobId = ja.JobId,
                                            JobTitle = jo.JobTitle,
                                            CompanyName = jo.CompanyName,
                                            Location = jo.CompanyName,
                                            JobType = jo.JobType,
                                            JobDescription = jo.JobDescription
                                        }).FirstOrDefault();

            return jobApplicationDetail ?? null; // return job application details else null if not found
        }

        // This method updates the job application status and interview date for a given job application id
        public bool UpdateJobStatus(int id, JobApplicationStatusModel model)
        {
            // Find job application details for the given id
            var jobApplication = db.JobApplications.FirstOrDefault(x => x.Id == id);

            if (jobApplication != null) // check if jobapplication is not null; if null return false
            {
                var userId = jobApplication.UserId;
                var jobId = jobApplication.JobId;
                if (model.Status == "2") // status "2" means rejected
                {
                    jobApplication.InterviewDate = null;
                    jobApplication.Status = Convert.ToInt32(model.Status);
                    db.SaveChanges();
                    var userDetails = db.Users.Where(x => x.UserId == userId).Select(x => new UserModel
                    {
                        UserEmail = x.UserEmail,
                        Fullname = x.Fullname
                    }).FirstOrDefault();
                    var companyName = db.JobOpenings.FirstOrDefault(x => x.JobId == jobId);
                    //interview.SendEmail(userDetails, model, companyName);
                    return true;
                }
                else if (model.Status == "3") // status "3" means approved
                {
                    jobApplication.InterviewDate = model.InterviewDate;
                    jobApplication.Status = Convert.ToInt32(model.Status);
                    db.SaveChanges();
                    UpdateVacancy((int)jobApplication.JobId); // reduce job vacancy by 1
                    var userDetails = db.Users.Where(x => x.UserId == userId).Select(x => new UserModel
                    {
                        UserEmail = x.UserEmail,
                        Fullname = x.Fullname
                    }).FirstOrDefault();
                    var companyName = db.JobOpenings.FirstOrDefault(x => x.JobId == jobId);
                    //interview.SendEmail(userDetails, model, companyName);
                    return true;
                }
            }
            return false;
        }
    }
}