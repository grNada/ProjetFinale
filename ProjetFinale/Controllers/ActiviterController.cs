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
using System.Web.Http.Results;
using System.Web.Mvc;
using ProjetFinale;
using ProjetFinale.Models;

namespace ProjetFinale.Controllers
{
    public class ActiviterController : ApiController
    {
        Traittement trait = new Traittement();
        private FREELANCEEntities1 db = new FREELANCEEntities1();
        [System.Web.Http.Route("api/Activiter")]
        public IHttpActionResult GetACT()
        {
            List<ProjetFinale.Activiter> list = db.Activiter.ToList();
            return Ok(list);

        }
        [System.Web.Http.Route("api/Activiter/{id:int}")]
        public IHttpActionResult GetActiviter(int id)
        {
            ProjetFinale.Activiter ACT = db.Activiter.Find(id);
            if (ACT == null)
            {
                return NotFound();
            }

            return Ok(ACT);
        }

    
        // PUT: api/Activiter/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActiviter(int id, Activiter activiter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activiter.id_activiter)
            {
                return BadRequest();
            }

            db.Entry(activiter).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiviterExists(id))
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

        // POST: api/Activiter
        [ResponseType(typeof(Activiter))]
        public IHttpActionResult PostActiviter(Activiter activiter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Activiter.Add(activiter);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ActiviterExists(activiter.id_activiter))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = activiter.id_activiter }, activiter);
        }

        // DELETE: api/Activiter/5
        [ResponseType(typeof(Activiter))]
        public IHttpActionResult DeleteActiviter(int id)
        {
            Activiter activiter = db.Activiter.Find(id);
            if (activiter == null)
            {
                return NotFound();
            }

            db.Activiter.Remove(activiter);
            db.SaveChanges();

            return Ok(activiter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActiviterExists(int id)
        {
            return db.Activiter.Count(e => e.id_activiter == id) > 0;
        }
    }
}