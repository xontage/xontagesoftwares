﻿using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRIT.DAL
{
    public class ContactDL
    {
        PRITEntities db = new PRITEntities();
        public void SaveContact(tbl_Contact obj)
        {
          try
            {

                //added code by rahul
                obj.Date = DateTime.Now.ToString();
                db.tbl_Contact.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //added code by rahul
        }

        public void DeleteContact(int contactId)
        {
            try
            {
                var id = db.tbl_Contact.Find(contactId);
                db.tbl_Contact.Remove(id);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
