using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 3,ErrorMessage="Password should be between 3 to 15 characters")]
        [Display(Name = "Password") ]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "You must provide a Contact Number")]
        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public Nullable<long> ContactNo { get; set; }
        [Required(ErrorMessage = "Select Your Designation")]
        public string Designation { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        [Required(ErrorMessage = "Please Enter Role Name")]
        public Nullable<int> RoleId { get; set; }

        //[MustBeSelected(ErrorMessage = "Please Select College Name")]
        [Required(ErrorMessage = "Please Select College Name")]
        public Nullable<int> CollegeID { get; set; }

        [Required(ErrorMessage = "Please Provide Gender")]
        public string Gender { get; set; }

    }


    public class MustBeSelected : ValidationAttribute, IClientValidatable // IClientValidatable for client side Validation
    {
        public override bool IsValid(object value)
        {
            if (value == null || (int)value == 0)
                return false;
            else
                return true;
        }
        // Implement IClientValidatable for client side Validation
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRule { ValidationType = "dropdown", ErrorMessage = this.ErrorMessage } };
        }
    }

    /// <summary>
    /// loginview model is for storing login credential
    /// </summary>
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        // [Display(Name = "Remember me?")]
        // public bool RememberMe { get; set; }
    }

}
