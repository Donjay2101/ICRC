using ICRCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IC_RC.Controllers
{
    public class FileMakerController : Controller
    {
        public readonly IFileMakerService FileMakerService;
        public FileMakerController(IFileMakerService FileMakerService)
        {
            this.FileMakerService = FileMakerService;
        }
        // GET: FileMaker
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Certificates()
        {
            var data = FileMakerService.GetFileMakerData().ToList();

            return PartialView("_FileMaker",data);
        }

        public ActionResult Reciprocities()
        {
            var data = FileMakerService.GetReciprocities().ToList();

            return PartialView("_FileMakerReciprocities", data);
        }

        // GET: FileMaker/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FileMaker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileMaker/Create
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

        // GET: FileMaker/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FileMaker/Edit/5
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

        // GET: FileMaker/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FileMaker/Delete/5
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
