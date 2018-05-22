using PRIT.Entity.MetaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity
{
    [MetadataType(typeof(tbl_CourseFeesMetaModel))]
    public partial class tbl_CourseFees
    {
        public string CourseName { get; set; }
        public string  DurationName { get; set; }
    }
}
