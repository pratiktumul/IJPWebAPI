using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class GeneralReportRepo
    {
        readonly private IJPDBEntities db;
        public GeneralReportRepo()
        {
            db = new IJPDBEntities(); // Create instance of DBContext Class
        }

        // This method will return an instance of GeneralReportModel Class
        public GeneralReportModel GeneralReport()
        {
            var allUsers = db.Users.Where(x => x.RoleId == 2).ToList();
            var allVacancies = db.JobOpenings.Where(x => x.Vacancy != 0 && x.IsExpired == false && x.LastApplyDate >= DateTime.Now).ToList();
            var userApplication = db.JobApplications.Where(x => x.Status == 1).ToList();
            
            GeneralReportModel model = new GeneralReportModel
            {
                TotalEmployee = allUsers.Count(),
                TotalVacancy = allVacancies.Count(),
                TotalApplied = userApplication.Count()
            };
            return model;
        }
    }
}