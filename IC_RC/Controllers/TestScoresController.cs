using ICRCService;
using IRCRC.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IC_RC.Controllers
{
    public class TestScoresController : Controller
    {

        public readonly ITestScoreService testScoreService;
        public readonly IScoreboardService scoreboardService;
        public TestScoresController(ITestScoreService testScoreService, IScoreboardService scoreboardService)
        {
            this.testScoreService = testScoreService;
            this.scoreboardService = scoreboardService;
        }

        // GET: TestScores
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var data = testScoreService.GetTestScores().ToList();
            return PartialView("_TestScore", data);
        }

        public ActionResult NormalView()
        {
            ViewBag.PageNumber = 1;
           // ViewBag.LastName = new SelectList(testScoreService.GetLastNames(1),"LastName","LastName");
            return PartialView("_TestScoreNormalView");
        }


        public ActionResult GetLastNames(int PageNum)
        {
            var data = testScoreService.GetLastNames(PageNum);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SearchLastNames(string initial)
        {
            var data = testScoreService.GetTestScoreByPerson(initial);
            return Json(data,JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetFirstNames(string name)
        {
            var data = testScoreService.GetFirstNames(name);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFullData(TestScoreViewModel model)
        {            
                var data = testScoreService.GetDataByFirstAndLastName(model);
                return Json(data,JsonRequestBehavior.AllowGet);            
        }


        // GET: TestScores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestScores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestScores/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TestScores/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TestScores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TestScores/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestScores/Delete/5
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
