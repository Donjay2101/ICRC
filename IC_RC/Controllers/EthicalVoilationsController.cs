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
    public class EthicalVoilationsController : Controller
    {

        public readonly ICertifiedPersonService CertifiedPersonService;
        public readonly ICertificationService CertificationService;
        public readonly ICertificateService CertificateService;
        public readonly IBoardService BoardService;
        public readonly IStudentEthicalVoliationService studentethicalvoilationService;
        public readonly IEthicalVoliationService ethicalVoilationService;
        string returnUrl="";
        public EthicalVoilationsController(ICertifiedPersonService CertifiedPersonService, ICertificateService CertificateService, ICertificationService CertificationService, IBoardService BoardService, IStudentEthicalVoliationService studentethicalvoilationService, IEthicalVoliationService ethicalVoilationService)
        {
            this.CertifiedPersonService = CertifiedPersonService;
            this.CertificationService = CertificationService;
            this.CertificateService = CertificateService;
            this.BoardService = BoardService;
            this.studentethicalvoilationService = studentethicalvoilationService;
            this.ethicalVoilationService = ethicalVoilationService;
        }

        // GET: EthicalVoilations
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var data = studentethicalvoilationService.GetEthicalVoilations();

            return PartialView("_Voilations", data);
        }
        // GET: EthicalVoilations/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EthicalVoilations/Create
        public ActionResult Create()
        {
            SetReturnUrl();
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.EthicalVoilations = new SelectList(ethicalVoilationService.GetEthicalVoilations(), "ID", "Name");            
            return View();
        }



        // POST: EthicalVoilations/Create
        [HttpPost]
        public ActionResult Create(StudentVoilations model)
        {
            SetReturnUrl();
            if(ModelState.IsValid)
            {
                studentethicalvoilationService.CreateEthicalVoilation(model);
                studentethicalvoilationService.Save();
                return Redirect(returnUrl);
            }
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.EthicalVoilations = new SelectList(ethicalVoilationService.GetEthicalVoilations(), "ID", "Name");
            return View(model);
        }

        // GET: EthicalVoilations/Edit/5
        public ActionResult Edit(int? id)
        {
            SetReturnUrl();
            if(id==null)
            {
                return RedirectToActionPermanent("PageNotFound", "Home");
            }
            var data = studentethicalvoilationService.GetEthicalVoilationByID(id.Value);
            if(data==null)
            {
                return RedirectToActionPermanent("PageNotFound", "Home");
            }
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.EthicalVoilations = new SelectList(ethicalVoilationService.GetEthicalVoilations(), "ID", "Name");
            return View(data);
        }

        // POST: EthicalVoilations/Edit/5
        [HttpPost]
        public ActionResult Edit(StudentVoilations model)
        {
            SetReturnUrl();
            if (ModelState.IsValid)
            {
                studentethicalvoilationService.UpdateEthicalVoilation(model);
                studentethicalvoilationService.Save();

                return Redirect(returnUrl);
            }
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.EthicalVoilations = new SelectList(ethicalVoilationService.GetEthicalVoilations(), "ID", "Name");
            return View(model);
        }

        // GET: EthicalVoilations/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: EthicalVoilations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                studentethicalvoilationService.Delete(id);
                return Json(true, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
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
