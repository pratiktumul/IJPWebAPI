﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiAuthenticationToken.Models;

namespace WebApiAuthenticationToken.Repository
{
    public class JobReferalRepo
    {
        readonly private IJPDBEntities db;
        public JobReferalRepo()
        {
            db = new IJPDBEntities();
        }

        public bool AddReferal(JobReferalViewModel jobReferal)
        {
            RefTable refTablev = new RefTable()
            {
               // pEmployeId = jobReferal.pEmployeId,
                pEmailId = jobReferal.pEmailId,
                pLocation = jobReferal.pLocation,
                pJobName = jobReferal.pJobName,
                pPhoneNo = jobReferal.pPhoneNo,
                pSkillSet = jobReferal.pSkillSet,
                pName = jobReferal.pName

            };
            db.RefTables.Add(refTablev);
            db.SaveChanges();
            return true;
        }
    }
}