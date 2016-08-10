using ICRC.Model;
using ICRCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IC_RC.Controllers
{
    public class ReciprocitiesController : Controller
    {
        public readonly ICertifiedPersonService CertifiedPersonService;
        public readonly IReciprocitiesService reciprocityService;
        public readonly ICertificateService certificateService;
        public readonly IBoardService BoardService;
        public readonly IPaymentTypeService paymentService;

        public ReciprocitiesController(ICertifiedPersonService CertifiedPersonService, IReciprocitiesService reciprocityService, IBoardService BoardService, ICertificateService certificateService, IPaymentTypeService paymentService)
        {
            this.CertifiedPersonService = CertifiedPersonService;
            this.reciprocityService = reciprocityService;
            this.BoardService = BoardService;
            this.certificateService = certificateService;
            this.paymentService = paymentService;
        }


        // GET: Reciprocity
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var data=reciprocityService.GetReciprocities();

            return PartialView("_Reciprocities",data);
        }


        // GET: Reciprocity/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reciprocity/Create
        public ActionResult Create()
        {
            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Certificates = new SelectList(certificateService.GetCertificates(), "ID", "Name");
            ViewBag.PaymentTypes = new SelectList(paymentService.GetPaymentTypes(), "ID", "Name");
            ViewBag.Persons= new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            return View();
        }

        // POST: Reciprocity/Create
        [HttpPost]
        public ActionResult Create(Reciprocities model)
        {
            if(ModelState.IsValid)
            {
                reciprocityService.CreateReciprocity(model);
                reciprocityService.Save();
                return RedirectToAction("index");
            }

            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Certificates = new SelectList(certificateService.GetCertificates(), "ID", "Name");
            ViewBag.PaymentTypes = new SelectList(paymentService.GetPaymentTypes(), "ID", "Name");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            return View(model);
        }

        // GET: Reciprocity/Edit/5
        public ActionResult Edit(int ?id)
        {

            if(id==null)
            {
                return HttpNotFound();
            }

            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Certificates = new SelectList(certificateService.GetCertificates(), "ID", "Name");
            ViewBag.PaymentTypes = new SelectList(paymentService.GetPaymentTypes(), "ID", "Name");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            var data=reciprocityService.GetReciprocitiesByID(id.Value);

            return View(data);
        }

        // POST: Reciprocity/Edit/5
        [HttpPost]
        public ActionResult Edit(Reciprocities model)
        {
           if(ModelState.IsValid)
            {
                reciprocityService.UpdateReciprocity(model);
                reciprocityService.Save();
                return RedirectToAction("index");
            }

            ViewBag.Boards = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
            ViewBag.Certificates = new SelectList(certificateService.GetCertificates(), "ID", "Name");
            ViewBag.PaymentTypes = new SelectList(paymentService.GetPaymentTypes(), "ID", "Name");
            ViewBag.Persons = new SelectList(CertifiedPersonService.GetCertifiedPersons(), "ID", "FullName");
            return View(model);
        }

        // GET: Reciprocity/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reciprocity/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
