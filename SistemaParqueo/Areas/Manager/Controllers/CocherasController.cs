using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SistemaParqueo.Models;

namespace SistemaParqueo.Areas.Manager.Controllers
{
    public class CocherasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager/Cocheras
        public ActionResult Index()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (userId != null)
            {
                var user = db.Users.Find(userId);
                var cocherasUsu = db.Cochera.Include(c => c.CocheraEstado).Include(c => c.Empresa).Where(m=>m.EmpresaId == user.EmpresaId);
                return View(cocherasUsu.ToList());
            }
            var cochera = db.Cochera.Include(c => c.CocheraEstado).Include(c => c.Empresa);
            return View(cochera.ToList());
        }

        // GET: Manager/Cocheras/Details/5
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

        // GET: Manager/Cocheras/Create
        public ActionResult Create()
        {
            ViewBag.CocheraEstadoId = new SelectList(db.CocheraEstado, "CocheraEstadoId", "Descripcion");
            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre");
            return View();
        }

        // POST: Manager/Cocheras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CocheraId,Nombre,Direccion,Descripcion,Longitud,Latitud,EmpresaId,CocheraEstadoId,CodigoPostal")] Cochera cochera, HttpPostedFileBase fotoFile)
        {
            
            if (ModelState.IsValid)
            {
                db.Cochera.Add(cochera);
                db.SaveChanges();

                var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");

                cochera.Foto = timeStamp + ".png";
                db.SaveChanges();
                fotoFile?.SaveAs(Server.MapPath("~/Uploads/Cocheras/" + cochera.Foto));

                return RedirectToAction("Index");
            }

            ViewBag.CocheraEstadoId = new SelectList(db.CocheraEstado, "CocheraEstadoId", "Descripcion", cochera.CocheraEstadoId);
            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre", cochera.EmpresaId);
            return View(cochera);
        }

        // GET: Manager/Cocheras/Edit/5
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

        // POST: Manager/Cocheras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cochera cochera, HttpPostedFileBase fotoFile)
        {

            if (ModelState.IsValid)
            {

                db.Entry(cochera).State = EntityState.Modified;
                db.SaveChanges();

                if (fotoFile != null)
                {
                    var fotoAntigua = Request.MapPath("~/Uploads/Cocheras/" + cochera.Foto);
                    if (System.IO.File.Exists(fotoAntigua))
                    {
                        System.IO.File.Delete(fotoAntigua);
                    }

                    var timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    cochera.Foto = timeStamp + ".png";
                    db.SaveChanges();
                    fotoFile?.SaveAs(Server.MapPath("~/Uploads/Cocheras/" + cochera.Foto));
                }

                return RedirectToAction("Index");
            }
            ViewBag.CocheraEstadoId = new SelectList(db.CocheraEstado, "CocheraEstadoId", "Descripcion", cochera.CocheraEstadoId);
            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre", cochera.EmpresaId);
            return View(cochera);
        }

        // GET: Manager/Cocheras/Delete/5
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

        // POST: Manager/Cocheras/Delete/5
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
