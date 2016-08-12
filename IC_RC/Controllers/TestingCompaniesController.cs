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
    public class TestingCompaniesController : Controller
    {

        
        public readonly ITestingCompanyService companyService;

        public TestingCompaniesController(ITestingCompanyService companyService)
        {
            this.companyService = companyService;            
        }
        // GET: TestingCompany
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult  GetData()
        {
            var data = companyService.GetTestingCompanies();

            return PartialView("_TestingCompany", data);

        }


        // GET: TestingCompany/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestingCompany/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestingCompany/Create
        [HttpPost]
        public ActionResult Create(TestingCompany model)
        {
            if(ModelState.IsValid)
            {
                companyService.CreateCompany(model);
                companyService.Save();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: TestingCompany/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return RedirectToActionPermanent("PageNotFound", "Home");
            }
            var data = companyService.GetTestingCompanyByID(id.Value);
            if(data==null)
            {
                return RedirectToActionPermanent("PageNotFound", "Home");
            }
            return View(data);
        }

        // POST: TestingCompany/Edit/5
        [HttpPost]
        public ActionResult Edit(TestingCompany model)
        {
            if(ModelState.IsValid)
            {
                companyService.UpdateCompany(model);
                companyService.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: TestingCompany/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestingCompany/Delete/5
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
