using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using ProjetFinale;
using ProjetFinale.Models;

namespace ProjetFinale.Controllers
{
    public class UtilisateurController : ApiController
    {
        Traittement trait = new Traittement();
        private FREELANCEEntities1 db = new FREELANCEEntities1();
        [System.Web.Http.Route("api/Utilisateur")]
        // GET: api/Utilisateur
        public IHttpActionResult Get()
         {
             List<Utilisateur> list = db.Utilisateur.ToList();
            return Ok(list);



         }

        /*[System.Web.Http.Route("api/Utilisateur")]
        /*public IQueryable<Utilisateur> GetUtilisateur()
        {
            return db.Utilisateur.Include(C => C.Condidat);

        }*/

        // GET: api/Utilisateur/5
        [ResponseType(typeof(Utilisateur))]
        [System.Web.Http.Route("api/Utilisateur/{id:int}")]
        public async Task<IHttpActionResult> GetUtilisateurAsync(int id)
        {
            Utilisateur utilisateur = await db.Utilisateur.Include(U => U.Condidat).SingleOrDefaultAsync(U => U.id_user == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return Ok(utilisateur);
        }
        
        // PUT: api/Utilisateur/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != utilisateur.id_user)
            {
                return BadRequest();
            }

            db.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
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

        // POST: api/Utilisateur
        [ResponseType(typeof(Utilisateur))]
        public async Task<IHttpActionResult> PostUtilisateur(Utilisateur utilisateur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Utilisateur.Add(utilisateur);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = utilisateur.id_user }, utilisateur);
        }

        // DELETE: api/Utilisateur/5
        [ResponseType(typeof(Utilisateur))]
        public async Task<IHttpActionResult> DeleteUtilisateur(int id)
        {
            Utilisateur utilisateur = await db.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            db.Utilisateur.Remove(utilisateur);
            await db.SaveChangesAsync();

            return Ok(utilisateur);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UtilisateurExists(int id)
        {
            return db.Utilisateur.Count(e => e.id_user == id) > 0;
        }
    }
}
 