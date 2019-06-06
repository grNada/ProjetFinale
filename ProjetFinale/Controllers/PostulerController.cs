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
using System.Web.Mvc;

using ProjetFinale;
using ProjetFinale.Models;

namespace ProjetFinale.Controllers
{
    public class PostulerController : ApiController
    {
        Traittement trait = new Traittement();
        private FREELANCEEntities1 db = new FREELANCEEntities1();
      
        [System.Web.Http.Route("api/Postuler")]
        public IHttpActionResult Get()
        {
            List<ProjetFinale.Postuler> list = db.Postuler.ToList();
            return Ok(list);

        }
        [System.Web.Http.Route("api/Postuler/{id:int}")]
        public IHttpActionResult GetPostuler(int id)
        {
            ProjetFinale.Postuler postuler = db.Postuler.Find(id);
            if (postuler == null)
            {
                return NotFound();
            }

            return Ok(postuler);
        }

        
        // PUT: api/Postuler/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPostuler(int id, Postuler postuler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postuler.id_post)
            {
                return BadRequest();
            }

            db.Entry(postuler).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostulerExists(id))
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

        // POST: api/Postuler
        [ResponseType(typeof(Postuler))]
        public IHttpActionResult PostPostuler(Postuler postuler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Postuler.Add(postuler);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PostulerExists(postuler.id_post))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = postuler.id_post }, postuler);
        }

        // DELETE: api/Postuler/5
        [ResponseType(typeof(Postuler))]
        public IHttpActionResult DeletePostuler(int id)
        {
            Postuler postuler = db.Postuler.Find(id);
            if (postuler == null)
            {
                return NotFound();
            }

            db.Postuler.Remove(postuler);
            db.SaveChanges();

            return Ok(postuler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostulerExists(int id)
        {
            return db.Postuler.Count(e => e.id_post == id) > 0;
        }
    }
}