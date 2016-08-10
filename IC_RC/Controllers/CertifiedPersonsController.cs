using ICRC.Model;
using ICRCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IC_RC.Controllers
{
    [Authorize(Users="Admin")]        
    public class CertifiedPersonsController : Controller
    {
        public readonly ICertifiedPersonService CertifiedPersonService;
        public readonly ICertificationService CertificationService;
        public readonly IReciprocitiesService ReciprocityService;
        public readonly IStudentEthicalVoliationService VoilationService;
        public readonly IBoardService BoardService;
        

        public CertifiedPersonsController(ICertifiedPersonService CertifiedPersonService, ICertificationService CertificationService, IReciprocitiesService ReciprocityService, IStudentEthicalVoliationService VoilationService, IBoardService BoardService)
        {
            this.CertifiedPersonService = CertifiedPersonService;
            this.CertificationService = CertificationService;
            this.ReciprocityService = ReciprocityService;
            this.VoilationService = VoilationService;
            this.BoardService = BoardService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var data = CertifiedPersonService.GetCertifiedPersons();
            return PartialView("_CertifiedPersons", data);
        }




        // GET: CertifiedPersons/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


       

        // GET: CertifiedPersons/Create
        public ActionResult Create()


        {

            ViewBag.CurrentBoardID = new SelectList(BoardService.GetBoards(), "ID", "Board");
            
            return View();
        }

        // POST: CertifiedPersons/Create
        [HttpPost]
        public ActionResult Create(CertifiedPersons person)
        {
            
                // TODO: Add insert logic here
                if(ModelState.IsValid)
                {
                    person.CreatedAt = DateTime.Now;
                    person.CreatedBy = 1;                                    
                    CertifiedPersonService.CreateCertifiedPerson(person);
                    CertifiedPersonService.Save();
                //db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.CurrentBoardID = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
                ViewBag.OtherBoardID = new SelectList(BoardService.GetBoards(), "ID", "Acronym");
                return View(person);
                            
        }

        // GET: CertifiedPersons/Edit/5
        public ActionResult Edit(int id)
        {
            var data = CertifiedPersonService.GetCertifiedPersonByID(id);

            ViewBag.CurrentBoardID = new SelectList(BoardService.GetBoards(), "ID", "Acronym",data.CurrentBoardID);
            ViewBag.OtherBoardID = new SelectList(BoardService.GetBoards(), "ID", "Acronym",data.OtherBoardID);
            //Certifications


            return View(data);
        }


        [HttpPost]
        public ActionResult Edit(CertifiedPersons person)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                person.ModifiedAt= DateTime.Now;
                person.ModifiedBy= 1;
                CertifiedPersonService.UpdateCertifiedPerson(person);
                CertifiedPersonService.Save();
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrentBoardID = new SelectList(BoardService.GetBoards(), "ID", "Name");
            ViewBag.OtherBoardID = new SelectList(BoardService.GetBoards(), "ID", "Name");
            return View(person);
        }

        [ChildActionOnly]
        public ActionResult Certifications(int ID)
        {

            var certifications = CertificationService.GetCertificationsByPersonID(ID);

            return PartialView("_Certifications",certifications);
        }

        [ChildActionOnly]
        public ActionResult Voilations(int ID)
        {
            var voilations = VoilationService.GetVoiltaionsByPersonID(ID);

            return PartialView("_Voilations",voilations);

        }

        [ChildActionOnly]
        public ActionResult Reciprocity(int ID)
        {

            var data = ReciprocityService.ReciprocityGetByPersonID(ID);           
            return PartialView("_Recprocities",data);
        }





        //// POST: CertifiedPersons/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        ViewBag.CurrentBoardID = new SelectList(BoardService.GetBoards(), "ID", "Name");
        //        ViewBag.OtherBoardID = new SelectList(BoardService.GetBoards(), "ID", "Name");
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: CertifiedPersons/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CertifiedPersons/Delete/5
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
