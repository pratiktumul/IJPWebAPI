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
    
    public partial class tbl_Skill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Skill()
        {
            this.job_posts_skill_sets = new HashSet<job_posts_skill_sets>();
        }
    
        public int SkillId { get; set; }
        public string SkillName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<job_posts_skill_sets> job_posts_skill_sets { get; set; }
    }
}
