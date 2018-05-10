using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity.MetaModel
{
   public class tbl_EmploymentDetailsMetaModel
    {
        public int ID { get; set; }
        public int EmplD { get; set; }
        [Required(ErrorMessage = "Please Enter Department Name")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Please Enter Company Name")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Please Select Role ")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Please Select Date Of Joining")]

        public string DateOfJoining { get; set; }

        [Required(ErrorMessage = "Please Select Joined As")]
        public string JoinedAs { get; set; }

        [Required(ErrorMessage = "Please Enter Total Relevant Experienced")]
        public string TotalRelevantExperienced { get; set; }

        [Required(ErrorMessage = "Please Enter Previous Company Name")]
        public string PreviousCompanyName { get; set; }
        [Required(ErrorMessage = "Please Enter Previous Company Location")]
        public string PreviousCompanyLocation { get; set; }
        [Required(ErrorMessage = "Please Enter Previous Company CTC")]
        [Range(36000, 10000000, ErrorMessage = "Salary must be between 36000 and 10000000")]
        public string PreviousCompanyCTC { get; set; }
        [Required(ErrorMessage = "Please Enter Reason For Leaving")]
        public string ReasonForLeaving { get; set; }
        [Required(ErrorMessage = "Please Enter Previous Company Last Day")]
        public System.DateTime PreviousCompanyLastDay { get; set; }
    }
}
