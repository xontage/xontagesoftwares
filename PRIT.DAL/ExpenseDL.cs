using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.DAL
{
   public class ExpenseDL
    {
        PRITEntities db = new PRITEntities();
        public long SaveExpense(tbl_Expense obj)
        {
            try
            {
                if (obj.ID > 0)
                {
                    var lst = db.tbl_Expense.Where(Ind => Ind.ID == obj.ID).FirstOrDefault();
                    lst.TotalExpenseAmount = obj.TotalExpenseAmount;
                    lst.Comment = obj.Comment;
                    lst.Amount = obj.Amount;
                    lst.ExpenseType = obj.ExpenseType;
                    lst.ModifiedDate = obj.ModifiedDate;
                    lst.ModifiedBy = obj.ModifiedBy;
                    db.Configuration.ValidateOnSaveEnabled = false;

                }
                else
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tbl_Expense.Add(obj);
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

        public void DeleteExpense(int expenseId)
        {
            try
            {
                var id = db.tbl_Expense.Find(expenseId);
                db.tbl_Expense.Remove(id);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
