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
    
    public partial class employee_skill_set
    {
        public int SId { get; set; }
        public Nullable<int> EmpId { get; set; }
        public Nullable<int> Skill_Set_Id { get; set; }
        public Nullable<int> Skill_Level { get; set; }
    
        public virtual tbl_Skill tbl_Skill { get; set; }
    }
}
