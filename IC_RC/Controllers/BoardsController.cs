using ICRC.Model;
using ICRCService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IC_RC.Models;

namespace IC_RC.Controllers
{
    [Authorize]
    public class BoardsController : Controller
    {
        public readonly IBoardService boardService;

        public BoardsController(IBoardService boardService)
        {
            this.boardService = boardService;
        }


        // GET: Boards
        public ActionResult Index()
        {
            
            
            return View();
        }


        public ActionResult GetData()
        {            
            var data = boardService.GetBoards();
            return PartialView("_Boards", data);
        }

        // GET: Boards/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Boards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boards/Create
        [HttpPost]
        public ActionResult Create(Boards model)
        {
            if(ModelState.IsValid)
            {
                boardService.CreateBoard(model);
                boardService.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Boards/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return RedirectToActionPermanent("PageNotFound","Home");
            }
            var data = boardService.GetBoardByID(id.Value);
            if(data==null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

        // POST: Boards/Edit/5
        [HttpPost]
        public ActionResult Edit(Boards model)
        {
           if(ModelState.IsValid)
            {
                boardService.UpdateBoard(model);
                boardService.Save();

                return RedirectToAction("Index");
            }

            return View(model);

        }

        // GET: Boards/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Boards/Delete/5
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
