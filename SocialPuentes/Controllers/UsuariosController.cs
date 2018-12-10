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
    public class UsuariosController : Controller
    {
        private Db_CPuentesEntities db = new Db_CPuentesEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Cede).Include(u => u.Especialidade).Include(u => u.Role);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cede = new SelectList(db.Cedes, "Id_Cede", "CedeNombre");
            ViewBag.Id_Especialidad = new SelectList(db.Especialidades, "Id_Especialidad", "EspecialidadNombre");
            ViewBag.Id_Rol = new SelectList(db.Roles, "Id_Rol", "Rol");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Usuario,Full_Name,ImagenPerfil,FechaNacimiento,Direccion,Correo,Telefono,Id_Cede,Id_Especialidad,Contraseña,ConfirmarCrontraseña,Id_Rol,Estado,Fecha_Registro")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cede = new SelectList(db.Cedes, "Id_Cede", "CedeNombre", usuario.Id_Cede);
            ViewBag.Id_Especialidad = new SelectList(db.Especialidades, "Id_Especialidad", "EspecialidadNombre", usuario.Id_Especialidad);
            ViewBag.Id_Rol = new SelectList(db.Roles, "Id_Rol", "Rol", usuario.Id_Rol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cede = new SelectList(db.Cedes, "Id_Cede", "CedeNombre", usuario.Id_Cede);
            ViewBag.Id_Especialidad = new SelectList(db.Especialidades, "Id_Especialidad", "EspecialidadNombre", usuario.Id_Especialidad);
            ViewBag.Id_Rol = new SelectList(db.Roles, "Id_Rol", "Rol", usuario.Id_Rol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Usuario,Full_Name,ImagenPerfil,FechaNacimiento,Direccion,Correo,Telefono,Id_Cede,Id_Especialidad,Contraseña,ConfirmarCrontraseña,Id_Rol,Estado,Fecha_Registro")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cede = new SelectList(db.Cedes, "Id_Cede", "CedeNombre", usuario.Id_Cede);
            ViewBag.Id_Especialidad = new SelectList(db.Especialidades, "Id_Especialidad", "EspecialidadNombre", usuario.Id_Especialidad);
            ViewBag.Id_Rol = new SelectList(db.Roles, "Id_Rol", "Rol", usuario.Id_Rol);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Login(Usuario usuario)
        {
            ViewBag.Id_Cede = new SelectList(db.Cedes, "Id_Cede", "CedeNombre", usuario.Id_Cede);
            ViewBag.Id_Especialidad = new SelectList(db.Especialidades, "Id_Especialidad", "EspecialidadNombre", usuario.Id_Especialidad);
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Nombre, string Apellido, DateTime Fecha, string Email, string Pass, string PassConfirm, string CorreoLog, string PassLog, int cede, int Especialidad)
        {

            TimeZoneInfo zona = TimeZoneInfo.FindSystemTimeZoneById("central America standard Time");
            DateTime HoraES = TimeZoneInfo.ConvertTime(DateTime.Now, zona);

            string Valid = Nombre.Trim();

            if (Valid != "")
            {
                var registrar = new Usuario
                {
                    Nombres = Nombre,
                    Apellidos = Apellido,
                    FechaNacimiento = Fecha,
                    Correo = Email,
                    Contraseña = Pass,
                    ConfirmarCrontraseña = PassConfirm,
                    Id_Cede = cede,
                    Id_Especialidad = Especialidad,
                    Id_Rol = 1,
                    Estado = false,
                    Fecha_Registro = HoraES,
                };

                db.Usuarios.Add(registrar);
                db.SaveChanges();
            }
            
            
            return View();
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
