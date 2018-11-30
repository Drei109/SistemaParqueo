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
    public class UserRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/UserRoles
        public ActionResult Index()
        {
            var cocheraUsuarios = db.CocheraUsuario.Include(c => c.Cochera).Include(c => c.User);
            return View(cocheraUsuarios.ToList());
        }

        // GET: Admin/UserRoles/Details/5
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

        // GET: Admin/UserRoles/Create
        public ActionResult Create()
        {
            ViewBag.CocheraId = new SelectList(db.Cochera, "CocheraId", "Nombre");
            ViewBag.AspNetUsersId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Admin/UserRoles/Create
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

        // GET: Admin/UserRoles/Edit/5
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

        // POST: Admin/UserRoles/Edit/5
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

        // GET: Admin/UserRoles/Delete/5
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

        // POST: Admin/UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CocheraUsuario cocheraUsuario = db.CocheraUsuario.Find(id);
            db.CocheraUsuario.Remove(cocheraUsuario);
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
