using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.DAL
{
    public class CandidateCourseDL
    {
        PRITEntities db = new PRITEntities();

        public long AddEditCandidateCourse(tbl_CandidateWithCourseDetails obj)
        {
            
            try
            {

                if (obj.CandidateId > 0)
                {
                    var lst = db.tbl_CandidateWithCourseDetails.Where(Ind => Ind.CandidateId == obj.CandidateId).FirstOrDefault();
                    
                    lst.FirstName = obj.FirstName;
                    lst.MiddleName = obj.MiddleName;
                    lst.LastName = obj.LastName;
                    lst.CandidateExperience = obj.CandidateExperience;
                    lst.Gender = obj.Gender;
                    lst.DateOfBirth = obj.DateOfBirth;
                    lst.EmailId = obj.EmailId;
                    lst.MobileNo = obj.MobileNo;
                    lst.AltMobileNo = obj.AltMobileNo;

                    lst.Address = obj.Address;
                    lst.HighestEducation = obj.HighestEducation;
                    lst.Specialization = obj.Specialization;
                    lst.InstituteName = obj.InstituteName;
                    lst.YearOfPassing = obj.YearOfPassing;
                    lst.University = obj.University;
                    lst.PercentageOrGrade = obj.PercentageOrGrade;

                    lst.CourseNameId = obj.CourseNameId;
                    lst.CourseTypeId = obj.CourseTypeId;
                    lst.HoursPerDay = obj.HoursPerDay;
                    lst.Fees = obj.Fees;
                    lst.Duration = obj.Duration;
                    lst.CourseCategory = obj.CourseCategory;
                 
                    
                    lst.ModifiedBy = obj.ModifiedBy;
                    lst.ModifiedDate = obj.ModifiedDate;

                    db.Configuration.ValidateOnSaveEnabled = false;
                   
                }
                else
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_CandidateWithCourseDetails.Add(obj);
                }
                var result = db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;

                if (result >= 0)
                    return obj.CandidateId;
                else
                    return 0;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteCandidateCourse(int? id)
        {
            try
            {
                // var user = db.tbl_Employee.Where(x => x.ID == emp.ID).FirstOrDefault();
                var db1 = db.tbl_CandidateWithCourseDetails.Where(u => u.CandidateId==id).FirstOrDefault();
                db1.IsDeleted = true;
                db.Configuration.ValidateOnSaveEnabled = false;
                var result = db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = false;
                if (result >= 0)
                    return db1.CandidateId;
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
