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
    
    public partial class tbl_SubMenu
    {
        public int Id { get; set; }
        public string SubMenu { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public Nullable<int> MainMenuId { get; set; }
        public Nullable<int> RoleId { get; set; }
    
        public virtual tbl_MainMenu tbl_MainMenu { get; set; }
        public virtual tbl_UserRole tbl_UserRole { get; set; }
    }
}
