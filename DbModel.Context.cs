﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiAuthenticationToken
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TestDBEntities2 : DbContext
    {
        public TestDBEntities2()
            : base("name=TestDBEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Demo> Demoes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<JobOpening> JobOpenings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User_Log> User_Log { get; set; }
        public virtual DbSet<tbl_Skill> tbl_Skill { get; set; }
        public virtual DbSet<job_posts_skill_sets> job_posts_skill_sets { get; set; }
        public virtual DbSet<tbl_Status> tbl_Status { get; set; }
    }
}
