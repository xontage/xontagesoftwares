using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity.ViewModels
{
   public class MyProfileViewModel
    {
        public IEnumerable<tbl_Registration> Registration { get; set; }
        public IEnumerable<tbl_Employee> Employee { get; set; }
        public IEnumerable<tbl_EmploymentDetails> EmploymentDetails { get; set; }
       
    }
}
