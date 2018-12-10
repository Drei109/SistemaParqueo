using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SistemaParqueo.DTO;
using SistemaParqueo.Models;

namespace SistemaParqueo.APIControllers
{
    public class ReservasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Reservas
        public List<Reserva> GetReserva()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Reserva.ToList();
        }

        // GET: api/Reservas/5
        [ResponseType(typeof(Reserva))]
        public IHttpActionResult GetReserva(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Reserva reserva = db.Reserva.Find(id);
            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);
        }

        // PUT: api/Reservas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReserva(int id, Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reserva.ReservaId)
            {
                return BadRequest();
            }

            db.Entry(reserva).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Reservas
        //[ResponseType(typeof(Reserva))]
        //public IHttpActionResult PostReserva(Reserva reserva)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Reserva.Add(reserva);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = reserva.ReservaId }, reserva);
        //}

        // POST: api/Reservas
        [ResponseType(typeof(ReservaDTO))]
        public IHttpActionResult PostReserva(ReservaDTO reservaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reserva = new Reserva()
            {
                Costo = reservaDTO.costo,
                horaReservaLlegada = reservaDTO.hora_reserva_llegada,
                horaReservaSalida = reservaDTO.hora_reserva_salida,
                horaSistemaLLegada = reservaDTO.hora_sistema_llegada,
                horaSistemaSalida = reservaDTO.hora_sistema_salida,
                ReservaId = reservaDTO.id,
                ReservaEstadoId = reservaDTO.reserva_estado_id,
                ServicioId = reservaDTO.servicio_id,
                VehiculoId = reservaDTO.vehiculo_id
            };

            db.Reserva.Add(reserva);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reserva.ReservaId }, reserva);
        }

        // DELETE: api/Reservas/5
        [ResponseType(typeof(Reserva))]
        public IHttpActionResult DeleteReserva(int id)
        {
            Reserva reserva = db.Reserva.Find(id);
            if (reserva == null)
            {
                return NotFound();
            }

            db.Reserva.Remove(reserva);
            db.SaveChanges();

            return Ok(reserva);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReservaExists(int id)
        {
            return db.Reserva.Count(e => e.ReservaId == id) > 0;
        }
    }
}