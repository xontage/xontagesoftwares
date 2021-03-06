﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity.MetaModel
{
   public class tbl_EmployeeMetaModel
    {
        
        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }
       

        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Provide Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "You must provide a Marital Status")]
        public string MaritalStatus { get; set; }
        [Required(ErrorMessage = "Please Enter Date of Birth")]
        public string DateofBirth { get; set; }

        [Required(ErrorMessage = "Please Enter an Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "Email is not valid")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "You must provide a Designation")]
        public string Designation { get; set; }

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
       
       
        [Required(ErrorMessage = "Please Enter Pincode")]

        [RegularExpression(@"^\d{6}(-\d{4})?$", ErrorMessage = "Please Enter Valid Postal Code.")]
        public long Pincode { get; set; }
        
        
       

        [Required(ErrorMessage = "Please Enter Highest Education")]
        public string HighestEducation { get; set; }
      
       
        [Required(ErrorMessage = "Please Enter Year Of Passing")]
        public int YearOfPassing { get; set; }
       
    
        [Required(ErrorMessage = "Please Enter Employee Type")]
        public string EmployeeType { get; set; }

        [Required(ErrorMessage = "Please Enter Adhar Card No")]
        public long AdharCardNo { get; set; }
       
        [Required(ErrorMessage = "Please Enter PAN No")]
        public string PANNo { get; set; }
      
    }
}
