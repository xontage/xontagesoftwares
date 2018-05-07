using PRIT.Entity.MetaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PRIT.Entity
{

    [MetadataType(typeof(tbl_EmployeeMetaModel))]
    public partial class tbl_Employee
    {
        public string Status { get; set; }
        public MultiSelectList Skillset { get; set; }
        public int[] SkillIds { get; set; }
    }
}
