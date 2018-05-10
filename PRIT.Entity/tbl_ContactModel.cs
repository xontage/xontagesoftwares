using PRIT.Entity.MetaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity
{
    [MetadataType(typeof(tbl_ContactMetaModel))]
    public partial class tbl_Contact
    {
        public string Actions { get; set; }
    }
}
