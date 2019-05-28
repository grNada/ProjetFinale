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
    public class MessageController : ApiController
    {
        Traittement trait = new Traittement();
        private FREELANCEEntities1 db = new FREELANCEEntities1();
        public JsonResult Get()
        {
            List<Message> list = trait.ListeMessages();
            return new JsonResult()
            {
                Data = list,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        // GET: api/Message
        public IQueryable<Message> GetMessage()
        {
            return db.Message;
        }

        // GET: api/Message/5
        [ResponseType(typeof(Message))]
        public IHttpActionResult GetMessage(int id)
        {
            Message message = db.Message.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
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