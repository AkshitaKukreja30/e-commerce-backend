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
    public class user_cartController : ApiController
    {
        private cygshopnewEntities db = new cygshopnewEntities();

        // GET: api/user_cart
        public IQueryable<user_cart> Getuser_cart()
        {
            return db.user_cart;
        }

        // GET: api/user_cart/5
        [ResponseType(typeof(user_cart))]
        public IHttpActionResult Getuser_cart(int id)
        {
            user_cart user_cart = db.user_cart.Find(id);
            if (user_cart == null)
            {
                return NotFound();
            }

            return Ok(user_cart);
        }

        // PUT: api/user_cart/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuser_cart(int id, user_cart user_cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user_cart.id)
            {
                return BadRequest();
            }

            db.Entry(user_cart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!user_cartExists(id))
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

        // POST: api/user_cart
        [ResponseType(typeof(user_cart))]
        public IHttpActionResult Postuser_cart(user_cart user_cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.user_cart.Add(user_cart);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (user_cartExists(user_cart.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user_cart.id }, user_cart);
        }

        // DELETE: api/user_cart/5
        [ResponseType(typeof(user_cart))]
        public IHttpActionResult Deleteuser_cart(int id)
        {
            user_cart user_cart = db.user_cart.Find(id);
            if (user_cart == null)
            {
                return NotFound();
            }

            db.user_cart.Remove(user_cart);
            db.SaveChanges();

            return Ok(user_cart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool user_cartExists(int id)
        {
            return db.user_cart.Count(e => e.id == id) > 0;
        }
    }
}