using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class JobOpeningRepo
    {
        private readonly dbEntities1 db;
        public JobOpeningRepo()
        {
            db = new dbEntities1(); // create instance of DBContext class
        }

        // Funtion to find time difference between created job date and current date
        private int FindTimeDiff(DateTime? createDate)
        {
            DateTime date1 = DateTime.Now;
            TimeSpan ts = (TimeSpan)(date1 - createDate);
            return (int)ts.TotalHours; //returns number of hours
        }

        // This method returns a list of all active jobs in Job Opening table
        public List<JobModel> FindAllActiveJobs()
        {
            DateTime TodaysDate = DateTime.Now; // use current date time to compare with last date to apply
            List<JobModel> ActiveJobs = new List<JobModel>();
            var job_opening = db.JobOpenings.Where(x => x.IsExpired != true).ToList(); // get list of all the jobs which have not been marked expired
            foreach (var item in job_opening) //loop through each item in job_opening and add it to ActiveJobs list
            {
                if (TodaysDate <= item.LastApplyDate && item.Vacancy != 0) // add a job in ActiveJobs list only if it is still active and vacancy is not filled
                {
                    int timediff = FindTimeDiff(item.CreateDate);
                    ActiveJobs.Add(new JobModel
                    {
                        JobId = item.JobId,
                        JobTitle = item.JobTitle,
                        Location = item.Location,
                        CompanyName = item.CompanyName,
                        JobType = item.JobType,
                        TimeDiff = timediff,
                        Salary = item.Salary,
                    });
                }
            }
            return ActiveJobs;
        }

        // This method returns a list of active jobs based on keywords (job title and  company location)
        public List<JobModel> FindJobBySearch(string title, string location)
        {
            DateTime TodaysDate = DateTime.Now;
            List<JobModel> ActiveJobs = new List<JobModel>();
            // get a list of all jobs from job opening table in DB based on keyword( search is not case sensitive )
            var job_opening = db.JobOpenings.Where(x => x.JobTitle.Equals(title, StringComparison.OrdinalIgnoreCase) && x.Location == location).ToList();
            foreach (var item in job_opening) //loop through each item in job_opening and add it to ActiveJobs list
            {
                if (TodaysDate <= item.LastApplyDate && item.Vacancy != 0) // add a job in ActiveJobs list only if it is still active and vacancy is not filled
                {
                    int timediff = FindTimeDiff(item.CreateDate);
                    ActiveJobs.Add(new JobModel
                    {
                        JobId = item.JobId,
                        JobTitle = item.JobTitle,
                        Location = item.Location,
                        CompanyName = item.CompanyName,
                        JobType = item.JobType,
                        TimeDiff = timediff,
                        Salary = item.Salary,
                    });
                }
            }
            return ActiveJobs;
        }

        // This method returns the job details by passing job id in the method as parameter
        public JobViewModel FindJobById(int id)
        {
            using (var db = new dbEntities1())
            {
                var JobDetails = db.JobOpenings.FirstOrDefault(x => x.JobId == id);
                if (JobDetails != null)
                {
                    var CreateDate = (DateTime)JobDetails.CreateDate;
                    var PostedJobDate = CreateDate.ToString("MM/dd/yyyy"); // change date format of job posting date
                    JobViewModel jobViewModel = new JobViewModel() // add and return job details of given job id
                    {
                        JobId = JobDetails.JobId,
                        JobTitle = JobDetails.JobTitle,
                        CompanyName = JobDetails.CompanyName,
                        Location = JobDetails.Location,
                        JobDescription = JobDetails.JobDescription,
                        JobType = JobDetails.JobType,
                        PostedDate = PostedJobDate,
                        Vacancy = JobDetails.Vacancy,
                        Salary = JobDetails.Salary,
                        ApplicationDate = (DateTime)JobDetails.LastApplyDate
                    };
                    return jobViewModel;
                }
                return null;
            }
        }
    }
}