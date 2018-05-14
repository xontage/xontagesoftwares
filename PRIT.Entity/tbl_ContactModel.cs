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

        public ExportToExcel exportToExcel { get; set; }
    }

    public class ExportToExcel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string InquirySpecification { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Nullable<long> ContactNo { get; set; }
        public string InquiryText
        {
            get; set;
        }
    }
}
