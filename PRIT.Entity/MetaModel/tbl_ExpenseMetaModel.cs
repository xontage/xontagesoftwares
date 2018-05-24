using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity.MetaModel
{
   public class tbl_ExpenseMetaModel
    {
        [Required(ErrorMessage = "Please select Expense Type")]
        public string ExpenseType { get; set; }
        [Required(ErrorMessage = "Please Enter Amount")]
        public long Amount { get; set; }
        
    }
}
