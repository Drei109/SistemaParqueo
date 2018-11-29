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
    public class CocheraUsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CocheraUsuarios
        public ActionResult Index()
        {
            var cocheraUsuarios = db.CocheraUsuario.Include(c => c.Cochera).Include(c => c.User);
            return View(cocheraUsuarios.ToList());
        }

        // GET: Admin/CocheraUsuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CocheraUsuario cocheraUsuario = db.CocheraUsuario.Find(id);
            if (cocheraUsuario == null)
            {
                return HttpNotFound();
            }
            return View(cocheraUsuario);
        }

        // GET: Admin/CocheraUsuarios/Create
        public ActionResult Create()
        {
            ViewBag.CocheraId = new SelectList(db.Cochera, "CocheraId", "Nombre");
            ViewBag.AspNetUsersId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Admin/CocheraUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CocheraUsuarioId,CocheraId,AspNetUsersId")] CocheraUsuario cocheraUsuario)
        {
            if (ModelState.IsValid)
            {
                db.CocheraUsuario.Add(cocheraUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CocheraId = new SelectList(db.Cochera, "CocheraId", "Nombre", cocheraUsuario.CocheraId);
            ViewBag.AspNetUsersId = new SelectList(db.Users, "Id", "Email", cocheraUsuario.AspNetUsersId);
            return View(cocheraUsuario);
        }

        // GET: Admin/CocheraUsuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CocheraUsuario cocheraUsuario = db.CocheraUsuario.Find(id);
            if (cocheraUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.CocheraId = new SelectList(db.Cochera, "CocheraId", "Nombre", cocheraUsuario.CocheraId);
            ViewBag.AspNetUsersId = new SelectList(db.Users, "Id", "Email", cocheraUsuario.AspNetUsersId);
            return View(cocheraUsuario);
        }

        // POST: Admin/CocheraUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CocheraUsuarioId,CocheraId,AspNetUsersId")] CocheraUsuario cocheraUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cocheraUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CocheraId = new SelectList(db.Cochera, "CocheraId", "Nombre", cocheraUsuario.CocheraId);
            ViewBag.AspNetUsersId = new SelectList(db.Users, "Id", "Email", cocheraUsuario.AspNetUsersId);
            return View(cocheraUsuario);
        }

        // GET: Admin/CocheraUsuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CocheraUsuario cocheraUsuario = db.CocheraUsuario.Find(id);
            if (cocheraUsuario == null)
            {
                return HttpNotFound();
            }
            return View(cocheraUsuario);
        }

        // POST: Admin/CocheraUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CocheraUsuario cocheraUsuario = db.CocheraUsuario.Find(id);
            db.CocheraUsuario.Remove(cocheraUsuario ?? throw new InvalidOperationException());
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
