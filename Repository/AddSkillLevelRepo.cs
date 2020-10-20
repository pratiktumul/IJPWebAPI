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
        public bool AddSkillLevel(EmployeeSkillLevelModel newSkillLevel)
        {
            employee_skill_set SkillLevel = new employee_skill_set()
            {
                // pEmployeId = jobReferal.pEmployeId,
                EmpId = newSkillLevel.EmpId,
                Skill_Level = newSkillLevel.skill_level,
                Skill_Set_Id = newSkillLevel.skill_set_id
            };
            db.employee_skill_set.Add(SkillLevel);
            db.SaveChanges();
            return true;
        }
    }
}