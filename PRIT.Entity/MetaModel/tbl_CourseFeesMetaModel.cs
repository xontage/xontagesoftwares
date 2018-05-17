﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity.MetaModel
{
   public  class tbl_CourseFeesMetaModel
    {
        [Required(ErrorMessage = "Please Enter an Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                       @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                       @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                       ErrorMessage = "Email is not valid")]
        public string CandidateEmailId { get; set; }
        [Required(ErrorMessage = "You must provide a Number")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int PaidFees { get; set; }
    }
}
