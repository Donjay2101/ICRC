using ICRCService;
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
