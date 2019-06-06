using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR.Messaging;
using ProjetFinale;
using ProjetFinale.Models;
using Twilio.TwiML.Messaging;

namespace ProjetFinale.Controllers
{
    public class MessageController : ApiController
    {
        Traittement trait = new Traittement();
        private FREELANCEEntities1 db = new FREELANCEEntities1();
        [System.Web.Http.Route("api/Message")]
        public IHttpActionResult GetM()
        {
            List<ProjetFinale.Message> list = db.Message.ToList();
            return Ok(list);

        }
        [System.Web.Http.Route("api/Message/{id:int}")]
        public IHttpActionResult GetMessage(int id)
        {
            ProjetFinale.Message COMP = db.Message.Find(id);
            if (COMP == null)
            {
                return NotFound();
            }

            return Ok(COMP);
        }

        // PUT: api/Message/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.id_message)
            {
                return BadRequest();
            }

            db.Entry(message).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Message
        [ResponseType(typeof(Message))]
        public IHttpActionResult PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Message.Add(message);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (MessageExists(message.id_message))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = message.id_message }, message);
        }

        // DELETE: api/Message/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult DeleteMessage(int id)
        {
            Message message = db.Message.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            db.Message.Remove(message);
            db.SaveChanges();

            return Ok(message);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MessageExists(int id)
        {
            return db.Message.Count(e => e.id_message == id) > 0;
        }
    }
 
}