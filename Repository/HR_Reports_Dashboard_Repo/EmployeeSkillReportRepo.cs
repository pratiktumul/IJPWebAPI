﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;
using Microsoft.Ajax.Utilities;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class EmployeeSkillReportRepo
    {
        readonly private IJPDBEntities db;
        public EmployeeSkillReportRepo()
        {
            db = new IJPDBEntities(); // Create instance of DBContext Class
        }

        // This method will return a list of employee id along with the skill sets
        public List<EmployeeSkillReportModel> EmployeeSkillReport(int empId)
        {
            List<EmployeeSkillReportModel> Employeedetails = new List<EmployeeSkillReportModel>();

            var result = (from e in db.employee_skill_set
                          join s in db.tbl_Skill on e.Skill_Set_Id equals s.SkillId
                          where e.EmpId == empId
                          select new
                          {
                              e.EmpId,
                              e.Skill_Level,
                              s.SkillName
                          }).ToList();

            foreach (var item in result)
            {

                Employeedetails.Add(new EmployeeSkillReportModel
                {
                    SkillLevel = item.Skill_Level,
                    SkillSetName = item.SkillName,
                    EmpId = (int)item.EmpId
                });

            }
            var res = Employeedetails.OrderByDescending(x => x.SkillLevel);
            List<EmployeeSkillReportModel> distinct = res.DistinctBy(x => x.SkillSetName).ToList();
            return distinct;
        }
    }
}