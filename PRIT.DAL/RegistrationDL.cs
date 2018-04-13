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

        /// <summary>
        /// 2017/05/18 
        /// Insert register data
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public long Registration(tbl_Registration obj)
        {

            try
            {
                if (obj.Id > 0)
                {
                    
                    var lst = db.tbl_Registration.Where(Ind => Ind.Id == obj.Id).FirstOrDefault();
                    //modify individual information                                    
                    lst.FullName = obj.FullName;
                    lst.Email = obj.Email;
                    lst.ContactNo = obj.ContactNo;
                    lst.Designation = obj.Designation;
                    lst.CollegeID = obj.CollegeID;
                    lst.RoleId = obj.RoleId;
                    lst.UserName = obj.UserName;
                    lst.Gender = obj.Gender;
                    lst.ModifiedDate = obj.ModifiedDate;
                
                    lst.ModifiedBy = obj.ModifiedBy;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    //error "An entity object cannot be referenced by multiple instances of IEntityChangeTracker
                    //db.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                }
                else
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_Registration.Add(obj);
                }
                //Save changes
                var result = db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;

                if (result >= 0)
                    return obj.Id;
                else
                    return 0;

            }
            catch (Exception ex)
            {
                return 0;
                // throw ex;
            }
        }


        public void DeleteRegisteredUser(int Id)
        {
            try
            {
                var user = db.tbl_Registration.Where(x => x.Id == Id).FirstOrDefault();
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
