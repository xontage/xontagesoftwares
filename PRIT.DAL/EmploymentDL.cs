using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.DAL
{
 public   class EmploymentDL
    {
        PRITEntities db = new PRITEntities();
        public void SaveEmployment(tbl_EmploymentDetails obj)
        {
            
            try
            {
                if (obj.ID > 0)
                {
                    var entity = db.tbl_EmploymentDetails.Where(p => p.ID == obj.ID).AsQueryable().FirstOrDefault();
                    db.Entry(entity).CurrentValues.SetValues(obj);
                }
                else
                {
                    db.tbl_EmploymentDetails.Add(obj);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
