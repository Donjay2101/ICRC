using IC_RC.Models;
using IC_RC.ViewModels;
using ICRC.Model;
using ICRCService;
using System;
using System.Collections.Generic;
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
        }
        // GET: Certifications
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var userID = SessionContext<int>.Instance.GetSession("UserID");
            var data=CertificationService.GetCertificationsForIndex();
            return PartialView("_Certifications",data);
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
        public ActionResult Details(int id)
        {
            return View();
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
            if (Request.QueryString["returnUrl"] != null)
            {
                returnUrl = Request.QueryString["returnUrl"];
            }
            
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/Scores/Index";
                ViewBag.ReturnURL = returnUrl;

            }
            else
            {
                ViewBag.ReturnURL = returnUrl;
            }
            // return returnUrl;
        }
    }
}
