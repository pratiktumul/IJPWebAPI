using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class EmployeeStatusHistoryRepo
    {
        readonly private IJPDBEntities db;
        public EmployeeStatusHistoryRepo()
        {
            db = new IJPDBEntities();
        }

        public List<LoginHistoryModel> LoginHistories()
        {
            List<LoginHistoryModel> empLoginHistories = new List<LoginHistoryModel>();
            var result = (from u in db.Users
                          join ul in db.User_Log on u.UserId equals ul.UserId
                          where u.RoleId == 2
                          select new
                          {
                              u.EmpId,
                              u.Fullname,
                              ul.Last_Login_Date
                          }).ToList();

            foreach (var item in result)
            {
                empLoginHistories.Add(new LoginHistoryModel
                {
                    EmpId = (int)item.EmpId,
                    FullName = item.Fullname,
                    LastLogin = item.Last_Login_Date
                });
            };
            return empLoginHistories;
        }

        public List<EmployeeApplicationTrackModel> ApplicationHistory()
        {
            List<EmployeeApplicationTrackModel> employeeApplications = new List<EmployeeApplicationTrackModel>();
            var result = (from ja in db.JobApplications
                          join jo in db.JobOpenings on ja.JobId equals jo.JobId
                          orderby ja.Ename
                          select new
                          {
                              ja.EmpId,
                              ja.Ename,
                              ja.Curloc,
                              ja.Project,
                              jo.CompanyName,
                              jo.JobTitle
                          }).ToList();
            foreach(var item in result)
            {
                employeeApplications.Add(new EmployeeApplicationTrackModel
                {
                    EmpId = (int)item.EmpId,
                    EmpName = item.Ename,
                    CurrentLocation = item.Curloc,
                    LastProject = item.Project,
                    CompanyName = item.CompanyName,
                    JobTitle = item.JobTitle
                });
            }

            return employeeApplications;
        }
    }
}