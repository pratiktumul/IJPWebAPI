using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class AddSkillLevelRepo
    {
        private readonly IJPDBEntities db;
        public AddSkillLevelRepo()
        {
            db = new IJPDBEntities(); // create instance of DBContext class
        }
        public bool AddSkillLevel(EmployeeSkillLevelModel[] newSkillLevel)
        {
            foreach (var item in newSkillLevel)
            {
                employee_skill_set SkillLevel = new employee_skill_set()
                {
                    // pEmployeId = jobReferal.pEmployeId,
                    EmpId = item.EmpId,
                    Skill_Level = item.skill_level,
                    Skill_Set_Id = item.skill_set_id
                };
                db.employee_skill_set.Add(SkillLevel);
                db.SaveChanges();
            }
            return true;
        }

        public bool UpdateSkills(EmployeeSkillLevelModel[] newSkillLevel, int userId)
        {
            var empId = db.Users.Where(x => x.UserId == userId).Select(x => x.EmpId).FirstOrDefault();
            var currentSkills = db.employee_skill_set.Where(x => x.EmpId == empId).ToArray();
            foreach (var item in newSkillLevel)
            {
                employee_skill_set SkillLevel = new employee_skill_set()
                {
                    // pEmployeId = jobReferal.pEmployeId,
                    EmpId = item.EmpId,
                    Skill_Level = item.skill_level,
                    Skill_Set_Id = item.skill_set_id
                };
                db.employee_skill_set.Add(SkillLevel);
                db.SaveChanges();
            }
            return true;
        }
    }
}