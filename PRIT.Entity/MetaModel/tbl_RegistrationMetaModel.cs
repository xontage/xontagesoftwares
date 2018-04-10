using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity.MetaModel
{
    public class tbl_RegistrationMetaModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter an Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Full Name")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }
        public Nullable<long> ContactNo { get; set; }
        public string Designation { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> RoleId { get; set; }
        public Nullable<int> CollegeID { get; set; }
        public string Gender { get; set; }

    }
}
