using PRIT.DAL;
using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.BAL
{
   public class EmployeeBL
    {
        EmployeeDL empDL = new EmployeeDL();
        PRITEntities db = new PRITEntities();
        public void AddEmployees(tbl_Employee obj1, string userName)
        {
            var myEmp = empDL.GetAllEmployee();
            var myEmpList = empDL.GetAllEmployeeList();
            tbl_Employee p = empDL.GetEmployeeById(obj1.ID);


            try
            {
                if (obj1.ID > 0)
                {
                   
                    obj1.EmployeeId = p.EmployeeId;
                    obj1.IsDeleted = p.IsDeleted;
                }
                else
                {
                    int count = myEmpList.Count;
                    count++;
                    obj1.EmployeeId = "XSS-10" + count;
                    obj1.IsDeleted = false;
                }

                empDL.SaveEmployees(obj1);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public tbl_Employee GetEmployeeById(int? id)
        {
            try
            {
                return empDL.GetEmployeeById(id);
            }
            catch (Exception ex)
            {
                //ExceptionManager.Publish(ex, MethodBase.GetCurrentMethod().DeclaringType.Namespace + "-" + MethodBase.GetCurrentMethod().DeclaringType.Name, "FrameWork");
                throw ex;
            }
        }
        public int DeleteEmployee(tbl_Employee emp)
        {
            return empDL.DeleteEmployee(emp);
        }
    }
}
