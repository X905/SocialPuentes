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
    public class PublicacionesController : Controller
    {
        private Db_CPuentesEntities db = new Db_CPuentesEntities();

        // GET: Publicaciones
        public ActionResult Index()
        {
            var publicaciones = db.Publicaciones.Include(p => p.Modulo).Include(p => p.Usuario);
            return View(publicaciones.ToList());
        }

        // GET: Publicaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacione publicacione = db.Publicaciones.Find(id);
            if (publicacione == null)
            {
                return HttpNotFound();
            }
            return View(publicacione);
        }

        // GET: Publicaciones/Create
        public ActionResult Create()
        {
            ViewBag.Id_Categoria = new SelectList(db.Modulos, "Id_Categoria", "CategoriaNombre");
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name");
            return View();
        }

        // POST: Publicaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Publicacion,Id_Usuario,Nombre_Usuario,Tema,Id_Categoria,Publicacion,URL_Imagen,URL_Archivo,Fecha_Publicacion,Estado")] Publicacione publicacione)
        {
            if (ModelState.IsValid)
            {
                db.Publicaciones.Add(publicacione);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Categoria = new SelectList(db.Modulos, "Id_Modulos", "ModulosNombre", publicacione.Id_Modulos);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", publicacione.Id_Usuario);
            return View(publicacione);
        }

        // GET: Publicaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacione publicacione = db.Publicaciones.Find(id);
            if (publicacione == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Categoria = new SelectList(db.Modulos, "Id_Modulos", "ModulosNombre", publicacione.Id_Modulos);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", publicacione.Id_Usuario);
            return View(publicacione);
        }

        // POST: Publicaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Publicacion,Id_Usuario,Nombre_Usuario,Tema,Id_Categoria,Publicacion,URL_Imagen,URL_Archivo,Fecha_Publicacion,Estado")] Publicacione publicacione)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publicacione).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Categoria = new SelectList(db.Modulos, "Id_Modulos", "ModulosNombre", publicacione.Id_Modulos);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", publicacione.Id_Usuario);
            return View(publicacione);
        }

        // GET: Publicaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publicacione publicacione = db.Publicaciones.Find(id);
            if (publicacione == null)
            {
                return HttpNotFound();
            }
            return View(publicacione);
        }

        // POST: Publicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publicacione publicacione = db.Publicaciones.Find(id);
            db.Publicaciones.Remove(publicacione);
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
