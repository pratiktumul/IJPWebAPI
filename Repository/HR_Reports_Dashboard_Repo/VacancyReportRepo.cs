﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class VacancyReportRepo
    {
        readonly private IJPDBEntities db;
        public VacancyReportRepo()
        {
            db = new IJPDBEntities();
        }
        public List<VacancyByLocationModel> GetVacancyByLocations()
        {
            List<VacancyByLocationModel> vacancy_list = new List<VacancyByLocationModel>();
            var result = db.JobOpenings.OrderBy(x => x.Location).GroupBy(x => x.Location).ToList();
            foreach (var item in result)
            {
                vacancy_list.Add(new VacancyByLocationModel
                {
                    Location = item.Key,
                    Vacancy = (int)item.Sum(x => x.Vacancy)
                });
            }
            return vacancy_list;
        }
        public List<VacancyByCompanyModel> GetVacancyByCompany()
        {
            List<VacancyByCompanyModel> vacancy_list = new List<VacancyByCompanyModel>();
            var result = db.JobOpenings.OrderBy(x => x.CompanyName).GroupBy(x => x.CompanyName).Where(x => x.Count() != 0).ToList();
            foreach (var item in result)
            {
                vacancy_list.Add(new VacancyByCompanyModel
                {
                    Company = item.Key,
                    Vacancy = (int)item.Sum(x => x.Vacancy)
                });
            }
            return vacancy_list;
        }
    }
}