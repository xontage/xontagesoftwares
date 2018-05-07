﻿using PRIT.Entity.MetaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PRIT.Entity
{
    [MetadataType(typeof(tbl_CollegesMetaModel))]
    public partial  class tbl_Colleges
    {
        public MultiSelectList Degrees { get; set; }
        public int[] DegreeIds { get; set; }
    }
}
