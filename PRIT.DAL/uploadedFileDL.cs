using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.DAL
{
    public class UploadedFileDL
    {
        PRITEntities db = new PRITEntities();
        public void DeleteUploadedFile(int Id)
        {
            try
            {
                var file = db.tbl_FileUpload.Where(x => x.FileID == Id).FirstOrDefault();
                if (file != null)
                {
                    db.tbl_FileUpload.Remove(file);
                    db.SaveChanges();

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
