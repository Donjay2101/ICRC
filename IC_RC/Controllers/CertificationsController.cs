using IC_RC.Models;
using IC_RC.ViewModels;
using ICRC.Model;
using ICRCService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IC_RC.Controllers
{
    [Authorize]
    public class CertificationsController : Controller
    {

        public readonly ICertifiedPersonService CertifiedPersonService;
        public readonly ICertificationService CertificationService;
        public readonly ICertificateService CertificateService;
        public readonly IBoardService BoardService;
        public readonly IPaymentTypeService paymenttypeService;
        string returnUrl="";

        public CertificationsController(ICertifiedPersonService CertifiedPersonService, ICertificateService CertificateService, ICertificationService CertificationService, IBoardService BoardService, IPaymentTypeService paymenttypeService)
        {
            this.CertifiedPersonService = CertifiedPersonService;
            this.CertificationService = CertificationService;
            this.CertificateService = CertificateService;
            this.BoardService = BoardService;
            this.paymenttypeService = paymenttypeService;       
            //if(Server!=null)
            //{
               
            //}    
        }

        // GET: Certifications
        public ActionResult Index()
        {
            var user = ShrdMaster.Instance.GetUser(User.Identity.Name);
            if (user == null)
            {
                ViewBag.Error = "User is not active.";
                return RedirectToAction("/Account/login");
            }
            return View();
        }

        public ActionResult GetData()
        {
           // var userID = SessionContext<int>.Instance.GetSession("UserID");
            var user = ShrdMaster.Instance.GetUser(User.Identity.Name);
            if (user == null)
            {
                ViewBag.Error = "User is not active.";
                return Redirect("Account/login");
            }
            IEnumerable<Certifications> certifications;
            if(ShrdMaster.Instance.IsAdmin(user.Username))
            {
                certifications= CertificationService.GetCertificationsForIndex();
            }
            else
            {
                certifications = CertificationService.GetCertificationsByBoardID(user.BoardID);
            }
            
            return PartialView("_Certifications",certifications);
        }


        public int GenerateNumber()
        {
            Random r = new Random();
            int number= r.Next(100000, 999999);                
            if (CertificationService.CheckNumber(number))
            {
                //number = r.Next(100000, 999999);
                GenerateNumber();
            }            
            return number;
        }


        public JsonResult GenerateCertificateNumber()
        {

            int result = GenerateNumber();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Certifications/Details/5
        public ActionResult Details(int ?id)
        {
            SetReturnUrl();
            if (id==null)
            {
                return HttpNotFound();
            }
            var data = CertificationService.GetCertificationByID(id.Value);
            return View(data);
        }

        // GET: Certifications/Create
        public ActionResult Create()
        {
            SetReturnUrl();
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(),"ID","FullName");
            ViewBag.Certificates= new SelectList(CertificateService.GetCertificates(), "ID", "Name");
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.CertificateNumber = GenerateNumber();
            ViewBag.PaymentTypes = paymenttypeService.GetPaymentTypes();
            return View();
        }

        // POST: Certifications/Create
        [HttpPost]
        public ActionResult Create(Certifications model)
        {
            SetReturnUrl();
            
                if(ModelState.IsValid)
                {
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = SessionContext<int>.Instance.GetSession("UserID");
                CertificationService.CreateCertification(model);
                CertifiedPersonService.Save();
                return Redirect(returnUrl);
                }

            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Certificates = new SelectList(CertificateService.GetCertificates(), "ID", "Name");
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.CertificateNumber = GenerateNumber();
            ViewBag.PaymentTypes = paymenttypeService.GetPaymentTypes();
            return View(model);                                            
        }

        // GET: Certifications/Edit/5
        public ActionResult Edit(int id)
        {
            SetReturnUrl();
            var data = CertificationService.GetCertificationByID(id);

            if(data == null)
            {
                return RedirectToActionPermanent("PageNotFound", "Home");
            }

            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Certificates = new SelectList(CertificateService.GetCertificates(), "ID", "Name");
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
          
            ViewBag.PaymentTypes = paymenttypeService.GetPaymentTypes();
            return View(data);
        }

        // POST: Certifications/Edit/5
        [HttpPost]
        public ActionResult Edit(Certifications model)
        {
            SetReturnUrl();
           if(ModelState.IsValid)
            {
                model.ModifiedAt = DateTime.Now;
                model.ModifiedBy = SessionContext<int>.Instance.GetSession("UserID");
                CertificationService.UpdateCertification(model);
                CertificateService.Save();
                return Redirect(returnUrl);
            }
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Certificates = new SelectList(CertificateService.GetCertificates(), "ID", "Name");
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            
            ViewBag.PaymentTypes = paymenttypeService.GetPaymentTypes();
            return View(model);
                    
                
        }

        //// GET: Certifications/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Certifications/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                CertificationService.Delete(id);
                CertificationService.Save();
                return Json(true,JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return View();
            }
        }

        public void SetReturnUrl()
        {
            
            //to go to previous page
            
                returnUrl =ShrdMaster.Instance.GetReturnUrl("/Certifications/Index");
                ViewBag.ReturnURL = returnUrl;

            
            // return returnUrl;
        }

        public ActionResult UploadCertifications()
        {
            try
            {
                LoggerApp.Logger.ExceptionPath = Server.MapPath("~/Errors.txt");
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    HttpPostedFileBase file = files[0];
                    string filename = "";

                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        filename = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        filename = file.FileName;// + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + "-(" + DateTime.Now.Minute + "-" + DateTime.Now.Second + ")";
                    }
                    filename = Path.Combine(Server.MapPath("~/Uploads/Certifications"), filename);
                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);
                    }
                    file.SaveAs(filename);

                    CertificationService.UploadCSV(filename);
                    // testScoreService.UploadCSV(filename);

                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("-1", JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                LoggerApp.Logger.Instance.LogException(ex);
                return Json(ex.InnerException.Message, JsonRequestBehavior.AllowGet);
            }

           // return Json("-1", JsonRequestBehavior.AllowGet);
        }


        public ActionResult ShowAddtoQueueCertificates()
        {
            var data = CertificationService.QueueForPrint();
            return View(data);
        }
        public ActionResult PrintBatchCertificates()
        {
            var data = CertificationService.QueueForPrint();
            string path = Server.MapPath("~/PrintedCertifications");            
            CertificationService.GenerateCertificate(data.Select(x => x.ID).ToList(),path); 
            
            
                       
            return View();
        }

        

        [AllowAnonymous]
        public ActionResult  PrintCertificate(int ID)
        {
            var data= CertificationService.QueueToPrintByCertificationID(ID);
            return View(data);
        }
    }
}
