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
using System.Web.Configuration;
using static PRIT.Models.JqueryDataTable;
using System.ComponentModel;
using PRIT.Entity.ViewModels;
using OfficeOpenXml;

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
        CandidateCourseBL candidateCourseBL = new CandidateCourseBL();

        // GET: Admin

        //[CheckSessionTimeOut]
        public ActionResult Index()
        {
            return View();
        }

        #region  ..Single file upload functionality

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
        #endregion..End of  ..single file upload functionality

        #region ..Multiple file upload functionality

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
                            tempfileName = fileN[0] + "- Copy" + "(" + DateTime.Now.ToString().Replace(':', '-').Replace('/', '-') + ")" + "." + fileN[1];
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

                            FileName = System.IO.Path.GetFileName(fname),
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

        [HttpPost]
        public ActionResult DeleteUploadedFile(int id)
        {
            List<tbl_FileUpload> lstN = new List<tbl_FileUpload>();
            uploadedFileBL.DeleteUploadedFile(id);
            lstN = db.tbl_FileUpload.OrderByDescending(file => file.FileID).ToList();
            return PartialView("_MultipleFileUploadPartial", lstN);
        }

        #endregion...End of ..Multiple file upload functionality

        #region ..View Contacts functionality

        public ActionResult ViewContacts()
        {
            return View();
        }
        public ActionResult ViewContactsNew()
        {
            return View();
        }

        public ActionResult GetAppsListData(DTParameters dtModel, int id)
        {
            var draw = dtModel.Draw;
            var searchValue = dtModel.Search.Value;
            string Date, InquirySpecification, Name, Email, ContactNo, InquiryText, sorting = null;

            Date = dtModel.Columns[0].Search.Value;
            InquirySpecification = dtModel.Columns[1].Search.Value;
            Name = dtModel.Columns[2].Search.Value;
            Email = dtModel.Columns[3].Search.Value;
            ContactNo = dtModel.Columns[4].Search.Value;
            InquiryText = dtModel.Columns[5].Search.Value;

            sorting = dtModel.SortOrder;
            var start = dtModel.Start;
            var length = dtModel.Length;

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            List<tbl_Contact> list = new List<tbl_Contact>();
            // list = db.tbl_Contacts.Where(a => a.Plant.IndustryID == id).OrderByDescending(app => app.CreatedAt).ToList();
            list = db.tbl_Contact.OrderByDescending(a => a.Id).ToList();
            List<tbl_Contact> appsCementList = new List<tbl_Contact>();

            foreach (var item in list)
            {
                tbl_Contact appsCement = new tbl_Contact();
                appsCement.Date = item.Date;
                appsCement.InquirySpecification = item.InquirySpecification;
                appsCement.Name = item.Name;
                appsCement.Email = item.Email;
                appsCement.ContactNo = item.ContactNo;
                //appsCement.ClosingDate = item.ClosingDate.ToString("dd MMM yyyy");
                appsCement.InquiryText = item.InquiryText;
                appsCement.Actions = "<div class='btn-group'><button data-toggle='dropdown' class='btn btn-success btn-xs dropdown-toggle' type='button'>" +
                    "Select Action <span class='caret'></span> </button> <ul role='menu' class='dropdown-menu pull-right' style='min-width: 121px !important;'><li>" +
                     //"<a href='/Admin/DeleteContact/" + item.Id + "'>Delete Record</a></li>"+
                     "<button type='button' class='btn btn-danger' onclick='abc(" + item.Id + ");' data-contactid=" + item.Id + ">Delete Record</button></li>";

                //if (item.Plant.IndustryID == 1 || item.Plant.IndustryID == 2)
                //    appsCement.Actions += "<li><a href='/Admin/Apps/SupplierData/" + item.ID + "'>Supplier Data</a></li>";
                //if (item.Plant.IndustryID == 1)
                //    appsCement.Actions += "<li><a href='/Admin/Apps/ReviewCement/" + item.ID + "'>Review</a></li>";
                //else if (item.Plant.IndustryID == 2)
                //    appsCement.Actions += "<li><a href='/Admin/Apps/ReviewSteel/" + item.ID + "'>Review</a></li>";
                //appsCement.Actions += "</ul> </div>";
                //string archived = item.Archived ? "checked" : "";
                //appsCement.Archive = "<input type='checkbox' name='cbxArchive' value='" + item.ID + "' data-on-color='success' data-size='mini'" +
                //   "data-off-text ='No' data-on-text='Yes' " + archived + " /> ";
                //string active = item.Active ? "checked" : "";
                //appsCement.Activate = "<input type='checkbox' name='cbxActivate' value='" + item.ID + "' data-on-color='success' data-size='mini'" +
                //   "data-off-text= 'No' data-on-text='Yes' active) /> ";

                appsCementList.Add(appsCement);
            }

            if (!string.IsNullOrEmpty(searchValue))
                appsCementList = appsCementList.Where(m => m.Date.Contains(searchValue) || m.InquirySpecification.ToString().Contains(searchValue) ||
                   m.Name.Contains(searchValue) || m.Email.ToString().Contains(searchValue) || m.ContactNo.ToString().Contains(searchValue)
                    || m.InquiryText.Contains(searchValue)).ToList();

            appsCementList = appsCementList.Where(m => (Date == null || m.Date.Contains(Date)) && (Name == null || m.Name.Contains(Name))
                && (InquirySpecification == null || m.InquirySpecification.ToString().Contains(InquirySpecification)) && (Email == null || m.Email.ToString().Contains(Email))
                 && (ContactNo == null || m.ContactNo.ToString().Contains(ContactNo)) && (InquiryText == null || m.InquiryText.ToString().Contains(InquiryText))
                 ).ToList();


            recordsTotal = appsCementList.Count;
            appsCementList = appsCementList.Skip(skip).Take(pageSize).OrderBy(M => M.Date).ToList();

            if (sorting.Contains("DESC"))
            {
                sorting = (!string.IsNullOrEmpty(sorting)) ? sorting.Replace("DESC", "").Trim() : "";
                appsCementList = appsCementList.OrderBy(m => ((sorting == "Name") ? m.Name : (sorting == "Date") ?
                    m.Date.ToString() : (sorting == "InquirySpecification") ? m.InquirySpecification : (sorting == "Email") ? m.Email : (sorting == "ContactNo") ?
                    m.ContactNo.ToString() : m.InquiryText)).ToList();
            }
            else
                appsCementList = appsCementList.OrderByDescending(m => ((sorting == "Name") ? m.Name : (sorting == "Date") ?
                        m.Date.ToString() : (sorting == "InquirySpecification") ? m.InquirySpecification : (sorting == "Email") ? m.Email : (sorting == "ContactNo") ?
                        m.ContactNo.ToString() : m.InquiryText)).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = appsCementList }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult DeleteContact(int contactId)
        {
            List<tbl_Contact> lstN = new List<tbl_Contact>();
            contactsBL.DeleteContact(contactId);
            lstN = db.tbl_Contact.OrderByDescending(person => person.Id).ToList();
            //return PartialView("_ViewContactsPartial");
            return Json(new { Status = "success", Message = "Deleted Successfully!!" }, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        ///     Code to export data in Excel format   
        /// </summary>
        public void ExportToExcel()
        {
            var data = db.tbl_Contact.Select(x => new ExportToExcel
            {
                Id = x.Id,
                Name = x.Name,
                InquirySpecification = x.InquirySpecification,
                InquiryText = x.InquiryText,
                ContactNo = x.ContactNo,
                Email = x.Email,
                Date = x.Date
            }).ToList();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Contact.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            WriteToExcel(data, Response.Output);
            Response.End();
        }
        public void WriteToExcel<T>(IEnumerable<T> data, TextWriter output)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write("\t");
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(
                         prop.GetValue(item)));
                    output.Write("\t");
                }
                output.WriteLine();
            }
        }

        #endregion..End of ..View Contacts functionality

        #region ..Code related to Employee management functionality

        public ActionResult EmployeeMgmt()
        {
            List<tbl_Employee> lst = db.tbl_Employee.OrderByDescending(person => person.ID).ToList();
            foreach (var item in lst)
            {
                item.Status = item.IsDeleted == true ? "Non-Working" : "Working";
            }
            return View(lst);
        }

        [HttpGet]
        public ActionResult AddEditEmployee(int? EmpId)
        {

            //tbl_EmployeeMetaModel md = new tbl_EmployeeMetaModel();

            tbl_Employee model = new tbl_Employee();
            tbl_Employee model1 = new tbl_Employee();
            List<SelectListItem> items = new List<SelectListItem>();
            int[] SkillIds = new int[30];

            if (EmpId > 0)
            {
                model = db.tbl_Employee.Where(x => x.ID == EmpId).FirstOrDefault();

                string[] Sections;
                if (!string.IsNullOrEmpty(model.Skills))
                {
                    Sections = model.Skills.Split(',');
                }
                else
                {

                    Sections = new string[] { "0" };
                }

                int id;

                int i = 0;
                foreach (var Id in Sections)
                {
                    id = Convert.ToInt32(Id);
                    tbl_Degree Item = new tbl_Degree();
                    SkillIds[i] = id;
                    i++;
                }

                var selectedItemIds = SkillIds;
                var a = db.tbl_SkillSet.ToList();
                model1 = new tbl_Employee
                {
                    Skillset = new MultiSelectList(
                     a,
                         "Id",
                         "SkillName",
                         selectedItemIds
                     )
                };
                model.Skillset = model1.Skillset;
            }
            else
            {

                var a = db.tbl_SkillSet.ToList();
                model = new tbl_Employee
                {
                    Skillset = new MultiSelectList(
                     a,
                         "Id",
                         "SkillName",
                         null
                     )
                };
            }

            ViewBag.ddlEmployeeType = new SelectList(GetEmployeeType(), "Value", "Text");
            ViewBag.ddlMaritalStatus = new SelectList(GetMaritalStatus(), "Value", "Text");
            ViewBag.ddlEmpDesignation = new SelectList(GetEmpDesignation(), "Value", "Text");
            ViewBag.ddlBloodGroup = new SelectList(GetBloodGroup(), "Value", "Text");


            return PartialView("~/Views/Admin/_AddEditEmployee.cshtml", model);

        }

        [HttpPost]
        public ActionResult AddEditEmployee(tbl_Employee model)
        {
            List<tbl_Employee> lstN = new List<tbl_Employee>();

            if (ModelState.IsValid)
            {
                //employeeBL.AddEmployees(model, User.Identity.Name);
                //lstN = db.tbl_Employee.OrderByDescending(emp => emp.ID).Where(e => e.IsDeleted == false).ToList();
                //var aa = RenderRazorViewToString("_EmployeePartial", lstN);

                //return new JsonResult()
                //{
                //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                //    Data = new { message = model.ID == 0 ? "employee has been created successfully" : "employee has been updated successfully", success = true, result = aa }
                //};

                if (!string.IsNullOrEmpty(Request.Form["selectedSkillValues"]))
                {
                    model.Skills = Request.Form["selectedSkillValues"];
                }
                else if (model.SkillIds != null)
                {

                    string skillId = "";

                    foreach (var item in model.SkillIds)
                    {
                        skillId += item + ",";
                    }

                    model.Skills = skillId.TrimEnd(',');
                }
                else
                {
                    model.Skills = "";
                }

                employeeBL.AddEmployees(model, User.Identity.Name);
                lstN = db.tbl_Employee.OrderByDescending(emp => emp.ID).ToList();
                foreach (var item in lstN)
                {
                    item.Status = item.IsDeleted == true ? "Non-Working" : "Working";
                }
                var aa = RenderRazorViewToString("_EmployeePartial", lstN);

                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { message = model.ID == 0 ? "Employee has been created successfully" : "Employee has been updated successfully", success = true, result = aa }
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
            if (deletedEmpId > 0)
                lstN = db.tbl_Employee.OrderByDescending(e => e.ID).ToList();
            //lstN = db.tbl_Employee.OrderByDescending(e => e.ID).Where(e => e.IsDeleted == false).ToList();
            foreach (var item in lstN)
            {
                item.Status = item.IsDeleted == true ? "Non-Working" : "Working";
            }
            return PartialView("_EmployeePartial", lstN);
        }


        #endregion..End of ....Code related to Employee management functionality

        #region ....Code related to Employment management functionality


        public ActionResult EmploymentDetailMgmt()
        {
            List<tbl_EmploymentDetails> lst = db.tbl_EmploymentDetails.OrderByDescending(person => person.ID).Where(e => e.IsActive == true).ToList();
            tbl_Employee lstS = new tbl_Employee();
            foreach (var item in lst)
            {
                lstS = (from u in db.tbl_Employee
                        join ut in db.tbl_EmploymentDetails on u.ID equals ut.EmplD
                        where u.ID == item.EmplD
                        select u).FirstOrDefault();
                item.EmployeeFullName = lstS.FirstName + " " + lstS.LastName;

                item.EmployeeEmail = lstS.EmailId;

            }

            return View(lst);
        }

        [HttpGet]
        public ActionResult AddEditEmployment(int? empId)
        {

            //tbl_EmployeeMetaModel md = new tbl_EmployeeMetaModel();
            tbl_EmploymentDetails model = new tbl_EmploymentDetails();

            model = db.tbl_EmploymentDetails.Where(x => x.EmplD == empId).FirstOrDefault();
            if (model != null)
                ViewBag.EmploymentId = model.ID;

            //// if (model == null) { ViewBag.EmpID = model.ID; }

            ViewBag.ddlEmpDesignation = new SelectList(GetEmpDesignation(), "Value", "Text");
            ViewBag.ddlEmployeeType = new SelectList(GetEmployeeType(), "Value", "Text");
            ViewBag.ddlExperience = new SelectList(GetExperience(), "Value", "Text");
            ViewBag.EmpID = empId;

            return PartialView("~/Views/Admin/_AddEditEmployment.cshtml", model);

        }

        [HttpPost]
        public ActionResult AddEditEmployment([Bind(Exclude = "ID")] tbl_EmploymentDetails model = null)
        {
            List<tbl_Employee> lstN = new List<tbl_Employee>();

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

                //lstN = db.tbl_EmploymentDetails.OrderByDescending(emp => emp.ID).ToList();
                //var aa = RenderRazorViewToString("_EmploymentPartial", lstN);

                lstN = db.tbl_Employee.OrderByDescending(emp => emp.ID).ToList();
                foreach (var item in lstN)
                {
                    item.Status = item.IsDeleted == true ? "Non-Working" : "Working";
                }
                var aa = RenderRazorViewToString("_EmployeePartial", lstN);

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
        public ActionResult DeleteEmployment(int? Id)
        {

            tbl_EmploymentDetails employment = employmentBL.GetEmploymentById(Id);
            tbl_Employee lstS = new tbl_Employee();
            List<tbl_EmploymentDetails> lstN = new List<tbl_EmploymentDetails>();
            int deletedEmpId = employmentBL.DeleteEmployment(employment);
            if (deletedEmpId > 0)
                lstN = db.tbl_EmploymentDetails.OrderByDescending(e => e.ID).Where(e => e.IsActive == true).ToList();
            foreach (var item in lstN)
            {
                lstS = (from u in db.tbl_Employee
                        join ut in db.tbl_EmploymentDetails on u.ID equals ut.EmplD
                        where u.ID == item.EmplD
                        select u).FirstOrDefault();
                item.EmployeeFullName = lstS.FirstName + " " + lstS.LastName;

                item.EmployeeEmail = lstS.EmailId;

            }

            return PartialView("_EmploymentPartial", lstN);
        }

        #endregion..End of ....Code related to Employment management functionality

        #region ....Code related to User Registration functionality

        public ActionResult Registration()
        {
            List<tbl_Registration> lst = db.tbl_Registration.OrderByDescending(person => person.Id).ToList();

            foreach (var item in lst)
            {


                item.CollegeName = (from c in db.tbl_Colleges
                                    join R in db.tbl_Registration on c.collegeId equals R.CollegeID
                                    where c.collegeId == item.CollegeID
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
                model.Password = RegistrationBL.DecryptPassword(model.Password, model.UserSalt);
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
                foreach (var item in lstN)
                {


                    item.CollegeName = (from c in db.tbl_Colleges
                                        join R in db.tbl_Registration on c.collegeId equals R.CollegeID
                                        where c.collegeId == item.CollegeID
                                        select c.collegeName).FirstOrDefault();
                }
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

        #endregion...End of ....Code related to User Registration functionality

        #region ...all Dropdowns in application on Pop up window

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
        public List<SelectListItem> GetExperience()
        {
            try
            {
                List<SelectListItem> lstDropdownModelGeneric;
                lstDropdownModelGeneric = new List<SelectListItem>  {
                        //new SelectListItem(){Text="SELECT EMPLOYEE TYPE",Value="0"},
                        new SelectListItem(){Text="0 - 1 YEARS",Value="0 - 1 YEARS"},
                         new SelectListItem(){Text="1 - 2 YEARS",Value="1 - 2 YEARS"},
                         new SelectListItem(){Text="2 - 3 YEARS",Value="2 - 3 YEARS"},
                         new SelectListItem(){Text="3 - 4 YEARS",Value="3 - 4 YEARS"},
                         new SelectListItem(){Text="4 - 5 YEARS",Value="4 - 5 YEARS"},
                         new SelectListItem(){Text="5 - 6 YEARS",Value="5 - 6 YEARS"},
                         new SelectListItem(){Text="6 - 7 YEARS",Value="6 - 7 YEARS"},
                         new SelectListItem(){Text="7 - 8 YEARS",Value="7 - 8 YEARS"},
                         new SelectListItem(){Text="8 - 9 YEARS",Value="8 - 9 YEARS"},
                         new SelectListItem(){Text="9 - 10 YEARS",Value="9 - 10 YEARS"},
                         new SelectListItem(){Text="10 - 11 YEARS",Value="10 - 11 YEARS"}
                         
                    };
                return lstDropdownModelGeneric;
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }

        }
        [NonAction]
        public List<SelectListItem> GetBloodGroup()
        {
            try
            {
                List<SelectListItem> lstDropdownModelGeneric;
                lstDropdownModelGeneric = new List<SelectListItem>  {
                        //new SelectListItem(){Text="SELECT EMPLOYEE TYPE",Value="0"},
                        new SelectListItem(){Text="A+",Value="A+"},
                         new SelectListItem(){Text="A-",Value="A-"},
                          new SelectListItem(){Text="B+",Value="B+"},
                           new SelectListItem(){Text="B-",Value="B-"},
                            new SelectListItem(){Text="O+",Value="O+"},
                             new SelectListItem(){Text="O-",Value="O-"},
                             new SelectListItem(){Text="AB+",Value="AB+"},
                             new SelectListItem(){Text="AB-",Value="AB-"},
                             new SelectListItem(){Text="Unknown",Value="Unknown"}
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

        [NonAction]
        public List<DropdownModelGeneric> GetCourseName()
        {
            try
            {
                List<DropdownModelGeneric> lstDropdownModelGeneric;
                lstDropdownModelGeneric = new List<DropdownModelGeneric>  {
                      //  new DropdownModelGeneric(){Text="SELECT DESIGNATION",Value="0"},
                        new DropdownModelGeneric(){Text="Java",Value="1"},
                         new DropdownModelGeneric(){Text=".Net",Value="2"},
                        new DropdownModelGeneric(){Text="Designing",Value="3"},
                         new DropdownModelGeneric(){Text="PHP",Value="4"},
                        new DropdownModelGeneric(){Text="Testing",Value="5"}

                    };
                return lstDropdownModelGeneric;
            }
            catch (Exception ex)
            {
                return new List<DropdownModelGeneric>();
            }
        }
        
        [NonAction]
        public List<DropdownModelGeneric> GetCourseType()
        {
            try
            {
                
                List<DropdownModelGeneric> lstDropdownModelGeneric;
                lstDropdownModelGeneric = new List<DropdownModelGeneric>  {
                        //new DropdownModelGeneric(){Text="--SELECT COURSE TYPE--",Value="0"},
                        new DropdownModelGeneric(){Text="Crash Regular",Value="1"},
                         new DropdownModelGeneric(){Text="WeekEnd",Value="2"},
                        new DropdownModelGeneric(){Text="Crash WeekEnd",Value="3"},
                         new DropdownModelGeneric(){Text="Regular",Value="4"}
                        

                    };
                return lstDropdownModelGeneric;
            }
            catch (Exception ex)
            {
                return new List<DropdownModelGeneric>();
            }
        }
       

        [NonAction]
        public List<SelectListItem> GetDuration()
        {
            try
            {
                List<SelectListItem> lstDropdownModelGeneric;
                lstDropdownModelGeneric = new List<SelectListItem>  {
                        //new SelectListItem(){Text="--SELECT DURATION--",Value="0"},
                        new SelectListItem(){Text="1 MONTH",Value="1"},
                         new SelectListItem(){Text="2 MONTHS",Value="2"},
                        new SelectListItem(){Text="3 MONTHS",Value="3"},
                         new SelectListItem(){Text="6 MONTHS",Value="4"},
                         new SelectListItem(){Text="12 MONTHS",Value="5"}
                    };
                return lstDropdownModelGeneric;
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }

        }     
        
        [NonAction]
        public List<DropdownModelGeneric> GetCourseCategory()
        {
            try
            {

                List<DropdownModelGeneric> lstDropdownModelGeneric;
                lstDropdownModelGeneric = new List<DropdownModelGeneric>  {
                      //  new DropdownModelGeneric(){Text="SELECT DESIGNATION",Value="0"},
                        new DropdownModelGeneric(){Text="Diploma",Value="1"},
                         new DropdownModelGeneric(){Text="Certification",Value="2"},
                        new DropdownModelGeneric(){Text="Advanced Diploma",Value="3"},
                         new DropdownModelGeneric(){Text="Basic",Value="4"},
                         new DropdownModelGeneric(){Text="Internship",Value="5"},
                         new DropdownModelGeneric(){Text="Job Oriented Training Program(JOTP)",Value="6"},
                         new DropdownModelGeneric(){Text="Other",Value="7"},


                    };
                return lstDropdownModelGeneric;
            }
            catch (Exception ex)
            {
                return new List<DropdownModelGeneric>();
            }
        }

        #endregion...End of ...all Dropdowns in application on Pop up window

        #region ..College Management Functionality

        public ActionResult CollegeMgmt()
        {
            List<tbl_Colleges> lst = db.tbl_Colleges.OrderByDescending(college => college.collegeId).ToList();
            return View(lst);
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
                if (model.Degree != "")
                {
                    Sections = model.Degree.Split(',');
                }
                else
                {

                    Sections = new string[] { "0" };
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
                else
                {

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

        [HttpPost]
        public ActionResult DeleteCollege(int Id)
        {
            List<tbl_Colleges> lstN = new List<tbl_Colleges>();
            collegeBL.DeleteCollege(Id);
            lstN = db.tbl_Colleges.OrderByDescending(person => person.collegeId).ToList();
            return PartialView("_CollegePartial", lstN);
        }


        #endregion..End of ..College Management Functionality

        #region ...Candidate Cource management..
        
         public ActionResult CandidateCourseMgmt()
        {
            List<tbl_CandidateWithCourseDetails> lst = db.tbl_CandidateWithCourseDetails.OrderByDescending(c => c.CandidateId).ToList();
            var CourseNameList = GetCourseName();
            var DurationList = GetDuration();
            var CourseTypeList = GetCourseType();
            var CourseCategoryList = GetCourseCategory();
            foreach (var item in lst)
            {
                item.Status = item.IsDeleted == true ? "Discontinued" : "Continue..";
                item.CourseName= CourseNameList.Where(p => p.Value == item.CourseNameId.ToString()).First().Text;
                item.CourseType = CourseTypeList.Where(p => p.Value == item.CourseTypeId.ToString()).First().Text;
                if (!string.IsNullOrEmpty(item.Duration.ToString()))
                    item.DurationName = DurationList.Where(p => p.Value == item.Duration.ToString()).First().Text;
                else
                    item.DurationName = "NA";
                if (!string.IsNullOrEmpty(item.CourseCategory.ToString()))
                    item.CourseCategoryName = CourseCategoryList.Where(p => p.Value == item.CourseCategory.ToString()).First().Text;
                else
                    item.CourseCategoryName = "NA";
            }
            return View(lst);
        }

        
         [HttpGet]
        public ActionResult AddEditCandidateCourse(int? candidateCourseId)
        {
            tbl_CandidateWithCourseDetails model = new tbl_CandidateWithCourseDetails();
            if (candidateCourseId > 0)
            {
                model = db.tbl_CandidateWithCourseDetails.Where(x => x.CandidateId == candidateCourseId).FirstOrDefault();
                
            }

            ViewBag.ddlExperience = new SelectList(GetEmployeeType(), "Value", "Text");
            ViewBag.ddlCourseName = new SelectList(GetCourseName(), "Value", "Text");
            ViewBag.ddlCourseType = new SelectList(GetCourseType(), "Value", "Text");
            ViewBag.ddlDuration = new SelectList(GetDuration(), "Value", "Text");
            ViewBag.ddlCourseCategory = new SelectList(GetCourseCategory(), "Value", "Text");

            return PartialView("~/Views/Admin/_AddEditCandidateCourse.cshtml", model);

        }

        [HttpPost]
        public ActionResult AddEditCandidateCourse(tbl_CandidateWithCourseDetails model)
        {
            List<tbl_CandidateWithCourseDetails> lstN = new List<tbl_CandidateWithCourseDetails>();
            var CourseNameList = GetCourseName();
            var DurationList = GetDuration();
            var CourseTypeList = GetCourseType();
            var CourseCategoryList = GetCourseCategory();
            if (ModelState.IsValid)
            {
                candidateCourseBL.AddEditCandidateCourse(model, User.Identity.Name);
                lstN = db.tbl_CandidateWithCourseDetails.OrderByDescending(person => person.CandidateId).ToList();
                foreach (var item in lstN)
                {
                    item.Status = item.IsDeleted == true ? "Discontinued" : "Continue..";
                    item.CourseName = CourseNameList.Where(p => p.Value == item.CourseNameId.ToString()).First().Text;
                    item.CourseType = CourseTypeList.Where(p => p.Value == item.CourseTypeId.ToString()).First().Text;
                    if (!string.IsNullOrEmpty(item.Duration.ToString()))
                        item.DurationName = DurationList.Where(p => p.Value == item.Duration.ToString()).First().Text;
                    else
                        item.DurationName = "NA";
                    if (!string.IsNullOrEmpty(item.CourseCategory.ToString()))
                        item.CourseCategoryName = CourseCategoryList.Where(p => p.Value == item.CourseCategory.ToString()).First().Text;
                    else
                        item.CourseCategoryName = "NA";
                }
                var aa = RenderRazorViewToString("_candidateCoursePartial", lstN);

                return new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new { message = model.CandidateId == 0 ? "user has been created successfully" : "user has been updated successfully", success = true, result = aa }
                };
            }

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = new { message = "Something went wrong!!", success = false }
            };

        }


        [HttpPost]
        public ActionResult DeleteCandidateCourse(int? Id)
        {       
            List<tbl_CandidateWithCourseDetails> lstN = new List<tbl_CandidateWithCourseDetails>();
            var CourseNameList = GetCourseName();
            var DurationList = GetDuration();
            var CourseTypeList = GetCourseType();
            var CourseCategoryList = GetCourseCategory();
            int deletedEmpId = candidateCourseBL.DeleteCandidateCourse(Id);
            if (deletedEmpId > 0)
                lstN = db.tbl_CandidateWithCourseDetails.OrderByDescending(e => e.CandidateId).ToList();
            //lstN = db.tbl_Employee.OrderByDescending(e => e.ID).Where(e => e.IsDeleted == false).ToList();
            foreach (var item in lstN)
            {
                item.Status = item.IsDeleted == true ? "Discontinued" : "Continue..";
                item.CourseName = CourseNameList.Where(p => p.Value == item.CourseNameId.ToString()).First().Text;
                item.CourseType = CourseTypeList.Where(p => p.Value == item.CourseTypeId.ToString()).First().Text;
                if (!string.IsNullOrEmpty(item.Duration.ToString()))
                    item.DurationName = DurationList.Where(p => p.Value == item.Duration.ToString()).First().Text;
                else
                    item.DurationName = "NA";
                if (!string.IsNullOrEmpty(item.CourseCategory.ToString()))
                    item.CourseCategoryName = CourseCategoryList.Where(p => p.Value == item.CourseCategory.ToString()).First().Text;
                else
                    item.CourseCategoryName = "NA";
            }
            return PartialView("_candidateCoursePartial", lstN);
        }




        #endregion..End of ...Candidate Cource management..

        #region ..Fees Management functionality..

        public ActionResult FeesMgmt()
        {
            List<tbl_CourseFees> lst = db.tbl_CourseFees.OrderByDescending(c => c.Id).ToList();
                       
            return View(lst);
        }

        
        [HttpGet]
        public ActionResult AddEditCourseFees(int? CourseFeesId)
        {
            tbl_CourseFees model = new tbl_CourseFees();
            if (CourseFeesId > 0)
            {
                model = db.tbl_CourseFees.Where(x => x.Id == CourseFeesId).FirstOrDefault();

            }
         
            return PartialView("~/Views/Admin/_AddEditCourseFees.cshtml", model);

        }
        public JsonResult GetCandidateEmail(string countryName)
        {

          var txtItems=  db.tbl_CandidateWithCourseDetails.Where(m => m.EmailId.Contains(countryName)).ToList().Select(s => s.EmailId);
                          
            return Json(txtItems, JsonRequestBehavior.AllowGet);
        }

        #endregion .End Of ..Fees Management functionality..

        //Generic Method for Converting any Razor View into String
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

        //public ActionResult AdminDashboard()
        //{            
        //    ViewBag.name = User.Identity.Name;
        //    return View();
        //}
        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();

        //    Session.Abandon();
        //    Session.Clear();
        //    Session.RemoveAll();

        //    return RedirectToAction("Index", "Home");
        //}

        public ActionResult Logout()
        {
            // Delete the user details from cache.
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();
            // Clear authentication cookie.
            //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            //cookie.Expires = DateTime.Now.AddYears(-1);
            //Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }

        #region ..Profile
        public ActionResult MyProfile()
        {
            var UserEmail = User.Identity.Name;
            ViewBag.Message = "Welcome to my demo!";
            PRIT.Entity.ViewModels.MyProfileViewModel mymodel = new Entity.ViewModels.MyProfileViewModel();
            mymodel.Registration = db.tbl_Registration.Where(m => m.Email == UserEmail).ToList();
            mymodel.Employee = db.tbl_Employee.Where(m => m.EmailId == UserEmail).ToList();
            foreach (var item in mymodel.Employee)
            {
                mymodel.EmploymentDetails = db.tbl_EmploymentDetails.Where(p => p.EmplD == item.ID).ToList();

            }

            //Successful Lamda expression Join on two tables on emailId
            //var UserInRole = db.tbl_EmploymentDetails.
            // Join(db.tbl_Employee, u => u.EmplD, uir => uir.ID,
            //(u, uir) => new { u, uir })
            //.Where(m => m.uir.EmailId == UserEmail).ToList();            

            return View(mymodel);

        }
        #endregion


    }
}