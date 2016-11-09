using IC_RC.Models;
using ICRC.Model;
using ICRC.Model.ViewModel;
using ICRCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IC_RC.Controllers
{
    [Authorize]
    public class EthicalviolationsController : Controller
    {

        public readonly ICertifiedPersonService CertifiedPersonService;
        public readonly ICertificationService CertificationService;
        public readonly ICertificateService CertificateService;
        public readonly IBoardService BoardService;
        public readonly IStudentEthicalViolationService studentethicalviolationservice;
        public readonly IEthicalViolationService ethicalviolationservice;
        string returnUrl="";
        string submitUrl = "";
        public EthicalviolationsController(ICertifiedPersonService CertifiedPersonService, ICertificateService CertificateService, ICertificationService CertificationService, IBoardService BoardService, IStudentEthicalViolationService studentethicalviolationservice, IEthicalViolationService ethicalviolationservice)
        {
            this.CertifiedPersonService = CertifiedPersonService;
            this.CertificationService = CertificationService;
            this.CertificateService = CertificateService;
            this.BoardService = BoardService;
            this.studentethicalviolationservice = studentethicalviolationservice;
            this.ethicalviolationservice = ethicalviolationservice;
        }

        // GET: Ethicalviolations
        public ActionResult Index()
        {
            var user = ShrdMaster.Instance.GetUser(User.Identity.Name);
            if (user == null)
            {
                ViewBag.Error = "User is not active.";
                return Redirect("/Account/login");
            }
            var data = GetVoilations();
            return View(data);
        }

        public List<StudentVoilationForIndex> GetVoilations(string board="",string person="",string violation="")
        {
            var user = ShrdMaster.Instance.GetUser(User.Identity.Name);
            List<StudentVoilationForIndex> ethicalvoiliation;
            if (ShrdMaster.Instance.IsAdmin(user.Username))
            {
                ethicalvoiliation = studentethicalviolationservice.GetVoilationsForIndex(board,person,violation).ToList();
            }
            else
            {
                ethicalvoiliation = studentethicalviolationservice.GetEthicalviolationsByBoardID(user.BoardID).ToList();
            }
            return ethicalvoiliation;
        }
        public ActionResult GetData(string board = "", string person = "", string violation = "")
        {
            var user = ShrdMaster.Instance.GetUser(User.Identity.Name);
            if (user == null)
            {
                ViewBag.Error = "User is not active.";
                return Redirect("/Account/login");
            }
            var data = GetVoilations(board,person,violation);
            return PartialView("_violations",data);
        }
        // GET: Ethicalviolations/Details/5
        public ActionResult Details(int ?id)
        {
            SetReturnUrl();
            if (id==null)
            {
                return HttpNotFound();
            }
            var voilation = studentethicalviolationservice.GetEthicalviolationByID(id.Value);
            return View(voilation);
        }

        // GET: Ethicalviolations/Create
        public ActionResult Create()
        {
            SetReturnUrl();
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Ethicalviolations = new SelectList(ethicalviolationservice.GetEthicalviolations(), "ID", "Name");            
            return View();
        }



        // POST: Ethicalviolations/Create
        [HttpPost]
        public ActionResult Create(Studentviolations model)
        {
            SetReturnUrl();
            if(ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = SessionContext<int>.Instance.GetSession("UserID");
                studentethicalviolationservice.CreateEthicalviolation(model);
                studentethicalviolationservice.Save();
                return Redirect(returnUrl);
            }
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Ethicalviolations = new SelectList(ethicalviolationservice.GetEthicalviolations(), "ID", "Name");
            return View(model);
        }

        // GET: Ethicalviolations/Edit/5
        public ActionResult Edit(int? id)
        {
            SetReturnUrl();
            if(id==null)
            {
                return RedirectToActionPermanent("PageNotFound", "Home");
            }
            var data = studentethicalviolationservice.GetEthicalviolationByID(id.Value);
            if(data==null)
            {
                return RedirectToActionPermanent("PageNotFound", "Home");
            }
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Ethicalviolations = new SelectList(ethicalviolationservice.GetEthicalviolations(), "ID", "Name");
            return View(data);
        }

        // POST: Ethicalviolations/Edit/5
        [HttpPost]
        public ActionResult Edit(Studentviolations model)
        {
            SetReturnUrl();
            if (ModelState.IsValid)
            {
                model.ModifiedAt = DateTime.Now;
                model.ModifiedBy = SessionContext<int>.Instance.GetSession("UserID");
                studentethicalviolationservice.UpdateEthicalviolation(model);
                studentethicalviolationservice.Save();

                return Redirect(returnUrl);
            }
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Ethicalviolations = new SelectList(ethicalviolationservice.GetEthicalviolations(), "ID", "Name");
            return View(model);
        }

        // GET: Ethicalviolations/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Ethicalviolations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                studentethicalviolationservice.Delete(id);
                studentethicalviolationservice.Save();
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
            returnUrl = ShrdMaster.Instance.GetReturnUrl("/EthicalVoilations/Index");
            ViewBag.ReturnURL = returnUrl;

            ////to go to previous page
            //if (Request.QueryString["returnUrl"] != null)
            //{
            //    returnUrl = Request.QueryString["returnUrl"];
            //    var arr = returnUrl.Split('/');
            //}

            //if (string.IsNullOrEmpty(returnUrl))
            //{
            //    returnUrl = ;
            //    ViewBag.ReturnURL = returnUrl;

            //}
            //else
            //{
            //    ViewBag.ReturnURL = returnUrl;
            //}
            // return returnUrl;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
