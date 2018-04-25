using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRIT.Entity;
using PRIT.DAL;

namespace PRIT.BAL
{
    public class CollegeBL 
    {
        CollegeDL collegeDL = new CollegeDL();
        PRITEntities db = new PRITEntities();

        public void AddEditCollege(tbl_Colleges obj)
        {


            try
           {                
                collegeDL.AddEditCollege(obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void DeleteCollege(int Id)
        {
            collegeDL.DeleteCollege(Id);
        }
    }
}
