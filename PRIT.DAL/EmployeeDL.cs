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
        public long SaveEmployees(tbl_Employee obj)
        {
           // var entity = db.tbl_Employee.Where(p => p.ID == obj.ID).AsQueryable().FirstOrDefault();
            try
            {
             
                if (obj.ID > 0)
                {
                    var lst = db.tbl_Employee.Where(Ind => Ind.ID == obj.ID).FirstOrDefault();

                    lst.InstituteLocation = obj.InstituteLocation;
                    lst.InstituteName = obj.InstituteName;
                    lst.LastName = obj.LastName;
                    lst.FirstName = obj.FirstName;
                    lst.MiddleName = obj.MiddleName;
                    lst.MobileNo = obj.MobileNo;
                    lst.PANNo = obj.PANNo;
                    lst.PassportNo = obj.PassportNo;
                    lst.YearOfPassing = obj.YearOfPassing;
                    lst.University = obj.University;
                    lst.Percentage = obj.Percentage;
                    lst.Specialization = obj.Specialization;
                    lst.State = obj.State;
                    lst.MaritalStatus = obj.MaritalStatus;
                    lst.Pincode = obj.Pincode;
                    lst.AccountNo = obj.AccountNo;
                    lst.Address = obj.Address;
                    lst.AdharCardNo = obj.AdharCardNo;
                    lst.AltMobileNo = obj.AltMobileNo;
                    lst.BankName = obj.BankName;
                    lst.BloodGroup = obj.BloodGroup;
                    lst.City = obj.City;
                    lst.EmailId = obj.EmailId;
                    lst.Gender = obj.Gender;
                    lst.Country = obj.Country;
                    lst.EmployeeType = obj.EmployeeType;
                    lst.Designation = obj.Designation;
                    lst.HighestEducation = obj.HighestEducation;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    //   db.Entry(entity).CurrentValues.SetValues(obj);
                }
                else
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_Employee.Add(obj);
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
