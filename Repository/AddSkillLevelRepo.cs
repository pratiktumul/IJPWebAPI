using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class AddSkillLevelRepo
    {
        private readonly dbEntities1 db;
        public AddSkillLevelRepo()
        {
            db = new dbEntities1(); // create instance of DBContext class
        }
        public bool AddSkillLevel(EmployeeSkillLevelModel newSkillLevel)
        {
            employee_skill_level SkillLevel = new employee_skill_level()
            {
                // pEmployeId = jobReferal.pEmployeId,
                EmpId = newSkillLevel.EmpId,
                skill_level = newSkillLevel.skill_level,
                skill_set_id = newSkillLevel.skill_set_id
               
            };
            db.employee_skill_level.Add(SkillLevel);
            db.SaveChanges();
            return true;
        }
    }
}
    
    
