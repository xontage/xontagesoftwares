using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity.MetaModel
{
    public class tbl_ContactMetaModel
    {     

        [Required(ErrorMessage = "Select Your Inquiry Specification")]
        public string InquirySpecification { get; set; }
        [Required(ErrorMessage = "Please Enter Your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter an Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                         ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You must provide a Contact Number")]
        //[Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public Nullable<long> ContactNo { get; set; }
        [Required(ErrorMessage = "Please Enter Message")]
        public string InquiryText { get; set; }


    }
}
