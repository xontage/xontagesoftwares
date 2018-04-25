using PRIT.DAL;
using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.BAL
{
    public class ContactsBL
    {
        
        ContactDL contactDL = new ContactDL();
        public void SaveContact(tbl_Contact obj)
        {
            try
            {
                contactDL.SaveContact(obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteContact(int contactId)
        {
            try
            {
                contactDL.DeleteContact(contactId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
