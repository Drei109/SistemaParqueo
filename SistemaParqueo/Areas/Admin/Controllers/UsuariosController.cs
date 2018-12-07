using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SistemaParqueo.Models;

namespace SistemaParqueo.Areas.Admin.Controllers
{
    public class UsuariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Usuarios
        public ActionResult Index()
        {
            var applicationUsers = db.Users.Include(a => a.Empresa);
            return View(applicationUsers.ToList());
        }

        // GET: Admin/Usuarios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: Admin/Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre");
            return View();
        }

        // POST: Admin/Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmpresaId,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre", applicationUser.EmpresaId);
            return View(applicationUser);
        }

        // GET: Admin/Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var userRole = userManager.GetRoles(id).ToList().FirstOrDefault();
            var roles = roleManager.Roles.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre", applicationUser.EmpresaId);
            ViewBag.RoleId = new SelectList(roles, "Name", "Name", userRole);

            return View(applicationUser);
        }

        // POST: Admin/Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser applicationUser, string RoleId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                //var oldUserRole = db.Users.Find(applicationUser.Id).Roles.ToList().FirstOrDefault(m => m.UserId == applicationUser.Id)?.RoleId;
                //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

                //var oldUserRole = Roles.Provider.GetRolesForUser(applicationUser.UserName);
                //var test = oldUserRole.ToList().FirstOrDefault();

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var oldUserRoles = userManager.GetRoles(applicationUser.Id);
                userManager.RemoveFromRoles(applicationUser.Id, oldUserRoles.ToArray());
                userManager.AddToRole(applicationUser.Id, RoleId);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmpresaId = new SelectList(db.Empresa, "EmpresaId", "Nombre", applicationUser.EmpresaId);
            return View(applicationUser);
        }

        // GET: Admin/Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Admin/Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
