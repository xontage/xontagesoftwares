using PRIT.DAL;
using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.BAL
{
  public  class EmploymentBL
    {
        EmploymentDL employmentDL = new EmploymentDL();
        PRITEntities db = new PRITEntities();
        public void AddEmployment(tbl_EmploymentDetails obj, string userName,int employeeId)
        {
            try
            {
                if (obj.ID > 0)
                {
                    obj.ModifiedBy = db.tbl_Registration.Where(x => x.Email == userName).FirstOrDefault().UserName;
                    obj.ModifiedDate = DateTime.Now;
                    employmentDL.SaveEmployment(obj);
                }
                else
                {
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedBy = db.tbl_Registration.Where(x => x.Email == userName).FirstOrDefault().UserName;
                    obj.EmplD = employeeId;
                    obj.IsActive = true;
                    //var user = db.tbl_EmploymentDetails.Where(u => u.EmplD==employeeId).FirstOrDefault();
                    //var aa = db.tbl_Employee.Where(x => x.ID == employeeId).FirstOrDefault();
                   // user.tbl_Employee.EmploymentDetailsFlag  = true;
                    
                    employmentDL.SaveEmployment(obj);
                }
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
                return employmentDL.GetEmploymentById(id);
            }
            catch (Exception ex)
            {
                //ExceptionManager.Publish(ex, MethodBase.GetCurrentMethod().DeclaringType.Namespace + "-" + MethodBase.GetCurrentMethod().DeclaringType.Name, "FrameWork");
                throw ex;
            }
        }

        public int DeleteEmployment(tbl_EmploymentDetails employment)
        {
            return employmentDL.DeleteEmployment(employment);
        }
    }
}
