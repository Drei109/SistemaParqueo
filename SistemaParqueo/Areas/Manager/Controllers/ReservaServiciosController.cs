using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaParqueo.Models;

namespace SistemaParqueo.Areas.Manager.Controllers
{
    public class ReservaServiciosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager/ReservaServicios
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create(int? id)
        {
            var servicioId = db.Reserva.Find(id).ServicioId;
            var cocheraId = db.Servicio.Find(servicioId).CocheraId;
            var serviciosCochera = db.Servicio.Where(m => m.CocheraId == cocheraId).ToList();
            ViewBag.serviciosCochera = new SelectList(serviciosCochera, "ServicioId", "Descripcion");
            ViewBag.reservaId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservaServicios reservaServicios)
        {
            ModelState.Remove("Costo");
            if (ModelState.IsValid)
            {
                var costoServicio = db.Servicio.Find(reservaServicios.ServicioId).Costo;
                reservaServicios.Costo = (decimal) (reservaServicios.Cantidad * costoServicio);
                
                db.ReservaServicios.Add(reservaServicios);
                db.SaveChanges();

                return RedirectToAction("Index","Reservas");
            }

            var servicioId = db.Reserva.Find(reservaServicios.ReservaId).ServicioId;
            var cocheraId = db.Servicio.Find(servicioId).CocheraId;
            var serviciosCochera = db.Servicio.Where(m => m.CocheraId == cocheraId).ToList();
            ViewBag.serviciosCochera = new SelectList(serviciosCochera, "ServicioId", "Descripcion");
            ViewBag.reservaId = reservaServicios.ReservaId;
            return View("Create");
        }
    }
}