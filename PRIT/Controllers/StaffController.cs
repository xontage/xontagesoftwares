using PRIT.BAL;
using PRIT.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PRIT.Controllers
{
    [Authorize(Roles = "Staff")]
    public class StaffController : Controller
    {
        PRITEntities db = new PRITEntities();
       

        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }

        ///write code for FileUpload method



        public ActionResult StaffDashboard()
        {
            ViewBag.name = User.Identity.Name;
            return View();
        }

      
      
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }


    }
}