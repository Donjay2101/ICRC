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
    public class ScoresController : Controller
    {

        public readonly IScoreservice scoreService;
        public readonly ICertifiedPersonService personService;
        public readonly ITestingCompanyService companyService;
        public readonly IBoardService boardService;
        public readonly ICertificateService certificateService;

        public ScoresController(IScoreservice scoreService, ICertifiedPersonService personService, ITestingCompanyService companyService, IBoardService boardService, ICertificateService certificateService)
        {
            this.scoreService= scoreService;
            this.personService = personService;
            this.companyService = companyService;
            this.boardService = boardService;
            this.certificateService = certificateService;
        }


        // GET: Scores
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var data = scoreService.GetScores();
            return PartialView("_scores",data);
        }

        // GET: Scores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Scores/Create
        public ActionResult Create()
        {
            ViewBag.Exams = new SelectList(certificateService.GetCertificates(), "ID", "Name");
            ViewBag.Persons=new SelectList(personService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Companies = new SelectList(companyService.GetTestingCompanies(), "ID", "Name");
            ViewBag.Boards = new SelectList(boardService.GetBoards(), "ID", "Acronym");
            return View();
        }

        // POST: Scores/Create
        [HttpPost]
        public ActionResult Create(Scores model)
        {
            if(ModelState.IsValid)
            {
                scoreService.CreateScore(model);
                scoreService.Save();

                return RedirectToAction("Index");
            }

            ViewBag.Exams = new SelectList(certificateService.GetCertificates(), "ID", "Name");
            ViewBag.Persons = new SelectList(personService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Companies = new SelectList(companyService.GetTestingCompanies(), "ID", "Name");
            ViewBag.Boards = new SelectList(boardService.GetBoards(), "ID", "Acronym");
            return View(model);
        }

        // GET: Scores/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return RedirectToActionPermanent("PageNotFound", "Home");
            }

            var data = scoreService.GetScoreByID(id.Value);

            if(data==null)
            {
                return RedirectToActionPermanent("PageNotFound", "Home");
            }
            ViewBag.Exams = new SelectList(certificateService.GetCertificates(), "ID", "Name");
            ViewBag.Persons = new SelectList(personService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Companies = new SelectList(companyService.GetTestingCompanies(), "ID", "Name");
            ViewBag.Boards = new SelectList(boardService.GetBoards(), "ID", "Acronym");

            return View(data);
        }

        // POST: Scores/Edit/5
        [HttpPost]
        public ActionResult Edit(Scores model)
        {
            if (ModelState.IsValid)
            {
                scoreService.UpdateScore(model);
                scoreService.Save();

                return RedirectToAction("Index");
            }

            ViewBag.Exams = new SelectList(certificateService.GetCertificates(), "ID", "Name");
            ViewBag.Persons = new SelectList(personService.GetCertifiedPersons(), "ID", "FullName");
            ViewBag.Companies = new SelectList(companyService.GetTestingCompanies(), "ID", "Name");
            ViewBag.Boards = new SelectList(boardService.GetBoards(), "ID", "Acronym");
            return View(model);
        }

        // GET: Scores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Scores/Delete/5
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
