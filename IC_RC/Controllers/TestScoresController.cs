﻿using ICRC.Model;
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
        public readonly ITestingCompanyService companyService;
        string returnUrl;
        public TestScoresController(ITestScoreService testScoreService, IScoreboardService scoreboardService, ITestingCompanyService companyService)
        {
            this.testScoreService = testScoreService;
            this.scoreboardService = scoreboardService;
            this.companyService = companyService;
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


        public ActionResult EditTestScores(int ID)
        {

            var data = testScoreService.GetTestScoresByID(ID);
            ViewBag.Companies = new SelectList(companyService.GetTestingCompanies(), "ID", "Name");
            ViewBag.Boards = new SelectList(scoreboardService.GetScoreboards(), "ID", "Name");
            ViewBag.Status = new SelectList(ShrdMaster.Instance.Getstatus(), "ID", "Name");
            return PartialView("_TestScoreView", data);
        }

        [HttpPost]
        public ActionResult UpdateTestScores(TestScore Model)
        {
            testScoreService.UpdateScores(Model);
            testScoreService.Save();

            return Json("1", JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult UpdateInformation(TestScore Model)
        {
            testScoreService.UpdateTestScore(Model);
            testScoreService.Save();

            return Json("1", JsonRequestBehavior.AllowGet);

        }


        // GET: TestScores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestScores/Create
        public ActionResult Create()
        {
            SetReturnUrl();
            ViewBag.Companies = new SelectList(companyService.GetTestingCompanies(), "ID", "Name");
            ViewBag.Boards= new SelectList(scoreboardService.GetScoreboards(), "ID", "Name");
            ViewBag.Status = new SelectList(ShrdMaster.Instance.Getstatus(), "ID", "Name");
            return View();
        }

        // POST: TestScores/Create
        [HttpPost]
        public ActionResult Create(TestScore model)
        {
            SetReturnUrl();
            if (ModelState.IsValid)
            {
                testScoreService.CreateTestScore(model);
                testScoreService.Save();

                return RedirectToAction(returnUrl);
            }
            ViewBag.Companies = new SelectList(companyService.GetTestingCompanies(), "ID", "Name");
            ViewBag.Boards = new SelectList(scoreboardService.GetScoreboards(), "ID", "Name");
            ViewBag.Status = new SelectList(ShrdMaster.Instance.Getstatus(), "ID", "Name");
            return View(model);
            //try
            //{
            //    // TODO: Add insert logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
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

        public void SetReturnUrl()
        {
            //to go to previous page
            if (Request.QueryString["returnUrl"] != null)
            {
                returnUrl = Request.QueryString["returnUrl"];
            }

            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/TestScores/Index";
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
