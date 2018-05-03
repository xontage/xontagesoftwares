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
        public long SaveEmployment(tbl_EmploymentDetails obj)
        {
            try
            {
                if (obj.ID > 0)
                {
                    //var entity = db.tbl_EmploymentDetails.Where(p => p.ID == obj.ID).AsQueryable().FirstOrDefault();
                    //db.Entry(entity).CurrentValues.SetValues(obj);

                    var lst = db.tbl_EmploymentDetails.Where(Ind => Ind.ID == obj.ID).FirstOrDefault();
                    lst.JoinedAs = obj.JoinedAs;
                    lst.PreviousCompanyCTC = obj.PreviousCompanyCTC;
                    lst.PreviousCompanyLastDay = obj.PreviousCompanyLastDay;
                    lst.PreviousCompanyLocation = obj.PreviousCompanyLocation;
                    lst.PreviousCompanyName = obj.PreviousCompanyName;
                    lst.ReasonForLeaving = obj.ReasonForLeaving;
                    lst.Role = obj.Role;
                    lst.DateOfJoining = obj.DateOfJoining;
                    lst.DepartmentName = obj.DepartmentName;
                    lst.CompanyName = obj.CompanyName;
                    lst.TotalRelevantExperienced = obj.TotalRelevantExperienced;
                    db.Configuration.ValidateOnSaveEnabled = false;

                }
                else
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_EmploymentDetails.Add(obj);
                }

                var result = db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = true;

                if (result >= 0)
                    return obj.ID;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tbl_EmploymentDetails GetEmploymentById(int? id)
        {
            try
            {
                var record = db.tbl_EmploymentDetails.Find(id);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteEmployment(tbl_EmploymentDetails employment)
        {
            try
            {
                // var user = db.tbl_Employee.Where(x => x.ID == emp.ID).FirstOrDefault();
                var db1 = db.tbl_EmploymentDetails.Where(u => u.ID.Equals(employment.ID)).FirstOrDefault();
                db.tbl_EmploymentDetails.Remove(db1);
                db.Configuration.ValidateOnSaveEnabled = false;
                var result = db.SaveChanges();
                db.Configuration.ValidateOnSaveEnabled = false;
                if (result >= 0)
                    return db1.ID;
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
