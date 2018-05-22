using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.DAL
{
    public class CourseFeesDL
    {
        PRITEntities db = new PRITEntities();

        public long AddEditCourseFees(tbl_CourseFees obj)
        {

            try
            {
                if (obj.Id > 0)
                {                  
                    var lst = db.tbl_CourseFees.Where(Ind => Ind.Id == obj.Id).FirstOrDefault();
                    lst.PaidFees = obj.PaidFees;
                    lst.TotalPaidFees = obj.TotalPaidFees;
                    lst.RemainingFees = obj.RemainingFees;
                    lst.Remark = obj.Remark;
                    lst.ModifiedDate = obj.ModifiedDate;
                    lst.ModifiedBy = obj.ModifiedBy;                    
                    db.Configuration.ValidateOnSaveEnabled = false;

                }
                else {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_CourseFees.Add(obj);
                }
                              
                var result = db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;

                if (result >= 0)
                    return obj.Id;
                else
                    return 0;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
