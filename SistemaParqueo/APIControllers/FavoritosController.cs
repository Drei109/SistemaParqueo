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
    public class FavoritosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Favoritos
        public IQueryable<Favoritos> GetFavoritos()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.Favoritos;
        }

        [ResponseType(typeof(List<Favoritos>))]
        public List<Favoritos> GetFavoritos(int? ClienteId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            var favoritos = db.Favoritos.Where(m => m.ClienteId == ClienteId).ToList();
            return favoritos;
        }

        // GET: api/Favoritos/5
        [ResponseType(typeof(Favoritos))]
        public IHttpActionResult GetFavoritos(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            Favoritos favoritos = db.Favoritos.Find(id);
            if (favoritos == null)
            {
                return NotFound();
            }

            return Ok(favoritos);
        }

        // PUT: api/Favoritos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFavoritos(int id, Favoritos favoritos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != favoritos.ClienteId)
            {
                return BadRequest();
            }

            db.Entry(favoritos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoritosExists(id))
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

        // POST: api/Favoritos
        [ResponseType(typeof(Favoritos))]
        public IHttpActionResult PostFavoritos(Favoritos favoritos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Favoritos.Add(favoritos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FavoritosExists(favoritos.ClienteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = favoritos.ClienteId }, favoritos);
        }

        // DELETE: api/Favoritos/5
        [ResponseType(typeof(Favoritos))]
        public IHttpActionResult DeleteFavoritos(int id)
        {
            Favoritos favoritos = db.Favoritos.Find(id);
            if (favoritos == null)
            {
                return NotFound();
            }

            db.Favoritos.Remove(favoritos);
            db.SaveChanges();

            return Ok(favoritos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FavoritosExists(int id)
        {
            return db.Favoritos.Count(e => e.ClienteId == id) > 0;
        }
    }
}