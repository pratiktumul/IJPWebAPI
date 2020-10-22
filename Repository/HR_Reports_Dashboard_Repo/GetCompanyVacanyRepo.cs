using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class GetCompanyVacanyRepo
    {
        private readonly IJPDBEntities db;
        public GetCompanyVacanyRepo()
        {
            db = new IJPDBEntities(); // Create instance of DBContext Class
        }
        public List<CompanyJobVacancyModel> GetCompanyVacancies(string company_name)
        {
            List<CompanyJobVacancyModel> jobApplications = new List<CompanyJobVacancyModel>();
            // Join JobApplications table and JobOpening table to get a list of all applied jobs
            var queryList = (from ja in db.companies
                             join jo in db.JobOpenings on ja.company_name equals jo.CompanyName
                             where ja.company_name == company_name
                             select new
                             {
                                 
                                 jo.CompanyName,
                                 jo.JobTitle,
                                 jo.Vacancy
                             }).ToList();

            foreach (var item in queryList)
            {
                jobApplications.Add(new CompanyJobVacancyModel
                {
                    
                    CompanyName = item.CompanyName,
                    JobTitle = item.JobTitle,
                    Vacancy = item.Vacancy
                });
            }
            return jobApplications;
        }
    }
}