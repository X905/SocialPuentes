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
    public class ValoracionesController : Controller
    {
        private Db_CPuentesEntities db = new Db_CPuentesEntities();

        // GET: Valoraciones
        public ActionResult Index()
        {
            var valoraciones = db.Valoraciones.Include(v => v.Publicacione).Include(v => v.Usuario);
            return View(valoraciones.ToList());
        }

        // GET: Valoraciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valoracione valoracione = db.Valoraciones.Find(id);
            if (valoracione == null)
            {
                return HttpNotFound();
            }
            return View(valoracione);
        }

        // GET: Valoraciones/Create
        public ActionResult Create()
        {
            ViewBag.Id_Publicacion = new SelectList(db.Publicaciones, "Id_Publicacion", "Nombre_Usuario");
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name");
            return View();
        }

        // POST: Valoraciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Valoracion,Id_Usuario,Nombre_Usuario,Id_Publicacion,Valoracion")] Valoracione valoracione)
        {
            if (ModelState.IsValid)
            {
                db.Valoraciones.Add(valoracione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Publicacion = new SelectList(db.Publicaciones, "Id_Publicacion", "Nombre_Usuario", valoracione.Id_Publicacion);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", valoracione.Id_Usuario);
            return View(valoracione);
        }

        // GET: Valoraciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valoracione valoracione = db.Valoraciones.Find(id);
            if (valoracione == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Publicacion = new SelectList(db.Publicaciones, "Id_Publicacion", "Nombre_Usuario", valoracione.Id_Publicacion);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", valoracione.Id_Usuario);
            return View(valoracione);
        }

        // POST: Valoraciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Valoracion,Id_Usuario,Nombre_Usuario,Id_Publicacion,Valoracion")] Valoracione valoracione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(valoracione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Publicacion = new SelectList(db.Publicaciones, "Id_Publicacion", "Nombre_Usuario", valoracione.Id_Publicacion);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", valoracione.Id_Usuario);
            return View(valoracione);
        }

        // GET: Valoraciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Valoracione valoracione = db.Valoraciones.Find(id);
            if (valoracione == null)
            {
                return HttpNotFound();
            }
            return View(valoracione);
        }

        // POST: Valoraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Valoracione valoracione = db.Valoraciones.Find(id);
            db.Valoraciones.Remove(valoracione);
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
