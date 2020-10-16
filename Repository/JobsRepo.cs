using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class JobsRepo
    {
        private readonly dbEntities1 db;
        public JobsRepo()
        {
            db = new dbEntities1(); // instantiate object of TestDBEntities2 inside the contructor
        }

        //this method return a list of all active jobs to the Jobs Controller
        public List<AdminJobs> AllActiveJobs()
        {
            DateTime TodaysDate = DateTime.Now; // use current date time to compare with last date to apply
            List<AdminJobs> ActiveJobs = new List<AdminJobs>();
            var JobOpeningList = db.JobOpenings.Where(x => x.IsExpired != true).ToList(); // get list of all the jobs which have not been marked expired
            foreach (var item in JobOpeningList) //loop through each item in job_opening and add it to ActiveJobs list
            {
                if (TodaysDate <= item.LastApplyDate && item.Vacancy != 0) // add a job in ActiveJobs list only if it is still active and vacancy is not filled
                {
                    ActiveJobs.Add(new AdminJobs
                    {
                        JobId = item.JobId,
                        JobTitle = item.JobTitle,
                        Location = item.Location,
                        CompanyName = item.CompanyName,
                        JobType = item.JobType,
                        CreateDate = (DateTime)item.CreateDate
                    });
                }
            }
            return ActiveJobs;
        }

        //this method returns boolean value true if a new job is successfully added else false
        public bool AddNewJob(JobOpening job)
        {
            try
            {
                JobOpening jobOpening = new JobOpening()
                {
                    JobTitle = job.JobTitle,
                    CompanyName = job.CompanyName,
                    Location = job.Location,
                    JobType = job.JobType,
                    CreateDate = DateTime.Now,
                    JobDescription = job.JobDescription,
                    IsExpired = false, // IsExpired Column will be ByDefault set to false: Admin can change the value explicitly later on
                    LastApplyDate = job.LastApplyDate,
                    Vacancy = job.Vacancy,
                    Salary = job.Salary
                };
                db.JobOpenings.Add(jobOpening);
                db.SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        // this method will return true is expiry status is successfully changed; false if no job is found for the given Job id
        public bool ExpireActiveJob(int id, IsExpired isExpiredValue)
        {
            var jobDetails = db.JobOpenings.FirstOrDefault(x => x.JobId == id);
            if (jobDetails == null)
            {
                return false; // return not found if no job is found for the given job id
            }
            jobDetails.IsExpired = isExpiredValue.isExpired; // update IsExpired Column in JobOpening table in DB when admin explicitly marks it as expired
            db.SaveChanges();
            return true;
        }
    }
}