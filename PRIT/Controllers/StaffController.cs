using PRIT.BAL;
using PRIT.Entity;
using PRIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PRIT.Controllers
{

    [Authorize(Roles = "Staff, Admin")]
    public class StaffController : Controller
    {
        PRITEntities db = new PRITEntities();

        //[CheckSessionTimeOut]
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
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index","Home");
        }


    }
}