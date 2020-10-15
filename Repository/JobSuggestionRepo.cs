using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class JobSuggestionRepo
    {
        private readonly IJPDBEntities db;
        public JobSuggestionRepo()
        {
            db = new IJPDBEntities(); // create instance of DBContext class
        }

        // Method to find time difference between created job date and current date
        private int FindTimeDiff(DateTime? createDate)
        {
            DateTime date1 = DateTime.Now;
            TimeSpan ts = (TimeSpan)(date1 - createDate);
            return (int)ts.TotalHours;
        }

        // This method finds the list of all active jobs in the pipeline based on users skill set
        private List<JobModel> Find(List<int> skillId)
        {
            DateTime TodaysDate = DateTime.Now;
            List<JobModel> JobList = new List<JobModel>();
            // iterate through each skill id find which job openings require the given skill, add it to a new list and return the list
            foreach (var item in skillId)
            {
                // find which job openings require the given skill 
                var TotalJobs = db.job_posts_skill_sets.Where(x => x.skill_set_id == item).Select(x => x.job_post_id).ToList();
                foreach (var job in TotalJobs)
                {
                    // add the job in new list only if it is active
                    var JobDetail = db.JobOpenings.FirstOrDefault(x => x.JobId == job && x.IsExpired != true);
                    if (JobDetail != null)
                    {
                        if (TodaysDate <= JobDetail.LastApplyDate && JobDetail.Vacancy != 0)
                        {
                            int timediff = FindTimeDiff(JobDetail.CreateDate);
                            JobList.Add(new JobModel
                            {
                                JobId = JobDetail.JobId,
                                JobTitle = JobDetail.JobTitle,
                                Location = JobDetail.Location,
                                CompanyName = JobDetail.CompanyName,
                                JobType = JobDetail.JobType,
                                TimeDiff = timediff
                            });
                        }
                    }
                }
            }
            return JobList;
        }

        // this method returns a list of unique jobs based on the skillset of user
        public List<JobModel> FindSuggestedSkills(int id)
        {
            List<JobModel> JobList = new List<JobModel>();

            var userApplication = db.JobApplications.FirstOrDefault(x => x.UserId == id); // get the skillset string from JobApplication table
            if (userApplication != null)
            {
                string[] SkillSet = userApplication.Skill.Split(','); // spilt the skillset string into string array
                List<int> SkillId = new List<int>();
                foreach (var skill in SkillSet) // iterate through each skill in the skillset array to find out the skill id of each skill set
                {
                    var SkillDetails = db.tbl_Skill.FirstOrDefault(x => x.SkillName.Equals(skill, StringComparison.OrdinalIgnoreCase));
                    SkillId.Add(SkillDetails.SkillId); // add the skill id in skillid list
                }
                JobList = Find(SkillId); // pass the skillid list to Find method
                List<JobModel> distinct = JobList.DistinctBy(x => x.JobId).ToList(); // remove duplicate jobs from the list 
                return distinct;
            }
            return null; // return null if no job application is found for the given userid
        }
    }
}