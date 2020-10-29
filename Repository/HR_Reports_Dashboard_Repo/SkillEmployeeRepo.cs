using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class SkillEmployeeRepo
    {
        private readonly IJPDBEntities db;
        public SkillEmployeeRepo()
        {
            db = new IJPDBEntities();
        }

        public List<SkillEmpModel> Skill_Employee_Report()
        {
            List<SkillEmpModel> skillEmps = new List<SkillEmpModel>();
            var res = (from e in db.employee_skill_set
                       join ts in db.tbl_Skill on e.Skill_Set_Id equals ts.SkillId
                       group ts by ts.SkillName into g
                       select new SkillEmpModel
                       {
                           Skills = g.Key,
                           EmpCount = g.Count()
                       }).ToList();

            return res;
        }
    }
}