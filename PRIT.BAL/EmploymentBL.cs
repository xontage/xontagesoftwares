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
                    //obj.EmplD = id;
                    employmentDL.SaveEmployment(obj);
                }
                else
                {
                    obj.EmplD = employeeId;
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
