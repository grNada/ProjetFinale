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
    public class EntrepriseController : ApiController
    {
        Traittement trait = new Traittement();
        private FREELANCEEntities1 db = new FREELANCEEntities1();
        

        [System.Web.Http.Route("api/Entreprise")]
        public IHttpActionResult GetE()
        {
            List<ProjetFinale.Entreprise> list = db.Entreprise.ToList();
            return Ok(list);

        }
        [System.Web.Http.Route("api/Entreprise/{id:int}")]
        public IHttpActionResult GetEntreprise(int id)
        {
            ProjetFinale.Entreprise E = db.Entreprise.Find(id);
            if (E == null)
            {
                return NotFound();
            }

            return Ok(E);
        }


        // PUT: api/Postuler/5
  

        // GET: api/Entreprise
        /*public IQueryable<Entreprise> GetEntreprise()
        {
            return db.Entreprise;
        }

        // GET: api/Entreprise/5
        [ResponseType(typeof(Entreprise))]
        public IHttpActionResult GetEntreprise(int id)
        {
            Entreprise entreprise = db.Entreprise.Find(id);
            if (entreprise == null)
            {
                return NotFound();
            }

            return Ok(entreprise);
        }
        */
        // PUT: api/Entreprise/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEntreprise(int id, Entreprise entreprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != entreprise.id_entreprise)
            {
                return BadRequest();
            }

            db.Entry(entreprise).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntrepriseExists(id))
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

        // POST: api/Entreprise
        [ResponseType(typeof(Entreprise))]
        public IHttpActionResult PostEntreprise(Entreprise entreprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entreprise.Add(entreprise);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EntrepriseExists(entreprise.id_entreprise))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = entreprise.id_entreprise }, entreprise);
        }

        // DELETE: api/Entreprise/5
        [ResponseType(typeof(Entreprise))]
        public IHttpActionResult DeleteEntreprise(int id)
        {
            Entreprise entreprise = db.Entreprise.Find(id);
            if (entreprise == null)
            {
                return NotFound();
            }

            db.Entreprise.Remove(entreprise);
            db.SaveChanges();

            return Ok(entreprise);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EntrepriseExists(int id)
        {
            return db.Entreprise.Count(e => e.id_entreprise == id) > 0;
        }
    }
}