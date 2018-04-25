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
        public void AddEmployment(tbl_EmploymentDetails obj, string userName,int id)
        {
            //var myEmp = empDL.GetAllEmployee();
            //var myEmpList = empDL.GetAllEmployeeList();
           // tbl_Employee p = empDL.GetEmployeeById(obj.ID);
           // tbl_Employee entity = db.tbl_Employee.Where(p => p.ID == obj.ID).AsQueryable().FirstOrDefault();

            try
            {

                if (obj.ID > 0)
                {
                    obj.EmplD = id;
                    
                }
                else
                {
                    obj.EmplD = id;
                    //int count = myEmpList.Count;
                    //count++;
                    //obj.EmployeeId = "XSS-10" + count;
                    //obj.IsDeleted = false;
                }

                employmentDL.SaveEmployment(obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
