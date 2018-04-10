using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.DAL
{
    public class RegistrationDL
    {
        PRITEntities db = new PRITEntities();

        public void Registration(tbl_Registration obj)
        {
            try
            {
                if (obj.Id > 0)
                {
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.tbl_Registration.Add(obj);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void DeleteRegisteredUser(int Id)
        {
            try
            {
              var user=  db.tbl_Registration.Where(x => x.Id == Id).FirstOrDefault();
                if (user != null)
                {
                    db.tbl_Registration.Remove(user);
                    db.SaveChanges();

                }
             
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
