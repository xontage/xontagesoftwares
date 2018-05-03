using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRIT.Entity;
using PRIT.Models;
using PRIT.BAL;
using System.Web.Security;
using Newtonsoft.Json;
using PRIT.Entity.MetaModel;

namespace PRIT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        PRITEntities db = new PRITEntities();
        ContactsBL contactsBL = new ContactsBL();
        RegistrationBL registrationBL = new RegistrationBL();
        CollegeBL collegeBL = new CollegeBL();
        UploadedFileBL uploadedFileBL = new UploadedFileBL();
        EmployeeBL employeeBL = new EmployeeBL();
        EmploymentBL employmentBL = new EmploymentBL();
        // GET: Admin

        public ActionResult Index()
        { 
            return View();
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]

        public ActionResult FileUpload(HttpPostedFileBase file, tbl_FileUpload objFile)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        string FileName = System.IO.Path.GetFileName(file.FileName);
                        string physicalPath = Server.MapPath("~/UploadedFiles/" + FileName);
                        string savePath = Server.MapPath("~/UploadedFiles/");

                        // Create a temporary file name to use for checking duplicates.
                        string tempfileName = "";

                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (System.IO.File.Exists(physicalPath))
                        {
                            int counter = 2;
                            while (System.IO.File.Exists(physicalPath))
                            {
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                var fileN = FileName.Split('.');
                                tempfileName = fileN[0] + "- Copy" + "(" + counter.ToString() + ")" + "." + fileN[1];
                                physicalPath = savePath + tempfileName;
                                counter++;
                            }


                            FileName = tempfileName;

                            // Notify the user that the file name was changed.
                            //UploadStatusLabel.Text = "A file with the same name already exists." +
                            //    "<br />Your file was saved as " + fileName;
                        }
                        else
                        {
                            // Notify the user that the file was saved successfully.
                            //UploadStatusLabel.Text = "Your file was uploaded successfully.";
                        }


                        // Append the name of the file to upload to the path.
                        savePath += FileName;


                        // Call the SaveAs method to save the uploaded
                        // file to the specified directory.

                        file.SaveAs(savePath);

                        var docFile = new tbl_FileUpload
                        {

                            FileName = System.IO.Path.GetFileName(physicalPath),
                            FileType = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            Title = objFile.Title
                        };

                        db.tbl_FileUpload.Add(docFile);
                        db.SaveChanges();
                    }



                }
                return View();
            }
            catch (Exception ex /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return Json("Error occurred. Error details: " + ex.Message);
            }

        }

        [HttpGet]
        public ActionResult MultipleFileUpload()
        {
            List<tbl_FileUpload> lstFile = db.tbl_FileUpload.OrderByDescending(file => file.FileID).ToList();
            return View(lstFile);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    List<tbl_FileUpload> lstN = new List<tbl_FileUpload>();
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Create a temporary file name to use for checking duplicates.
                        string tempfileName = "";
                        string physicalPath = Server.MapPath("~/UploadedFiles/" + fname);
                        
                        // Check to see if a file already exists with the
                        // same name as the file to upload.
                        if (System.IO.File.Exists(physicalPath))
                        {
                            
                            // if a file with this name already exists,
                            // prefix the filename with a number.
                            var fileN = fname.Split('.');                                                                           
                            tempfileName = fileN[0] + "- Copy" + "(" + DateTime.Now.ToString().Replace(':', '-').Replace('/','-') + ")" + "." + fileN[1];                                                      
                            fname = tempfileName;

                            // Notify the user that the file name was changed.
                            //UploadStatusLabel.Text = "A file with the same name already exists." +
                            //    "<br />Your file was saved as " + fileName;
                        }
                        else
                        {
                            // Notify the user that the file was saved successfully.
                            //UploadStatusLabel.Text = "Your file was uploaded successfully.";
                        }
                      

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/UploadedFiles/"), fname);
                        file.SaveAs(fname);

                      
                        var docFile = new tbl_FileUpload
                        {

                            FileName = System.IO.Path.GetFileName(fname) ,
                            FileType = Path.GetExtension(file.FileName),
                            ContentType = file.ContentType,
                            //Title = objFile.Title
                        };

                        db.tbl_FileUpload.Add(docFile);
                        db.SaveChanges();

                       
                        
                    }

                    lstN = db.tbl_FileUpload.OrderByDescending(person => person.FileID).ToList();
                    var aab = RenderRazorViewToString("_MultipleFileUploadPartial", lstN);
                    // Returns message that successfully uploaded  
                    return new JsonResult()
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new { message = "File Uploaded Successfully!", success = true, result = aab }
                    };
                    // return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        
        public FileResult Download(string id)
        {
            DataClasses objData = new DataClasses();

            int fid = Convert.ToInt32(id);
            var files = objData.GetFiles();
            string filename = (from f in files
                               where f.FileId == fid
                               select f.FilePath).First();

            string result = Path.GetFileName(filename);
            string contentType = "application/octet-stream";//"application/pdf";
            //Parameters to file are
            //1. The File Path on the File Server
            //2. The content type MIME type
            //3. The parameter for the file save by the browser
            return File(filename, contentType, result);
        }
        public ActionResult ViewContacts()
        {
            return View();
        }
        public ActionResult CollegeMgmt()
        {
            List<tbl_Colleges> lst = db.tbl_Colleges.OrderByDescending(college => college.collegeId).ToList();
            return View(lst);
        }
        public ActionResult EmployeeMgmt()
        {
            List<tbl_Employee> lst = db.tbl_Employee.OrderByDescending(person => person.ID).Where(e => e.IsDeleted == false).ToList();
            return View(lst);
        }

        
        public ActionResult EmploymentDetailMgmt()
        {
            List<tbl_EmploymentDetails> lst = db.tbl_EmploymentDetails.OrderByDescending(person => person.ID).ToList();
            tbl_Employee lstS = new tbl_Employee();
            foreach (var item in lst)
            {
                lstS = (from u in db.tbl_Employee
                                     join ut in db.tbl_EmploymentDetails on u.ID equals ut.EmplD                                    
                                     select u).FirstOrDefault();             
                item.EmployeeFullName = lstS.FirstName +" "+ lstS.LastName;
               
                item.EmployeeEmail = lstS.EmailId;

            }
                    
            return View(lst);
        }

        [HttpGet]
        public ActionResult AddEditEmployee(int? EmpId)
        {

            //tbl_EmployeeMetaModel md = new tbl_EmployeeMetaModel();
            tbl_Employee model = new tbl_Employee();
            if (EmpId > 0)
            {
                model = db.tbl_Employee.Where(x => x.ID == EmpId).FirstOrDefault();
            }

           ViewBag.ddlEmployeeType = new SelectList(GetEmployeeType(), "Value", "Text");
            ViewBag.ddlMaritalStatus = new SelectList(GetMaritalStatus(), "Value", "Text");
            ViewBag.ddlEmpDesignation = new SelectList(GetEmpDesignation(), "Value", "Text");

            return PartialView("~/Views/Admin/_AddEditEmployee.cshtml", model);

        }


        [HttpGet]
        public ActionResult AddEditEmployment(int? empId)
        {

            //tbl_EmployeeMetaModel md = new tbl_EmployeeMetaModel();
            tbl_EmploymentDetails model = new tbl_EmploymentDetails();
            
                model = db.tbl_EmploymentDetails.Where(x => x.EmplD == empId).FirstOrDefault();
            if(model!=null)
               ViewBag.EmploymentId= model.ID;
            
           //// if (model == null) { ViewBag.EmpID = model.ID; }
           
            ViewBag.ddlEmpDesignation = new SelectList(GetEmpDesignation(), "Value", "Text");
            ViewBag.ddlEmployeeType = new SelectList(GetEmployeeType(), "Value", "Text");
            ViewBag.EmpID = empId;

            return PartialView("~/Views/Admin/_AddEditEmployment.cshtml", model);

        }
        
        [NonAction]
        public List<SelectListItem> GetEmployeeType()
        {
              try
                {
                    List<SelectListItem> lstDropdownModelGeneric;
                    lstDropdownModelGeneric = new List<SelectListItem>  {
                        //new SelectListItem(){Text="SELECT EMPLOYEE TYPE",Value="0"},
                        new SelectListItem(){Text="Fresher",Value="Fresher"},
                         new SelectListItem(){Text="Experienced",Value="Experienced"}
                    };
                    return lstDropdownModelGeneric;
                }
                catch (Exception ex)
                {
                    return new List<SelectListItem>();
                }

        }

        [NonAction]
        public List<DropdownModelGeneric> GetMaritalStatus()
        {
            try
            {
                List<DropdownModelGeneric> lstDropdownModelGeneric;
                lstDropdownModelGeneric = new List<DropdownModelGeneric>  {
                        //new DropdownModelGeneric(){Text="SELECT MARRITAL STATUS",Value="0"},
                        new DropdownModelGeneric(){Text="Single",Value="Single"},
                         new DropdownModelGeneric(){Text="Married",Value="Married"}
                    };
                return lstDropdownModelGeneric;
            }
            catch (Exception ex)
            {
                return new List<DropdownModelGeneric>();
            }
        }
        [NonAction]
        public List<DropdownModelGeneric> GetEmpDesignation()
        {
            try
            {
                List<DropdownModelGeneric> lstDropdownModelGeneric;
                lstDropdownModelGeneric = new List<DropdownModelGeneric>  {
                        //new DropdownModelGeneric(){Text="SELECT EMPLOYEE DESIGNATION",Value="0"},
                        new DropdownModelGeneric(){Text="Software Trainee",Value="Software Trainee"},
                         new DropdownModelGeneric(){Text="Software Engineer",Value="Software Engineer"},
                         new DropdownModelGeneric(){Text="Sr.Software Engineer",Value="Sr.Software Engineer"},
                         new DropdownModelGeneric(){Text="QA",Value="QA"},
                         new DropdownModelGeneric(){Text="Receiptionist",Value="Receiptionist"}
                    };
                return lstDropdownModelGeneric;
            }
            catch (Exception ex)
            {
                return new List<DropdownModelGeneric>();
            }
        }


        [HttpPost]
        public ActionResult AddEditEmployee(tbl_Employee model)
        {
            List<tbl_Employee> lstN = new List<tbl_Employee>();

            if (ModelState.IsValid)
            {
                employeeBL.AddEmployees(model, User.Identity.Name);
                lstN = db.tbl_Employee.OrderByDescending(emp => emp.ID).Where(e=>e.IsDeleted==false).ToList();
                var aa = RenderRazorViewToString("_EmployeePartial", lstN);

                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { message = model.ID == 0 ? "employee has been created successfully" : "employee has been updated successfully", success = true, result = aa }
                };
            }

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { message = "Something went wrong!!", success = false }
            };

        }
        [HttpPost]
        public ActionResult AddEditEmployment([Bind(Exclude = "ID")] tbl_EmploymentDetails model=null)
        {
            List<tbl_EmploymentDetails> lstN = new List<tbl_EmploymentDetails>();



            int employeeId = 0;

            if (!string.IsNullOrEmpty(Request.Form["EmploymentIds"]))
            {
                model.ID = Convert.ToInt32(Request.Form["EmploymentIds"]);

            }
            if (!string.IsNullOrEmpty(Request.Form["EmpIds"]))
            {
                employeeId = Convert.ToInt32(Request.Form["EmpIds"]);

            }

            if (ModelState.IsValid)
            {
                employmentBL.AddEmployment(model, User.Identity.Name, employeeId);
                lstN = db.tbl_EmploymentDetails.OrderByDescending(emp => emp.ID).ToList();
                var aa = RenderRazorViewToString("_EmploymentPartial", lstN);

                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { message = model.ID == 0 ? "Employment Details of has been created successfully" : "Employment Details has been updated successfully", success = true, result = aa }
                };
            }

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { message = "Something went wrong!!", success = false }
            };

        }

        [HttpPost]
        public ActionResult DeleteEmployee(int? Id)
        {
            
            tbl_Employee emp = employeeBL.GetEmployeeById(Id);

            List<tbl_Employee> lstN = new List<tbl_Employee>();
            int deletedEmpId = employeeBL.DeleteEmployee(emp);
            if(deletedEmpId>0)
            lstN = db.tbl_Employee.OrderByDescending(e => e.ID).Where(e => e.IsDeleted == false).ToList();
            return PartialView("_EmployeePartial", lstN);
        }

        [HttpPost]
        public ActionResult DeleteEmployment(int? Id)
        {

            tbl_EmploymentDetails employment = employmentBL.GetEmploymentById(Id);

            List<tbl_EmploymentDetails> lstN = new List<tbl_EmploymentDetails>();
            int deletedEmpId = employmentBL.DeleteEmployment(employment);
            if (deletedEmpId > 0)
                lstN = db.tbl_EmploymentDetails.OrderByDescending(e => e.ID).ToList();
            return PartialView("_EmploymentPartial", lstN);
        }
        public ActionResult Registration()
        {
            List<tbl_Registration> lst = db.tbl_Registration.OrderByDescending(person => person.Id).ToList();

            foreach(var item in lst)
            {
               

                      item.CollegeName = (from c in db.tbl_Colleges
                                      join R in db.tbl_Registration on c.collegeId equals R.CollegeID
                                     // where u.Guid == userModel.Guid
                                      select c.collegeName).FirstOrDefault();
            }

            return View(lst);

        }  

        [HttpGet]
        public ActionResult AddEditUser(int? UserId)
        {
            tbl_Registration model = new tbl_Registration();
            if (UserId > 0)
            {
                model = db.tbl_Registration.Where(x => x.Id == UserId).FirstOrDefault();
            }

            ViewBag.ddlDestination = new SelectList(GetDestination(), "Value", "Text");
            ViewBag.ddlColleges = new SelectList(GetColleges(), "Value", "Text");
            ViewBag.ddlRoles = new SelectList(GetRoles(), "Value", "Text");

            return PartialView("~/Views/Admin/_AddEditUser.cshtml", model);

        }


        [HttpPost]
        public ActionResult AddEditUser(tbl_Registration model)
        {
            List<tbl_Registration> lstN = new List<tbl_Registration>();

            if (ModelState.IsValid)
            {
                registrationBL.Registration(model, User.Identity.Name);
                lstN = db.tbl_Registration.OrderByDescending(person => person.Id).ToList();
                var aa = RenderRazorViewToString("_RegistrationPartial", lstN);

                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { message = model.Id == 0 ? "user has been created successfully" : "user has been updated successfully", success = true, result = aa }
                };
            }

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { message = "Something went wrong!!", success = false }
            };

        }

        [HttpPost]
        public ActionResult DeleteRegisteredUser(int Id)
        {
            List<tbl_Registration> lstN = new List<tbl_Registration>();
            registrationBL.DeleteRegisteredUser(Id);
            lstN = db.tbl_Registration.OrderByDescending(person => person.Id).ToList();
            return PartialView("_RegistrationPartial", lstN);
        }
        [HttpPost]
        public ActionResult DeleteContact(int contactId)
        {
            contactsBL.DeleteContact(contactId);

            return Json(new { Status = "success", Message = "Deleted Successfully!!" }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DeleteCollege(int Id)
        {
            List<tbl_Colleges> lstN = new List<tbl_Colleges>();
            collegeBL.DeleteCollege(Id);
            lstN = db.tbl_Colleges.OrderByDescending(person => person.collegeId).ToList();
            return PartialView("_CollegePartial", lstN);
        }
        [HttpPost]
        public ActionResult DeleteUploadedFile(int id)
        {
            List<tbl_FileUpload> lstN = new List<tbl_FileUpload>();
            uploadedFileBL.DeleteUploadedFile(id);
            lstN = db.tbl_FileUpload.OrderByDescending(file => file.FileID).ToList();
            return PartialView("_MultipleFileUploadPartial", lstN);          
        }
        [NonAction]
        public List<DropdownModelGeneric> GetColleges()
        {
            try
            {
                List<tbl_Colleges> lstColleges = new List<tbl_Colleges>();
                lstColleges = db.tbl_Colleges.OrderByDescending(person => person.collegeId).ToList();
                List<DropdownModelGeneric> ddlList = lstColleges.Select(x => new DropdownModelGeneric() { Value = x.collegeId.ToString(), Text = x.collegeName }).ToList();
               // ddlList.Add(new DropdownModelGeneric() { Value = "0", Text = "PLEASE SELECT COLLEGE" });

                return ddlList.OrderBy(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                return new List<DropdownModelGeneric>();
            }
        }
        [NonAction]
        public List<DropdownModelGeneric> GetRoles()
        {
            try
            {
                List<tbl_UserRole> lstRoles = new List<tbl_UserRole>();
                lstRoles = db.tbl_UserRole.OrderByDescending(person => person.RoleId).ToList();
                List<DropdownModelGeneric> ddlList = lstRoles.Select(x => new DropdownModelGeneric() { Value = x.RoleId.ToString(), Text = x.RolName }).ToList();
              //  ddlList.Add(new DropdownModelGeneric() { Value = "0", Text = "PLEASE SELECT ROLE" });

                return ddlList.OrderBy(x => x.Value).ToList();
            }
            catch (Exception ex)
            {
                return new List<DropdownModelGeneric>();
            }
        }
        [NonAction]
        public List<DropdownModelGeneric> GetDestination()
        {
            try
            {
                List<DropdownModelGeneric> lstDropdownModelGeneric;
                lstDropdownModelGeneric = new List<DropdownModelGeneric>  {
                      //  new DropdownModelGeneric(){Text="SELECT DESIGNATION",Value="0"},
                        new DropdownModelGeneric(){Text="HOD",Value="HOD"},
                         new DropdownModelGeneric(){Text="TPO",Value="TPO"},
                        new DropdownModelGeneric(){Text="Principal",Value="Principal"},
                         new DropdownModelGeneric(){Text="Asst. Professor",Value="Asst. Professor"},
                        new DropdownModelGeneric(){Text="Professor",Value="Professor"}

                    };
                return lstDropdownModelGeneric;
            }
            catch (Exception ex)
            {
                return new List<DropdownModelGeneric>();
            }
        }
        [NonAction]
        public List<DropdownModelGeneric> GetDegrees()
        {
            try
            {
                List<tbl_Degree> lstColleges = new List<tbl_Degree>();
                lstColleges = db.tbl_Degree.OrderByDescending(person => person.Id).ToList();
                List<DropdownModelGeneric> ddlList = lstColleges.Select(x => new DropdownModelGeneric() { Value = x.Id.ToString(), Text = x.DegreeName }).ToList();
              //  ddlList.Add(new DropdownModelGeneric() { Value = "0", Text = "PLEASE SELECT DEGREE" });

                return ddlList;
            }
            catch (Exception ex)
            {
                return new List<DropdownModelGeneric>();
            }
        }
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        [HttpGet]
        public ActionResult AddEditCollege(int? CollegeId)
        {
            tbl_Colleges model = new tbl_Colleges();
            tbl_Colleges model1 = new tbl_Colleges();
            List<SelectListItem> items = new List<SelectListItem>();
          
            int[] DegreeIds = new int[20];

            if (CollegeId > 0)
            {
                model = db.tbl_Colleges.Where(x => x.collegeId == CollegeId).FirstOrDefault();
                string[] Sections;
                if (model.Degree != "") {
                    Sections = model.Degree.Split(',');
                }
                else {

                    Sections = new string[] { "0"};
                }

               
                int id;

              
                int i = 0;
                foreach (var Id in Sections)
                {                    
                    id = Convert.ToInt32(Id);
                    tbl_Degree Item = new tbl_Degree();                   
                    DegreeIds[i] = id;                    
                    i++;
                }
                      
                var selectedItemIds = DegreeIds;
                var a = db.tbl_Degree.ToList();
                model1 = new tbl_Colleges
                {
                    Degrees = new MultiSelectList(                     
                     a,
                         "Id",
                         "DegreeName",
                         selectedItemIds
                     )
                };
                model.Degrees = model1.Degrees;
            }
            else
            {
                var a = db.tbl_Degree.ToList();
                model = new tbl_Colleges
                {
                    Degrees = new MultiSelectList(
                     //    new[]
                     //    {
                     //// TODO: Fetch from your repository
                     //new { Id = 1, Name = "item 1" },
                     //new { Id = 2, Name = "item 2" },
                     //new { Id = 3, Name = "item 3" },
                     //    },
                     a,
                         "Id",
                         "DegreeName",
                         null
                     )
                };
            }
            
            return PartialView("~/Views/Admin/_AddEditCollege.cshtml", model);

        }
        [HttpPost]
        public ActionResult AddEditCollege(tbl_Colleges model)
        {
            List<tbl_Colleges> lstN = new List<tbl_Colleges>();

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Request.Form["selectedValues"]))
                {
                    model.Degree = Request.Form["selectedValues"];
                }
                else if (model.DegreeIds != null)
                {

                    string degId = "";

                    foreach (var item in model.DegreeIds)
                    {
                        degId += item + ",";
                    }

                    model.Degree = degId.TrimEnd(',');
                }
                else {

                    model.Degree = "";
                }
                

                collegeBL.AddEditCollege(model);
                lstN = db.tbl_Colleges.OrderByDescending(person => person.collegeId).ToList();
                
                var aa = RenderRazorViewToString("_CollegePartial", lstN);

                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { message = model.collegeId == 0 ? "College has been created successfully" : "College has been updated successfully", success = true, result = aa }
                };
            }

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { message = "Something went wrong!!", success = false }
            };

        }
        public ActionResult AdminDashboard()
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

            return RedirectToAction("Index", "Home");
        }

    }
}