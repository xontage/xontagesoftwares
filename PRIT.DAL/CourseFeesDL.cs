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
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_CourseFees.Add(obj);                
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
