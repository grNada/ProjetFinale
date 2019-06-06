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
    public class CompetenceController : ApiController
    {
        Traittement trait = new Traittement();
        private FREELANCEEntities1 db = new FREELANCEEntities1();
        [System.Web.Http.Route("api/Competence")]
        public IHttpActionResult GetCOM()
        {
            List<ProjetFinale.Competence> list = db.Competence.ToList();
            return Ok(list);

        }
        [System.Web.Http.Route("api/Competence/{id:int}")]
        public IHttpActionResult GetCompetence(int id)
        {
            ProjetFinale.Competence COM = db.Competence.Find(id);
            if (COM == null)
            {
                return NotFound();
            }

            return Ok(COM);
        }

       
        // PUT: api/Competence/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompetence(int id, Competence competence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != competence.id_competence)
            {
                return BadRequest();
            }

            db.Entry(competence).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetenceExists(id))
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

        // POST: api/Competence
       
        [ResponseType(typeof(Competence))]
        public IHttpActionResult PostCompetence(Competence competence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Competence.Add(competence);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CompetenceExists(competence.id_competence))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = competence.id_competence }, competence);
        }

        // DELETE: api/Competence/5
        [ResponseType(typeof(Competence))]
        public IHttpActionResult DeleteCompetence(int id)
        {
            Competence competence = db.Competence.Find(id);
            if (competence == null)
            {
                return NotFound();
            }

            db.Competence.Remove(competence);
            db.SaveChanges();

            return Ok(competence);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompetenceExists(int id)
        {
            return db.Competence.Count(e => e.id_competence == id) > 0;
        }
        private bool Existscompetence(string nom)
        {
            return db.Competence.Count(e => e.nom == nom) > 0;
        }
    }
}