using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialPuentes.Models;

namespace SocialPuentes.Controllers
{
    public class CedesController : Controller
    {
        private Db_CPuentesEntities db = new Db_CPuentesEntities();

        // GET: Cedes
        public ActionResult Index()
        {
            return View(db.Cedes.ToList());
        }

        // GET: Cedes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cede cede = db.Cedes.Find(id);
            if (cede == null)
            {
                return HttpNotFound();
            }
            return View(cede);
        }

        // GET: Cedes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cedes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Cede,CedeNombre,Ubicacion")] Cede cede)
        {
            if (ModelState.IsValid)
            {
                db.Cedes.Add(cede);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cede);
        }

        // GET: Cedes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cede cede = db.Cedes.Find(id);
            if (cede == null)
            {
                return HttpNotFound();
            }
            return View(cede);
        }

        // POST: Cedes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Cede,CedeNombre,Ubicacion")] Cede cede)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cede).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cede);
        }

        // GET: Cedes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cede cede = db.Cedes.Find(id);
            if (cede == null)
            {
                return HttpNotFound();
            }
            return View(cede);
        }

        // POST: Cedes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cede cede = db.Cedes.Find(id);
            db.Cedes.Remove(cede);
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
