using PRIT.DAL;
using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.BAL
{
    public class UploadedFileBL
    {
        UploadedFileDL uploadedFileDL = new UploadedFileDL();
        PRITEntities db = new PRITEntities();
        public void DeleteUploadedFile(int Id)
        {
            uploadedFileDL.DeleteUploadedFile(Id);
        }

    }
}
