using PRIT.Entity.MetaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.Entity
{
    
    
    
    [MetadataType(typeof(tbl_RegistrationMetaModel))]
    public  partial class tbl_Registration
    {

        /// <summary>
        /// View model class for adding extra fields required...
        /// </summary>
        
        public string CollegeName { get; set; }
       
        public string UserRoleName { get; set; }


        public bool RememberMe { get; set; }
    }
}
