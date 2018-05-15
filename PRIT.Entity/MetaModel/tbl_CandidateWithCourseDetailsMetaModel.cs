using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity.MetaModel
{
    public class tbl_CandidateWithCourseDetailsMetaModel
    {
        public int CandidateId { get; set; }
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }  
        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Select Candidate Experience")]
        public string CandidateExperience { get; set; }
        [Required(ErrorMessage = "Please Provide Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please Enter Date of Birth")]
        public string DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please Enter an Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                        ErrorMessage = "Email is not valid")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "You must provide a mobile number")]
        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public long MobileNo { get; set; }
        [Display(Name = "Alternate Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public Nullable<long> AltMobileNo { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
                 
        [Required(ErrorMessage = "Please Enter Highest Education")]
        public string HighestEducation { get; set; }
       
        [Required(ErrorMessage = "Please Enter Year Of Passing")]
        public Nullable<short> YearOfPassing { get; set; }

        [Required(ErrorMessage = "Please Enter Percentage Or Grade")]
        public string PercentageOrGrade { get; set; }
       
        [Required(ErrorMessage = "Please select Course Name ")]
        public short CourseNameId { get; set; }
        [Required(ErrorMessage = "Please select Course Type ")]
        public short CourseTypeId { get; set; }
                        

    }
}
