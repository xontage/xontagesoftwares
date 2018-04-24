using PRIT.Entity.MetaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity
{
    [MetadataType(typeof(tbl_EmploymentDetailsMetaModel))]
    public partial class tbl_EmploymentDetails
    {
      
        public string EmployeeFullName { get; set; }
        public string EmployeeEmail { get; set; }
    }
}
