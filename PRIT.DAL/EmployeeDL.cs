using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.DAL
{
   public class EmployeeDL
    {
        PRITEntities db = new PRITEntities();
        //added code by rahul
        public void SaveEmployees(tbl_Employee obj)
        {
            var entity = db.tbl_Employee.Where(p => p.ID == obj.ID).AsQueryable().FirstOrDefault();
            try
            {
                //added code by rahul
                if (obj.ID > 0)
                {

                  //  var Id = db.tbl_Employee.Find(obj.ID);

                    // p.FirstName = obj.FirstNamede
                    db.Entry(entity).CurrentValues.SetValues(obj);
                     //db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    //    db.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                    //db.SaveChanges();

                }
                else
                {
                  
                    db.tbl_Employee.Add(obj);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteEmployee(tbl_Employee emp)
        {
            try
            {
                // var user = db.tbl_Employee.Where(x => x.ID == emp.ID).FirstOrDefault();
                var db1 = db.tbl_Employee.Where(u => u.ID.Equals(emp.ID)).FirstOrDefault();
                db1.IsDeleted = true;
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
        public tbl_Employee GetEmployeeById(int? id)
        {
            try
            {
                var record = db.tbl_Employee.Find(id);
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tbl_Employee> GetAllEmployee()
        {
            try
            {
                var record = db.tbl_Employee.Where(p=>p.IsDeleted==false).ToList();
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<tbl_Employee> GetAllEmployeeList()
        {
            try
            {
                var record = db.tbl_Employee.ToList();
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
