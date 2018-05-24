using PRIT.DAL;
using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.BAL
{
    public class CandidateCourseBL
    {
        CandidateCourseDL candidateCourseDL = new CandidateCourseDL();
        PRITEntities db = new PRITEntities();

        //test merge rahul

        public void AddEditCandidateCourse(tbl_CandidateWithCourseDetails obj, string userName)
        {
           
            try
            {
                if (obj.CandidateId > 0)
                {
                    obj.ModifiedBy = db.tbl_Registration.Where(x => x.Email == userName).FirstOrDefault().UserName;
                    obj.ModifiedDate = DateTime.Now;
                }
                else
                {
                 
                    obj.IsDeleted = false;
                    obj.CreatedBy = db.tbl_Registration.Where(x => x.Email == userName).FirstOrDefault().UserName;
                    obj.CreatedDate = DateTime.Now;
                }

                candidateCourseDL.AddEditCandidateCourse(obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteCandidateCourse(int? id)
        {
            return candidateCourseDL.DeleteCandidateCourse(id);
        }
    }
}
