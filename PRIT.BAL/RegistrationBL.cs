using PRIT.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRIT.Entity;

namespace PRIT.BAL
{
   
    public class RegistrationBL
    {
        RegistrationDL registrationDL = new RegistrationDL();
        PRITEntities db = new PRITEntities();
        
        public void Registration(tbl_Registration obj,string userName)
        {
            try
            {
                if (obj.Id > 0)
                {
                    obj.ModifiedDate = DateTime.Now;
                    obj.IsActive = obj.IsActive;
                    obj.ModifiedBy = userName;
                }
                else {
                    obj.CreatedDate = DateTime.Now;
                    obj.IsActive = true;
                    obj.CreatedBy = userName;
                }

                registrationDL.Registration(obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void DeleteRegisteredUser(int Id)
        {
            registrationDL.DeleteRegisteredUser(Id);
        }

     
        


    }
}
