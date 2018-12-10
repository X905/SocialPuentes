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
    public class ComentariosController : Controller
    {
        private Db_CPuentesEntities db = new Db_CPuentesEntities();

        // GET: Comentarios
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Publicacione).Include(c => c.Usuario);
            return View(comentarios.ToList());
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: Comentarios/Create
        public ActionResult Create()
        {
            ViewBag.Id_Publicacion = new SelectList(db.Publicaciones, "Id_Publicacion", "Nombre_Usuario");
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Comentario,Id_Publicacion,Id_Usuario,Nombre_Usuario,Comentario1,URL_Imagen,URL_Archivo,Estado")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Publicacion = new SelectList(db.Publicaciones, "Id_Publicacion", "Nombre_Usuario", comentario.Id_Publicacion);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", comentario.Id_Usuario);
            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Publicacion = new SelectList(db.Publicaciones, "Id_Publicacion", "Nombre_Usuario", comentario.Id_Publicacion);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", comentario.Id_Usuario);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Comentario,Id_Publicacion,Id_Usuario,Nombre_Usuario,Comentario1,URL_Imagen,URL_Archivo,Estado")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Publicacion = new SelectList(db.Publicaciones, "Id_Publicacion", "Nombre_Usuario", comentario.Id_Publicacion);
            ViewBag.Id_Usuario = new SelectList(db.Usuarios, "Id_Usuario", "Full_Name", comentario.Id_Usuario);
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentario);
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
