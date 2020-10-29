using System.Collections.Generic;
using System.Linq;
using WebApiAuthenticationToken.Models.HR_Reports_Dashboard_Models;
using System.Data.Linq.Mapping;
using System;

namespace WebApiAuthenticationToken.Repository.HR_Reports_Dashboard_Repo
{
    public class SkillTrendRepo
    {
        private readonly IJPDBEntities db;
        public SkillTrendRepo()
        {
            db = new IJPDBEntities();
        }

        private List<SkillTrendModel> FindSkills(string[] skills)
        {
            var timePeriod = DateTime.Today.AddMonths(-1);
            string[] empSkills = db.JobApplications.Where(x => x.ApplyDate >= timePeriod).Select(x => x.Skill).ToArray();
            List<SkillTrendModel> skillTrends = new List<SkillTrendModel>();
            int skillCount = 0;
            foreach (var item in skills)
            {
                foreach(var emp_skill in empSkills)
                {
                    if (emp_skill.Contains(item))
                    {
                        skillTrends.Add(new SkillTrendModel
                        {
                            Count = ++skillCount,
                            SkillName = item
                        });
                    }
                }
                skillCount = 0;
            }
            return skillTrends;
        }

        public List<SkillTrendModel> getTrendingSkills()
        {
            List<SkillTrendModel> trendingSkills = new List<SkillTrendModel>();
            var Skills = db.tbl_Skill.Select(x => x.SkillName).ToArray();
            var skillTrends = FindSkills(Skills);
            var result = skillTrends.OrderBy(x => x.SkillName).GroupBy(x => x.SkillName).ToList();
            foreach (var item in result)
            {
                trendingSkills.Add(new SkillTrendModel
                {
                    SkillName = item.Key,
                    Count = item.Max(x => x.Count)
                });
            }

            return trendingSkills;

        }
    }
}