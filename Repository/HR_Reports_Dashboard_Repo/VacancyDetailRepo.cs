using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using WebApiAuthenticationToken.Models;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class VacancyDetailRepo
    {
        private readonly IJPDBEntities db;
        public VacancyDetailRepo()
        {
            db = new IJPDBEntities();
        }

        public List<VacancyDetailModel> CompanyWiseVacancy(int id)
        {
            List<VacancyDetailModel> vacancy_list = new List<VacancyDetailModel>();

            var result = db.JobOpenings.OrderBy(x => x.JobTitle).Where(x => x.company_id == id).GroupBy(x => x.JobTitle).ToList();

            foreach (var item in result)
            {
                vacancy_list.Add(new VacancyDetailModel
                {
                    JobTitle = item.Key,
                    vacancy = (int)item.Sum(x => x.Vacancy)
                });
            }
            return vacancy_list;
        }

        public List<CompanyModel> getAllCompanies()
        {
            List<CompanyModel> companies = db.companies.Select(x => new CompanyModel
            {
                Id = x.id,
                CompanyName = x.company_name
            }).ToList();

            return companies;
        }
    }
}