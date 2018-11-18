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
    public class CocherasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Cocheras
        public List<Cochera> GetCochera()
        {
            db.Configuration.LazyLoadingEnabled = false;
            List<Cochera> cocheras = db.Cochera.Include(m => m.Servicio).ToList();
            
            return cocheras;
        }

        // GET: api/Cocheras/5
        [ResponseType(typeof(Cochera))]
        public IHttpActionResult GetCochera(int id)
        {
            Cochera cochera = db.Cochera.Find(id);

            if (cochera == null)
            {
                return NotFound();
            }

            return Ok(cochera);
        }

        // PUT: api/Cocheras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCochera(int id, Cochera cochera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cochera.CocheraId)
            {
                return BadRequest();
            }

            db.Entry(cochera).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CocheraExists(id))
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

        // POST: api/Cocheras
        [ResponseType(typeof(Cochera))]
        public IHttpActionResult PostCochera(Cochera cochera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cochera.Add(cochera);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cochera.CocheraId }, cochera);
        }

        // DELETE: api/Cocheras/5
        [ResponseType(typeof(Cochera))]
        public IHttpActionResult DeleteCochera(int id)
        {
            Cochera cochera = db.Cochera.Find(id);
            if (cochera == null)
            {
                return NotFound();
            }

            db.Cochera.Remove(cochera);
            db.SaveChanges();

            return Ok(cochera);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CocheraExists(int id)
        {
            return db.Cochera.Count(e => e.CocheraId == id) > 0;
        }
    }
}