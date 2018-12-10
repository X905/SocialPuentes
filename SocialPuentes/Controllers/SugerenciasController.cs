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
    public class SugerenciasController : Controller
    {
        private Db_CPuentesEntities db = new Db_CPuentesEntities();

        // GET: Sugerencias
        public ActionResult Index()
        {
            var sugerencias = db.Sugerencias.Include(s => s.Usuario);
            return View(sugerencias.ToList());
        }

        // GET: Sugerencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sugerencia sugerencia = db.Sugerencias.Find(id);
            if (sugerencia == null)
            {
                return HttpNotFound();
            }
            return View(sugerencia);
        }

        // GET: Sugerencias/Create
        public ActionResult Create()
        {
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name");
            return View();
        }

        // POST: Sugerencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Sugerencias,Id_Usuario,Nombre_Usuario,Sugerencia1,Fecha")] Sugerencia sugerencia)
        {
            if (ModelState.IsValid)
            {
                db.Sugerencias.Add(sugerencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", sugerencia.Id_Usuario);
            return View(sugerencia);
        }

        // GET: Sugerencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sugerencia sugerencia = db.Sugerencias.Find(id);
            if (sugerencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", sugerencia.Id_Usuario);
            return View(sugerencia);
        }

        // POST: Sugerencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Sugerencias,Id_Usuario,Nombre_Usuario,Sugerencia1,Fecha")] Sugerencia sugerencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sugerencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", sugerencia.Id_Usuario);
            return View(sugerencia);
        }

        // GET: Sugerencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sugerencia sugerencia = db.Sugerencias.Find(id);
            if (sugerencia == null)
            {
                return HttpNotFound();
            }
            return View(sugerencia);
        }

        // POST: Sugerencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sugerencia sugerencia = db.Sugerencias.Find(id);
            db.Sugerencias.Remove(sugerencia);
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
