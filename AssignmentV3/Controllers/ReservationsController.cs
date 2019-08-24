using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentV3.Models;
using AssignmentV3.Utils;
using Microsoft.AspNet.Identity;

namespace AssignmentV3.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private BusinessDBContainer db = new BusinessDBContainer();

        // GET: Reservations
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var reservations = db.Reservations.Where(r => r.UserId == userId).ToList();
            return View(reservations);
        }
        
        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProgramId,DatetimeStart,DatetimeEnd,Comment")] Reservation reservation)
        {
            reservation.memberEmail = User.Identity.GetUserName();
            reservation.UserId = User.Identity.GetUserId();            
            reservation.Status = "Approved";
            if (ModelState.IsValid)
            {
                
                db.Reservations.Add(reservation);
                db.SaveChanges();

                try
                {
                    String toEmail = reservation.memberEmail;
                    String subject = "Fitness Center - Confirmation of booking";
                    String contents = "This is the confirmatin of your booking";
                    Program tempProgram = db.Programs.Find(reservation.ProgramId);
                    String program = tempProgram.ProgramName.ToString();
                    //String program = reservation.ProgramId.ToString();
                    String starttime = reservation.DatetimeStart.ToString();
                    String endtiem = reservation.DatetimeEnd.ToString();

                    EmailSender es = new EmailSender();
                    es.ReservationSendMail(toEmail, subject, contents, program, starttime, endtiem);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();
                }
                catch
                {
                    return View();
                }

                return RedirectToAction("Index");
            }

            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName", reservation.ProgramId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName", reservation.ProgramId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProgramId,DatetimeStart,DatetimeEnd,Comment")] Reservation reservation)
        {
            reservation.memberEmail = User.Identity.GetUserName();
            reservation.UserId = User.Identity.GetUserId();
            reservation.Status = "Approved";
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();

                try
                {
                    String toEmail = reservation.memberEmail;
                    String subject = "Fitness Center - Confirmation of booking";
                    String contents = "This is the confirmatin of your booking";
                    Program tempProgram = db.Programs.Find(reservation.ProgramId);
                    String program = tempProgram.ProgramName.ToString();
                    //String program = reservation.ProgramId.ToString();
                    String starttime = reservation.DatetimeStart.ToString();
                    String endtiem = reservation.DatetimeEnd.ToString();

                    EmailSender es = new EmailSender();
                    es.ReservationSendMail(toEmail, subject, contents, program, starttime, endtiem);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();
                }
                catch
                {
                    return View();
                }

                return RedirectToAction("Index");
            }
            ViewBag.ProgramId = new SelectList(db.Programs, "ProgramId", "ProgramName", reservation.ProgramId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
