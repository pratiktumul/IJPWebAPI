using System.Collections.Generic;
using System.Linq;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class UserDashboardRepo
    {
        private readonly dbEntities1 db;
        public UserDashboardRepo()
        {
            db = new dbEntities1();
        }

        // This method return an object that returns total jobs applied, approved and rejected
        public UserDashboardViewModel UserDashboardSummary(int userId)
        {
            var userApplications = db.JobApplications.Where(x => x.UserId == userId).ToList();
            var totalJobs = userApplications.Count();
            var totalApproved = userApplications.Where(x => x.Status == 3).Count();
            var totalRejected = userApplications.Where(x => x.Status == 2).Count();
            UserDashboardViewModel model = new UserDashboardViewModel
            {
                TotalJobs = totalJobs,
                TotalApproved = totalApproved,
                TotalRejected = totalRejected
            };
            return model;
        }

        // This method return a list of all jobs applied
        public List<AppliedJobsViewModel> AppiedJobs(string name)
        {
            var userDetails = db.Users.FirstOrDefault(x => x.UserName == name);
            List<AppliedJobsViewModel> list = new List<AppliedJobsViewModel>();
            if (userDetails != null)
            {
                var userId = userDetails.UserId;

                list = (from ja in db.JobApplications
                        join status in db.tbl_Status on ja.Status equals status.StatusId
                        join jo in db.JobOpenings on ja.JobId equals jo.JobId
                        where ja.UserId == userId
                        select new AppliedJobsViewModel
                        {
                            Id = ja.Id,
                            Ename = ja.Ename,
                            Curloc = ja.Curloc,
                            Year = (int)ja.Year,
                            Month = (int)ja.Month,
                            Skill = ja.Skill,
                            Status = status.Status,
                            CompanyName = jo.CompanyName,
                            JobTitle = jo.JobTitle
                        }).ToList();
                return list;
            }
            return null;
        }
    }
}