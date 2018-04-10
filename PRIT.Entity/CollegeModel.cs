using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace PRIT.Entity
{
   public partial class tbl_Colleges
    {
        public MultiSelectList Degrees { get; set; }
        public int[] DegreeIds { get; set; }      

    }
}
