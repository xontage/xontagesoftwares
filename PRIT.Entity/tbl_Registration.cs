//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PRIT.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Registration
    {
        public int Id { get; set; }
        public System.Guid Guid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserSalt { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public long ContactNo { get; set; }
        public string Designation { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> CollegeID { get; set; }
        public string Gender { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual tbl_Colleges tbl_Colleges { get; set; }
        public virtual tbl_UserRole tbl_UserRole { get; set; }
    }
}
