﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRIT.Models;
using PRIT.Entity;
using PRIT.BAL;
using System.Web.Security;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using PRIT.Entity.MetaModel;

namespace PRIT.Controllers
{
    public class HomeController : Controller
    {

        PRITEntities db = new PRITEntities();
        ContactsBL contactsBL = new ContactsBL();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(tbl_Contact obj)
        {
            ViewBag.Message = "Your contact page.";
            try
            {
                contactsBL.SaveContact(obj);

                return Json(new { Status = "success", Message = "File Upload Successfully!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json("Error occurred. Error details: " + ex.Message);
            }
        }
        public ActionResult Careers()
        {
            return View();
        }

        [HttpPost]      
        public ActionResult Careers(PRIT.Models.MailModel objModelMail, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                string from = "hr@xontagesoftwares.com"; //example:- sourabh9303@gmail.com
                using (MailMessage mail = new MailMessage(from, "jobs@xontagesoftwares.com"))
                {
                    mail.Subject = objModelMail.Subject;
                    mail.Body = objModelMail.Body;
                    
                    if (files != null)
                    {
                        string fileName = Path.GetFileName(files.FileName);
                        mail.Attachments.Add(new Attachment(files.InputStream, fileName));
                    }
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "webmail.xontagesoftwares.com";
                    smtp.EnableSsl = false;
                    NetworkCredential networkCredential = new NetworkCredential(from, "xontagerp123!@#");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 25;
                    smtp.Send(mail);
                    ViewBag.Message = "Sent";
                    return View("Careers");
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Portfolio()
        {
            return View();
        }
        public ActionResult Services(string innerText)
        {
            ViewBag.LinkText = innerText;
            return View();
        }
        public ActionResult Domains(string innerText)
        {
            ViewBag.LinkText = innerText;
            return View();
        }
        public ActionResult MobileDevelopment()
        {
            
            return View();
        }
        
        public ActionResult IEEEProjects()
        {
            return View();
        }
        
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult AboutUsNew()
        {
            return View();
        }
        public ActionResult Login()
        {

            return View();

        }
        public ActionResult Login1()
        {

            return View();

        }

        public ActionResult LoginNew()
        {

            return View();

        }



        /// <summary>
        /// 2017/05/18 - it will check credential of user and with valid credentials it give access to framework //commented by Noman
        /// </summary>
        /// <param name="loginViewModel">Login Credntials</param>
        /// <param name="returnUrl">return url</param>
        /// <returns>Login succeed or fail</returns>
        [HttpPost]
       
        public ActionResult Loginxxx(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {



                    var p = db.tbl_Registration.Where(x => x.UserName == model.Email && x.Password == model.Password).FirstOrDefault();
                    if (p != null)
                    {
                        FormsAuthentication.SetAuthCookie(p.UserName, false);

                        if (p.RoleId == 1 && p.IsActive == true)
                        {
                            return RedirectToAction("AdminDashboard", "Admin");
                        }
                        else if (p.RoleId == 2 && p.IsActive == true)
                        {
                            return RedirectToAction("StaffDashboard", "Staff");
                        }

                        string message1 = "YOUR ACCOUNT HAS BEEN DEACTIVATED TEMPORARY !!!";
                        ViewBag.Message = message1;

                    }
                    //}
                    else
                    {
                        string message = "ENTER CORRECT USERNAME AND PASSWORD";
                        ViewBag.Message = message;
                    }


                    return View();


                    //FrameWorkSettingsModel frameWorkSettingsModel = new FrameWorkSettingsModel();
                    //string IncryptApplUsrName = "";
                    //User user = null;
                    //double TotalDays = 1;
                    //string Expiretime = ConfigurationManager.AppSettings["ExpireTime"];
                    //if (string.IsNullOrEmpty(Expiretime))
                    //    Expiretime = "60";

                    //if (string.IsNullOrWhiteSpace(returnUrl))
                    //{
                    //    returnUrl = Convert.ToString(TempData["ReturnURL"]);
                    //}

                    //UserBL userBL = new UserBL();
                    //user = userBL.CheckUserNameExistsOrNot(loginViewModel.Email);
                    //frameWorkSettingsModel = frameWorkSettingsBL.GetSettingsInfo();
                    //if (frameWorkSettingsModel != null)
                    //{
                    //    if (frameWorkSettingsModel.ApplicationSalt != null)
                    //    {
                    //        IncryptApplUsrName = CommonMasterBL.EncryptPassword(loginViewModel.Email, frameWorkSettingsModel.ApplicationSalt);
                    //    }
                    //}
                    //Response.Cookies["UserName"].Value = IncryptApplUsrName;

                    //if (user != null)
                    //{                        
                    //    user = userBL.LoginVerification(loginViewModel.Email, loginViewModel.Password);//objUserBL.GetUserByUserName(loginViewModel.Email);//get userdetail by email id               
                    //    if (user != null)
                    //    {
                    //        string[] roles = null;
                    //        string[] types = null;
                    //        if (user.RoleId != null && user.RoleId > 0)
                    //            roles = user.UserRoleName.Split(',');
                    //        if (user.UserTypeId != null && user.UserTypeId > 0)
                    //        {
                    //            if (user.userType != null)
                    //                types = user.userType.Split(',');
                    //        }
                    //        CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    //        serializeModel.Id = user.Id;
                    //        serializeModel.FirstName = user.FirstName;
                    //        serializeModel.LastName = user.LastName;
                    //        serializeModel.roles = roles.Union(types).ToArray();
                    //        serializeModel.RoleName = user.UserRoleName;
                    //        serializeModel.EmailID = user.AccountEmail;
                    //        serializeModel.UserTypeId = Convert.ToInt32(user.UserTypeId);
                    //        JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();
                    //        string userData = objJavaScriptSerializer.Serialize(serializeModel);
                    //        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    //        1,
                    //        user.Id.ToString(),
                    //        DateTime.Now,
                    //       DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                    //      false, //  model.RememberMe, //pass here true, if you want to implement remember me functionality
                    //        userData,
                    //        FormsAuthentication.FormsCookiePath);

                    //        string encTicket = FormsAuthentication.Encrypt(authTicket);
                    //        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                    //        {
                    //            HttpOnly = true,
                    //            Secure = FormsAuthentication.RequireSSL,
                    //            Path = FormsAuthentication.FormsCookiePath,
                    //            Domain = FormsAuthentication.CookieDomain
                    //        };
                    //        Response.AppendCookie(cookie);


                    //        Response.Cookies["UserName"].Value = IncryptApplUsrName;
                    //        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    //            return Redirect(returnUrl);
                    //        else
                    //            return RedirectToAction("Index", "Home");
                    //    }
                    //    else
                    //    {
                    //        ModelState.AddModelError("spnmsg", "Invalid credentials.");
                    //    }
                    //}
                    //else
                    //{
                    //    ModelState.AddModelError("spnmsg", "User not exist!!!");
                    //}



                }
            }
            catch (Exception ex)
            {
                
                throw ex;
                
            }
            return View();
        }




        [HttpPost]
        public ActionResult Login(tbl_Registration model, string returnUrl)
        {
            //var cust = db.tbl_Registration.Where(x => x.UserName.Contains(model.UserName)).FirstOrDefault();

            //string uname = "";
            //string pass = "";

            //    uname = cust.UserName;
            //    pass = cust.Password;

            //   var dataItem = db.tbl_Registration.Where(x => x.UserName == model.UserName && x.Password == model.Password).First();


            var p = db.tbl_Registration.Where(x => x.UserName == model.UserName && x.Password == model.Password).FirstOrDefault();
            if (p != null)
            {
                FormsAuthentication.SetAuthCookie(p.UserName, false);

                if (p.RoleId == 1 && p.IsActive==true)
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else if (p.RoleId == 2 && p.IsActive == true)
                {
                    return RedirectToAction("StaffDashboard", "Staff");
                }

                string message1 = "YOUR ACCOUNT HAS BEEN DEACTIVATED TEMPORARY !!!";
                ViewBag.Message = message1;

            }
            //}
            else
            {
                string message = "ENTER CORRECT USERNAME AND PASSWORD";
                ViewBag.Message = message;
            }


            return View();
            //try
            //{
            //   var allUsers= db.tbl_Login.ToList();
            //    foreach (var item in allUsers)
            //    {
            //        if (objLogin.UserName == item.UserName && objLogin.Password == item.Password && item.RoleId=="1")
            //        {
            //            FormsAuthentication.SetAuthCookie(objLogin.UserName, false);
            //            return RedirectToAction("AdminDashboard", new { Name = "Admin" });

            //        }
            //      else  if (objLogin.UserName == item.UserName && objLogin.Password == item.Password && item.RoleId == "2")
            //        {
            //            FormsAuthentication.SetAuthCookie(objLogin.UserName, false);
            //            return RedirectToAction("StaffDashboard");                        
            //        }
            //    }
            //    string message="ENTER CORRECT USERNAME AND PASSWORD";
            //    ViewBag.Message = message;
            //    return View();

            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}

        }

        public ActionResult ForgotPassword()
        {
            return View();

        }

        //Step 1 : Forgot password  HTTP post action method


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public  ActionResult ForgotPassword(tbl_Registration model)
        {

            
               var user = GetUserDetailByMail(model.Email);
                //var user = await UserManager.FindByNameAsync(model.Email);

                string To = model.Email, UserID, Password, SMTPPort, Host;

                if (user == null)
                {

                    // If user does not exist or is not confirmed.

                    return View("ForgotPassword");

                }
                else
                {

                    //Generate password token

                    var guid = Guid.NewGuid();


                    //Create URL with above token

                    var lnkHref = "<a href='" + Url.Action("ResetPassword", "Home", new { email = model.Email, code = guid }, "http") + "'>Reset Password</a>";


                    //HTML Template for Send email

                    string subject = "Your changed password";

                    string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;


                    //Get and set the AppSettings using configuration manager.

                    EmailManager.AppSettings(out UserID, out Password, out SMTPPort, out Host);


                    //Call send email methods.

                    EmailManager.SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);

                }

           

            return View();

        }
        public ActionResult ResetPassword()
        {
            return View();

        }

        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult ResetPassword(tbl_Registration model, string email, string code)

        //{

        //    if (ModelState.IsValid)

        //    {

        //        AspNetUser user = _repoAspDotNetUser.GetAspNetUser(email);

        //        if (user != null)

        //        {

        //            String hashedNewPassword = userManager.PasswordHasher.HashPassword(model.Password);

        //            bool result = _repoAspDotNetUser.ResetPasswordByToken(email, code, hashedNewPassword);

        //            if (result)

        //            {

        //                ModelState.AddModelError("", "Please return to the login page and enjoy with new password.");

        //            }

        //        }

        //        else

        //        {

        //            ModelState.AddModelError("", "It's not a valid, this attempt is already processed.");

        //        }

        //    }

        //    return View();

        //}


        public tbl_Registration GetUserDetailByMail(string Email)
        {
            tbl_Registration user = new tbl_Registration();
            try
            {
                user = db.tbl_Registration.Where(m => m.Email == Email && m.IsActive == true ).FirstOrDefault();
              
            }
            catch (Exception ex)
            {
                
                return null;
            }
            return user;
        }



    }


}