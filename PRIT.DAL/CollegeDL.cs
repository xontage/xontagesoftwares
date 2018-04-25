using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.DAL
{
    public class CollegeDL
    {
        PRITEntities db = new PRITEntities();

        public void AddEditCollege(tbl_Colleges obj)
        {
            
            try
            {
                if (obj.collegeId > 0)
                {
                    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.tbl_Colleges.Add(obj);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void DeleteCollege(int Id)
        {
            try
            {
                var college = db.tbl_Colleges.Where(x => x.collegeId == Id).FirstOrDefault();
                if (college != null)
                {
                    db.tbl_Colleges.Remove(college);
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
