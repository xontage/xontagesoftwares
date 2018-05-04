using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PRIT.Entity
{
    public partial class tbl_Employee
    {
        public MultiSelectList Skillset { get; set; }
        public int[] SkillIds { get; set; }
    }
}
