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
    
    public partial class tbl_EmploymentDetails
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public string DateOfJoining { get; set; }
        public string JoinedAs { get; set; }
        public string TotalRelevantExperienced { get; set; }
        public string PreviousCompanyName { get; set; }
        public string PreviousCompanyLocation { get; set; }
        public string PreviousCompanyCTC { get; set; }
        public string ReasonForLeaving { get; set; }
        public string PreviousCompanyLastDay { get; set; }
        public Nullable<int> EmplD { get; set; }
    
        public virtual tbl_Employee tbl_Employee { get; set; }
    }
}
