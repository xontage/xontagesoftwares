using PRIT.DAL;
using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.BAL
{
    public class ExpenseBL
    {
        ExpenseDL expenseDL = new ExpenseDL();
        PRITEntities db = new PRITEntities();
        public void SaveExpense(tbl_Expense obj, string userName)
        {
            try
            {
                if (obj.ID > 0) {
                    obj.ModifiedBy = db.tbl_Registration.Where(x => x.Email == userName).FirstOrDefault().UserName;
                    obj.ModifiedDate = DateTime.Now;

                }
                else
                {
                    obj.CreatedBy = db.tbl_Registration.Where(x => x.Email == userName).FirstOrDefault().UserName;
                    obj.CreatedDate = DateTime.Now;
                }
              
                expenseDL.SaveExpense(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }

        public void DeleteExpense(int expenseId)
        {
            try
            {
                expenseDL.DeleteExpense(expenseId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
