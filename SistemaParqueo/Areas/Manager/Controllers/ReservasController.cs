using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaParqueo.Areas.Manager.Models;
using SistemaParqueo.Models;

namespace SistemaParqueo.Areas.Manager.Controllers
{
    public class ReservasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manager/Reservas
        public ActionResult Index()
        {
            var reserva = db.Reserva.Include(r => r.ReservaEstado).Include(r => r.Servicio).Include(r => r.Vehiculo).Include(r=>r.ReservaServicios).Where(r => r.ReservaEstadoId == EstadoReserva.Enviado || r.ReservaEstadoId == EstadoReserva.Ocupado);
            return View(reserva.ToList());
        }

        // GET: Manager/Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reserva.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Manager/Reservas/Create
        public ActionResult Create(string clienteDNI)
        {
            ViewBag.ReservaEstadoId = new SelectList(db.ReservaEstado, "ReservaEstadoId", "Descripcion");
            ViewBag.ServicioId = new SelectList(db.Servicio, "ServicioId", "Descripcion");
            ViewBag.VehiculoId = new SelectList(db.Vehiculo, "VehiculoId", "Placa");
            return View();
        }

        // POST: Manager/Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reserva reserva)
        {
            ModelState.Remove("horaSistemaLLegada");
            ModelState.Remove("horaSistemaSalida");
            ModelState.Remove("Costo");
            ModelState.Remove("ReservaEstadoId");

            if (ModelState.IsValid)
            {
                reserva.Costo = (decimal) db.Servicio.Find(reserva.ServicioId).Costo;
                db.Reserva.Add(reserva);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReservaEstadoId = new SelectList(db.ReservaEstado, "ReservaEstadoId", "Descripcion", reserva.ReservaEstadoId);
            ViewBag.ServicioId = new SelectList(db.Servicio, "ServicioId", "Descripcion", reserva.ServicioId);
            ViewBag.VehiculoId = new SelectList(db.Vehiculo, "VehiculoId", "Placa", reserva.VehiculoId);
            return View(reserva);
        }

        // GET: Manager/Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reserva.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservaEstadoId = new SelectList(db.ReservaEstado, "ReservaEstadoId", "Descripcion", reserva.ReservaEstadoId);
            ViewBag.ServicioId = new SelectList(db.Servicio, "ServicioId", "Descripcion", reserva.ServicioId);
            ViewBag.VehiculoId = new SelectList(db.Vehiculo, "VehiculoId", "Placa", reserva.VehiculoId);
            return View(reserva);
        }

        // POST: Manager/Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservaId,VehiculoId,ServicioId,horaReservaLlegada,horaReservaSalida,horaSistemaLLegada,horaSistemaSalida,Costo,ReservaEstadoId")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservaEstadoId = new SelectList(db.ReservaEstado, "ReservaEstadoId", "Descripcion", reserva.ReservaEstadoId);
            ViewBag.ServicioId = new SelectList(db.Servicio, "ServicioId", "Descripcion", reserva.ServicioId);
            ViewBag.VehiculoId = new SelectList(db.Vehiculo, "VehiculoId", "Placa", reserva.VehiculoId);
            return View(reserva);
        }

        // GET: Manager/Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reserva.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Manager/Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reserva.Find(id);
            db.Reserva.Remove(reserva);
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

        [HttpPost]
        public ActionResult BuscarCliente(string clienteDNI)
        {
            if (db.Cliente.Any(m => m.DNI == clienteDNI)) {
                Cliente cliente = db.Cliente.FirstOrDefault(m => m.DNI == clienteDNI);
                ViewBag.ReservaEstadoId = new SelectList(db.ReservaEstado, "ReservaEstadoId", "Descripcion");
                ViewBag.ServicioId = new SelectList(db.Servicio, "ServicioId", "Descripcion");
                ViewBag.VehiculoId = new SelectList(db.Vehiculo.Where(m => m.ClienteId == cliente.ClienteId), "VehiculoId", "Placa");
                return View("Create");
            }

            var crearClienteViewModel = new ManagerViewModels()
            {
                DNI = clienteDNI
            };

            return RedirectToAction("CrearCliente", "Reservas", crearClienteViewModel);
        }

        [HttpGet]
        public ActionResult CrearCliente(string clienteDNI)
        {
            var crearClienteViewModel = new ManagerViewModels()
            {
                DNI = clienteDNI
            };
            return View(crearClienteViewModel);
        }

        [HttpPost]
        public ActionResult CrearCliente(ManagerViewModels crearClienteViewModel)
        {
            var cliente = new Cliente()
            {
                DNI = crearClienteViewModel.DNI,
                Nombre = crearClienteViewModel.Nombre,
                Apellido = crearClienteViewModel.Apellido
            };           

            if (ModelState.IsValid)
            {
                cliente = db.Cliente.Add(cliente);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Create");
            }

            var vehiculo = new Vehiculo()
            {
                ClienteId = cliente.ClienteId,
                Placa = crearClienteViewModel.Placa,
                TipoVehiculoId = 4
            };

            if (ModelState.IsValid)
            {
                db.Vehiculo.Add(vehiculo);
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            
            return View("Create");
        }

        public ActionResult CancelarReserva(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var reserva = db.Reserva.Find(id);
            if (reserva != null) reserva.ReservaEstadoId = EstadoReserva.Cancelado;
            db.SaveChanges();

            return RedirectToAction("Index", "Reservas");
        }

        public ActionResult TerminarReserva(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var reserva = db.Reserva.Find(id);
            if (reserva != null) reserva.ReservaEstadoId = EstadoReserva.Terminado;
            db.SaveChanges();


            //Boleta generation

            var boletaCabecera = new BoletaCabecera()
            {
                ReservaId = reserva.ReservaId,
                Fecha = DateTime.Today,
                ClienteId = (int)reserva.Vehiculo.ClienteId,
                Estado = EstadoBoleta.Generado,
                Subtotal = 0,
                Total = 0
            };
            
            var boletaDetalles = new List<BoletaDetalle>();

            var boletaCabeceraDeReserva = new BoletaDetalle()
            {
                ServicioId = reserva.ServicioId,
                Cantidad = 1,
                Total = (decimal) reserva.Servicio.Costo
            };
            boletaDetalles.Add(boletaCabeceraDeReserva);

            foreach (var reservaServicios in db.ReservaServicios.Where(m => m.ReservaId == reserva.ReservaId).ToList())
            {
                var boletaCabeceraDeServicios = new BoletaDetalle()
                {
                    ServicioId = reservaServicios.ServicioId,
                    Cantidad = (int) reservaServicios.Cantidad,
                    Total = reservaServicios.Costo
                };
                boletaDetalles.Add(boletaCabeceraDeServicios);
            }

            foreach (var boletaDetalle in boletaDetalles)
            {
                boletaCabecera.Subtotal += boletaDetalle.Total;
            }
            boletaCabecera.Total = boletaCabecera.Subtotal * (1  + IGV.Valor);

            db.BoletaCabecera.Add(boletaCabecera);
            db.SaveChanges();
            foreach (var boletaDetalle in boletaDetalles)
            {
                boletaDetalle.BoletaCabeceraId = boletaCabecera.BoletaCabeceraId;
                db.BoletaDetalle.Add(boletaDetalle);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Reservas");
        }
    }
}
