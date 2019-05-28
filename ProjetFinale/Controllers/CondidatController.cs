﻿using System;
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
    public class CondidatController : ApiController
    {
        Traittement trait = new Traittement();
        private FREELANCEEntities1 db = new FREELANCEEntities1();
        [System.Web.Http.Route("api/Condidat")]
        public IHttpActionResult Get()
        {
            List<Condidat> list = db.Condidat.ToList();
            return Ok(list);

        }
        [System.Web.Http.Route("api/Condidat/email")]
        public IHttpActionResult getEmails()
        {
            List<string> listEmail = db.Condidat.ToList().Select(x => x.email).ToList();

            return Ok(listEmail);

        }

        /*// GET: api/Condidat
        public IQueryable<Condidat> GetCondidat()
        {
            return db.Condidat;
        }
        */

        // GET: api/Condidat/5
        [ResponseType(typeof(Condidat))]
        [System.Web.Http.Route("api/Condidat/{id:int}")]
        public IHttpActionResult GetCondidat(int id)
        {
            Condidat condidat = db.Condidat.Find(id);
            if (condidat == null)
            {
                return NotFound();
            }

            return Ok(condidat);
        }
        
        // PUT: api/Condidat/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCondidat(int id, Condidat condidat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != condidat.id_personne)
            {
                return BadRequest();
            }

            db.Entry(condidat).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CondidatExists(id))
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
       
        // POST: api/Condidat
        [ResponseType(typeof(Condidat))]
        public IHttpActionResult PostCondidat(Condidat condidat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Condidat.Add(condidat);
          db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = condidat.id_personne }, condidat);
        }

        // DELETE: api/Condidat/5
        [ResponseType(typeof(Condidat))]
        public IHttpActionResult DeleteCondidat(int id)
        {
            Condidat condidat = db.Condidat.Find(id);
            if (condidat == null)
            {
                return NotFound();
            }

            db.Condidat.Remove(condidat);
            db.SaveChanges();

            return Ok(condidat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CondidatExists(int id)
        {
            return db.Condidat.Count(e => e.id_personne == id) > 0;
        }
    }
}