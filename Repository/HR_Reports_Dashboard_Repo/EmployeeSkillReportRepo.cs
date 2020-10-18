using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class EmployeeSkillReportRepo
    {
        readonly private dbEntities1 db;
        public EmployeeSkillReportRepo()
        {
            db = new dbEntities1(); // Create instance of DBContext Class
        }
        public List<EmployeSkillReportModel> EmployeeSkillReport(int empid)
        {
            var employeeId = db.employee_skill_level.Where(x => x.EmpId == empid);
            List<EmployeSkillReportModel> Employeedetails = new List<EmployeSkillReportModel>();
            //var job_opening = db.JobOpenings.Where(x => x.IsExpired != true).ToList(); // get list of all the jobs which have not been marked expired
            foreach (var item in employeeId) //loop through each item in job_opening and add it to ActiveJobs list
            {

                Employeedetails.Add(new EmployeSkillReportModel
                    {
                        SkillLevel = item.skill_level,
                        SkillSetId = item.skill_set_id,
                       
                    });
                
            }
            return Employeedetails;
        }
    }
}