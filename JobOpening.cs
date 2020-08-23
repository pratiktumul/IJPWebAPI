//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class JobOpening
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobOpening()
        {
            this.job_posts_skill_sets = new HashSet<job_posts_skill_sets>();
            this.Demoes = new HashSet<Demo>();
        }
    
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string JobType { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string JobDescription { get; set; }
        public Nullable<bool> IsExpired { get; set; }
        public Nullable<int> Vacancy { get; set; }
        public Nullable<System.DateTime> LastApplyDate { get; set; }
        public Nullable<int> Salary { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<job_posts_skill_sets> job_posts_skill_sets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Demo> Demoes { get; set; }
    }
}
