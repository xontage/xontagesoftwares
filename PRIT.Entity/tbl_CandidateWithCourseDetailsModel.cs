using PRIT.Entity.MetaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity
{
    [MetadataType(typeof(tbl_CandidateWithCourseDetailsMetaModel))]
    public partial class tbl_CandidateWithCourseDetails
    {
        public string Status { get; set; }
        public string CourseName { get; set; }

        public string CourseType { get; set; }
        public string DurationName { get; set; }
        public string CourseCategoryName { get; set; }
    }
}
