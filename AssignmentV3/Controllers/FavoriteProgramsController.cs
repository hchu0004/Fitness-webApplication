using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentV3.Models;
using Microsoft.AspNet.Identity;

namespace AssignmentV3.Controllers
{
    [Authorize]
    public class FavoriteProgramsController : Controller
    {
        private BusinessDBContainer db = new BusinessDBContainer();

        // GET: FavoritePrograms
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var favoritePrograms = db.FavoritePrograms.Where(f => f.UserId == userId).ToList();
            return View(favoritePrograms);
        }

        // GET: FavoritePrograms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteProgram favoriteProgram = db.FavoritePrograms.Find(id);
            if (favoriteProgram == null)
            {
                return HttpNotFound();
            }
            return View(favoriteProgram);
        }

        // GET: FavoritePrograms/Create
        public ActionResult Create()
        {
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName");
            return View();
        }

        // POST: FavoritePrograms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProgramId")] FavoriteProgram favoriteProgram)
        {
            favoriteProgram.UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.FavoritePrograms.Add(favoriteProgram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName", favoriteProgram.ProgramId);
            return View(favoriteProgram);
        }

        // GET: FavoritePrograms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FavoriteProgram favoriteProgram = db.FavoritePrograms.Find(id);
            if (favoriteProgram == null)
            {
                return HttpNotFound();
            }
            return View(favoriteProgram);
        }

        // POST: FavoritePrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FavoriteProgram favoriteProgram = db.FavoritePrograms.Find(id);
            db.FavoritePrograms.Remove(favoriteProgram);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void HandleUnknownAction(string actionName)
        {
            Response.Redirect("~/Home/Index");
            base.HandleUnknownAction(actionName);
        }
    }
}
