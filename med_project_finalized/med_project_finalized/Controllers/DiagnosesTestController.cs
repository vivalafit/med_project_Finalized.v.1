using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using med_project_finalized.Models;

namespace med_project_finalized.Controllers
{
    public class DiagnosesTestController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DiagnosesTest
        public ActionResult Index()
        {
            return View(db.Diagnoses.ToList());
        }

        // GET: DiagnosesTest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnoses diagnoses = db.Diagnoses.Find(id);
            if (diagnoses == null)
            {
                return HttpNotFound();
            }
            return View(diagnoses);
        }

        // GET: DiagnosesTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiagnosesTest/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Diagnosis,Details,TimeAdded,IsCured,UserId")] Diagnoses diagnoses)
        {
            if (ModelState.IsValid)
            {
                db.Diagnoses.Add(diagnoses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diagnoses);
        }

        // GET: DiagnosesTest/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnoses diagnoses = db.Diagnoses.Find(id);
            if (diagnoses == null)
            {
                return HttpNotFound();
            }
            return View(diagnoses);
        }

        // POST: DiagnosesTest/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Diagnosis,Details,TimeAdded,IsCured,UserId")] Diagnoses diagnoses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnoses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diagnoses);
        }

        // GET: DiagnosesTest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnoses diagnoses = db.Diagnoses.Find(id);
            if (diagnoses == null)
            {
                return HttpNotFound();
            }
            return View(diagnoses);
        }

        // POST: DiagnosesTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diagnoses diagnoses = db.Diagnoses.Find(id);
            db.Diagnoses.Remove(diagnoses);
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
    }
}
