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
using cygshopnew.Models;

namespace cygshopnew.Controllers
{

    public class cartsController : ApiController
    {
        private cygshopnewEntities db = new cygshopnewEntities();

        // GET: api/carts
        public IQueryable<cart> Getcarts()
        {
            return db.carts;
        }

        // GET: api/carts/5
        [ResponseType(typeof(cart))]
        public IHttpActionResult Getcart(int id)
        {
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // PUT: api/carts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcart(int id, cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cart.id)
            {
                return BadRequest();
            }

            db.Entry(cart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!cartExists(id))
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

        // POST: api/carts
        [ResponseType(typeof(cart))]
        public IHttpActionResult Postcart(cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.carts.Add(cart);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (cartExists(cart.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cart.id }, cart);
        }

        // DELETE: api/carts/5
        [ResponseType(typeof(cart))]
        public IHttpActionResult Deletecart(int id)
        {
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return NotFound();
            }

            db.carts.Remove(cart);
            db.SaveChanges();

            return Ok(cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool cartExists(int id)
        {
            return db.carts.Count(e => e.id == id) > 0;
        }
    }
}