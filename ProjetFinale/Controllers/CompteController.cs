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
    public class CompteController : ApiController
    {
        Traittement trait = new Traittement();
        private FREELANCEEntities1 db = new FREELANCEEntities1();
        public JsonResult Get()
        {
            List<Compte> list = trait.ListeComptes();
            return new JsonResult()
            {
                Data = list,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        // GET: api/Compte
        public IQueryable<Compte> GetCompte()
        {
            return db.Compte;
        }

        // GET: api/Compte/5
        [ResponseType(typeof(Compte))]
        public IHttpActionResult GetCompte(int id)
        {
            Compte compte = db.Compte.Find(id);
            if (compte == null)
            {
                return NotFound();
            }

            return Ok(compte);
        }

        // PUT: api/Compte/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCompte(int id, Compte compte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != compte.id_Compte)
            {
                return BadRequest();
            }

            db.Entry(compte).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompteExists(id))
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

        // POST: api/Compte
        [ResponseType(typeof(Compte))]
        public IHttpActionResult PostCompte(Compte compte)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Compte.Add(compte);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CompteExists(compte.id_Compte))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = compte.id_Compte }, compte);
        }

        // DELETE: api/Compte/5
        [ResponseType(typeof(Compte))]
        public IHttpActionResult DeleteCompte(int id)
        {
            Compte compte = db.Compte.Find(id);
            if (compte == null)
            {
                return NotFound();
            }

            db.Compte.Remove(compte);
            db.SaveChanges();

            return Ok(compte);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompteExists(int id)
        {
            return db.Compte.Count(e => e.id_Compte == id) > 0;
        }
    }
}