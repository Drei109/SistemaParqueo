using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaParqueo.Models;

namespace SistemaParqueo.Areas.Admin.Controllers
{
    public class PersonasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Personas
        public ActionResult Index()
        {
            var personas = db.Persona.Include(p => p.PersonaEstado).Include(p => p.User);
            return View(personas.ToList());
        }

        // GET: Admin/Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Admin/Personas/Create
        public ActionResult Create()
        {
            ViewBag.PersonaEstadoId = new SelectList(db.PersonaEstado, "PersonaEstadoId", "Descripcion");
            ViewBag.AspNetUsersId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Admin/Personas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonaId,Nombre,Apellido,DNI,Telefono,Celular,Direccion,Email,PersonaEstadoId,AspNetUsersId")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Persona.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonaEstadoId = new SelectList(db.PersonaEstado, "PersonaEstadoId", "Descripcion", persona.PersonaEstadoId);
            ViewBag.AspNetUsersId = new SelectList(db.Users, "Id", "Email", persona.AspNetUsersId);
            return View(persona);
        }

        // GET: Admin/Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonaEstadoId = new SelectList(db.PersonaEstado, "PersonaEstadoId", "Descripcion", persona.PersonaEstadoId);
            ViewBag.AspNetUsersId = new SelectList(db.Users, "Id", "Email", persona.AspNetUsersId);
            return View(persona);
        }

        // POST: Admin/Personas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonaId,Nombre,Apellido,DNI,Telefono,Celular,Direccion,Email,PersonaEstadoId,AspNetUsersId")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonaEstadoId = new SelectList(db.PersonaEstado, "PersonaEstadoId", "Descripcion", persona.PersonaEstadoId);
            ViewBag.AspNetUsersId = new SelectList(db.Users, "Id", "Email", persona.AspNetUsersId);
            return View(persona);
        }

        // GET: Admin/Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Admin/Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Persona.Find(id);
            db.Persona.Remove(persona);
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
