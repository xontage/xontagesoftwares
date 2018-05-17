using PRIT.DAL;
using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.BAL
{
    public class CourseFeesBL
    {

        CourseFeesDL courseFeesDL = new CourseFeesDL();
        PRITEntities db = new PRITEntities();

        public void AddEditCourseFees(tbl_CourseFees obj, string userName)
        {

            try
            {                               
                    var counter = db.tbl_CourseFees.Where(m => m.CandidateEmailId == obj.CandidateEmailId).Count();
                    counter++;
                    obj.InstallmentNo = counter;
                    obj.PaidOnDate = DateTime.Now.ToString();
                    obj.CreatedBy = db.tbl_Registration.Where(x => x.Email == userName).FirstOrDefault().UserName;
                    obj.CreatedDate = DateTime.Now;
                
                courseFeesDL.AddEditCourseFees(obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
