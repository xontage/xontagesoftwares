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

                    #region code for updating...when for modify individual columns information ..
                    
                    var lst = db.tbl_Registration.Where(Ind => Ind.Id == obj.Id).FirstOrDefault();
                    lst.FullName = obj.FullName;
                    lst.Email = obj.Email;
                    lst.ContactNo = obj.ContactNo;
                    lst.Designation = obj.Designation;
                    lst.CollegeID = obj.CollegeID;
                    lst.RoleId = obj.RoleId;
                    lst.UserName = obj.UserName;
                    lst.Gender = obj.Gender;
                    lst.Password = obj.Password;
                    lst.UserSalt = obj.UserSalt;
                    lst.ModifiedDate = obj.ModifiedDate;
                    lst.ModifiedBy = obj.ModifiedBy;
                    db.Configuration.ValidateOnSaveEnabled = false;

                    #endregion

                    #region code for updating...when all columns of table are present on form..
                    //var entity = db.tbl_Registration.Where(p => p.Id == obj.Id).FirstOrDefault();                    
                    //db.Entry(entity).CurrentValues.SetValues(obj);
                    #endregion


                    #region code for updating...when all columns of table are allow nulls..
                    //error "An entity object cannot be referenced by multiple instances of IEntityChangeTracker
                    //db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    #endregion

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



        #region Login Functionality

        /// <summary>
        ///  - returns User Authentication for Login
        /// </summary>
        /// <param name="AccountName">Username for Login</param>
        /// <param name="Password">Password input for authenticate</param>
        /// <returns>User Authentication for Login</returns>
        public tbl_Registration GetUserDetailByMail(string Email)
        {
            tbl_Registration user = new tbl_Registration();
            try
            {
               
                user = db.tbl_Registration.Where(m => m.Email == Email && m.IsActive == true ).FirstOrDefault();
                if (user != null)
                {
                    user.UserRoleName = (from u in db.tbl_Registration
                                         join ut in db.tbl_UserRole on u.RoleId equals ut.RoleId
                                         where u.Email == Email && u.IsActive == true
                                         select ut.RolName).FirstOrDefault();
                   

                }
            }
            catch (Exception ex)
            {
                //ExceptionManager.Publish(ex, MethodBase.GetCurrentMethod().DeclaringType.Namespace + "-" + MethodBase.GetCurrentMethod().DeclaringType.Name, "FrameWork");
                return null;
            }
            return user;
        }

        #endregion


    }
}
