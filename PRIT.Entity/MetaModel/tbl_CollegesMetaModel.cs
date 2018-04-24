using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity.MetaModel
{
  public class tbl_CollegesMetaModel
    {
        public int collegeId { get; set; }
        public string registrationId { get; set; }
        [Required(ErrorMessage = "Please Enter College Name")]
        public string collegeName { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "You must provide a mobile number")]
        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public Nullable<long> contact1 { get; set; }
        [Display(Name = "Alternative contact No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public Nullable<long> contact2 { get; set; }
        public string website { get; set; }

        [Required(ErrorMessage = "Please Enter an Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                      ErrorMessage = "Email is not valid")]
        public string email { get; set; }
        [Required(ErrorMessage = "Please Enter TPO Name")]
        public string TPOName { get; set; }
        public string PrincipalName { get; set; }
        public string Description { get; set; }
        public string Degree { get; set; }
    }
}
