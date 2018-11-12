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
    public class CocherasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Cocheras
        public ActionResult Index()
        {
            var cochera = db.Cochera.Include(c => c.CocheraEstado).Include(c => c.Empresa);
            return View(cochera.ToList());
        }

        // GET: Admin/Cocheras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cochera cochera = db.Cochera.Find(id);
            if (cochera == null)
            {
                return HttpNotFound();
            }
            return View(cochera);
        }

        // GET: Admin/Cocheras/Create
        public ActionResult Create()
        {
            ViewBag.CocheraEstadoId = new SelectList(db.CocheraEstado, "CocheraEstadoId", "Descripcion");
            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre");
            return View();
        }

        // POST: Admin/Cocheras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CocheraId,Nombre,Direccion,Descripcion,Longitud,Latitud,Foto,EmpresaId,CocheraEstadoId,CodigoPostal")] Cochera cochera)
        {
            if (ModelState.IsValid)
            {
                db.Cochera.Add(cochera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CocheraEstadoId = new SelectList(db.CocheraEstado, "CocheraEstadoId", "Descripcion", cochera.CocheraEstadoId);
            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre", cochera.EmpresaId);
            return View(cochera);
        }

        // GET: Admin/Cocheras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cochera cochera = db.Cochera.Find(id);
            if (cochera == null)
            {
                return HttpNotFound();
            }
            ViewBag.CocheraEstadoId = new SelectList(db.CocheraEstado, "CocheraEstadoId", "Descripcion", cochera.CocheraEstadoId);
            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre", cochera.EmpresaId);
            return View(cochera);
        }

        // POST: Admin/Cocheras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CocheraId,Nombre,Direccion,Descripcion,Longitud,Latitud,Foto,EmpresaId,CocheraEstadoId,CodigoPostal")] Cochera cochera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cochera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CocheraEstadoId = new SelectList(db.CocheraEstado, "CocheraEstadoId", "Descripcion", cochera.CocheraEstadoId);
            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre", cochera.EmpresaId);
            return View(cochera);
        }

        // GET: Admin/Cocheras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cochera cochera = db.Cochera.Find(id);
            if (cochera == null)
            {
                return HttpNotFound();
            }
            return View(cochera);
        }

        // POST: Admin/Cocheras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cochera cochera = db.Cochera.Find(id);
            db.Cochera.Remove(cochera);
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
