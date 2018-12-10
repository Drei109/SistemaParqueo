using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SistemaParqueo.Models;

namespace SistemaParqueo.Areas.Manager.Controllers
{
    public class BoletaCabecerasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager/BoletaCabeceras
        public ActionResult Index()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            if (userId != null)
            {
                var user = db.Users.Find(userId);
                var boletaCabeceraUsuario = db.BoletaCabecera
                    .Include(b => b.Cliente)
                    .Include(b => b.Reserva)
                    .Where(b => b.Reserva.Servicio.Cochera.EmpresaId == user.EmpresaId);
                return View(boletaCabeceraUsuario.ToList());
            }
            var boletaCabecera = db.BoletaCabecera.Include(b => b.Cliente).Include(b => b.Reserva);
            return View(boletaCabecera.ToList());
        }

        // GET: Manager/BoletaCabeceras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoletaCabecera boletaCabecera = db.BoletaCabecera.Find(id);
            if (boletaCabecera == null)
            {
                return HttpNotFound();
            }
            return View(boletaCabecera);
        }

        // GET: Manager/BoletaCabeceras/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "Nombre");
            ViewBag.ReservaId = new SelectList(db.Reserva, "ReservaId", "ReservaId");
            return View();
        }

        // POST: Manager/BoletaCabeceras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BoletaCabeceraId,ReservaId,Fecha,ClienteId,Estado,Subtotal,Total")] BoletaCabecera boletaCabecera)
        {
            if (ModelState.IsValid)
            {
                db.BoletaCabecera.Add(boletaCabecera);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "Nombre", boletaCabecera.ClienteId);
            ViewBag.ReservaId = new SelectList(db.Reserva, "ReservaId", "ReservaId", boletaCabecera.ReservaId);
            return View(boletaCabecera);
        }

        // GET: Manager/BoletaCabeceras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoletaCabecera boletaCabecera = db.BoletaCabecera.Find(id);
            if (boletaCabecera == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "Nombre", boletaCabecera.ClienteId);
            ViewBag.ReservaId = new SelectList(db.Reserva, "ReservaId", "ReservaId", boletaCabecera.ReservaId);
            return View(boletaCabecera);
        }

        // POST: Manager/BoletaCabeceras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BoletaCabeceraId,ReservaId,Fecha,ClienteId,Estado,Subtotal,Total")] BoletaCabecera boletaCabecera)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boletaCabecera).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "Nombre", boletaCabecera.ClienteId);
            ViewBag.ReservaId = new SelectList(db.Reserva, "ReservaId", "ReservaId", boletaCabecera.ReservaId);
            return View(boletaCabecera);
        }

        // GET: Manager/BoletaCabeceras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoletaCabecera boletaCabecera = db.BoletaCabecera.Find(id);
            if (boletaCabecera == null)
            {
                return HttpNotFound();
            }
            return View(boletaCabecera);
        }

        // POST: Manager/BoletaCabeceras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoletaCabecera boletaCabecera = db.BoletaCabecera.Find(id);
            db.BoletaCabecera.Remove(boletaCabecera);
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
