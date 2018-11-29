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
using SistemaParqueo.Models;

namespace SistemaParqueo.APIControllers
{
    public class ClientesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Clientes
        public List<Cliente> GetCliente()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var clientes = db.Cliente.Include(m => m.Vehiculo).ToList();
            return clientes;
        }

        // GET: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult GetCliente(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var cliente = db.Cliente.Include(m => m.Vehiculo).FirstOrDefault(n => n.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [ResponseType(typeof(Cliente))]
        public IHttpActionResult GetCliente(string UID)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var  cliente = db.Cliente.Include(m => m.Vehiculo).FirstOrDefault(n => n.UID == UID);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCliente(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult PostCliente(Cliente cliente)
        {
            ModelState.Remove("ClienteId");
            ModelState.Remove("PersonaEstadoId");
            ModelState.Remove("ClienteEstado");
            ModelState.Remove("Vehiculo");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cliente.Add(cliente);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cliente.ClienteId }, cliente);
        }

        // DELETE: api/Clientes/5
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult DeleteCliente(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Cliente.Remove(cliente);
            db.SaveChanges();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Cliente.Count(e => e.ClienteId == id) > 0;
        }
    }
}