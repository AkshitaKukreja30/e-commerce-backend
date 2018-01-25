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
    public class registerationsController : ApiController
    {
        private cygshopnewEntities db = new cygshopnewEntities();

        // GET: api/registerations
        public IQueryable<registeration> Getregisterations()
        {
            return db.registerations;
        }

        // GET: api/registerations/Getregisteration
        [ResponseType(typeof(registeration))]
        public IHttpActionResult Getregisteration(int id)
        {
            registeration registeration = db.registerations.Find(id);
            if (registeration == null)
            {
                return NotFound();
            }

            return Ok(registeration);
        }

        [HttpGet]
        //api/registerations/Getregisteration2
        public IHttpActionResult Getregisteration2()
        {
            String detailsforlogin = Request.RequestUri.Query;
            var separator1 = detailsforlogin.IndexOf('=');
            separator1++;

            var s = detailsforlogin.IndexOf('&');
            String emailforlogin = detailsforlogin.Substring(separator1, s - separator1);
            var l = detailsforlogin.Length;
            var separator2 = detailsforlogin.LastIndexOf('=');
            separator2++;
            String tset = detailsforlogin.Substring(separator2);

            registeration ifuserexists = db.registerations.Where(a => a.email.Equals(emailforlogin)).FirstOrDefault();
            if (ifuserexists == null)
            {
                return NotFound();
            }

            else if (ifuserexists != null && tset.Equals(ifuserexists.password))
            {
                return Ok();

            }

            else
                return NotFound();




        }









        // PUT: api/registerations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putregisteration(int id, registeration registeration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registeration.id)
            {
                return BadRequest();
            }

            db.Entry(registeration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!registerationExists(id))
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

        // POST: api/registerations
        [ResponseType(typeof(registeration))]
        public IHttpActionResult Postregisteration(registeration registeration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.registerations.Add(registeration);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (registerationExists(registeration.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = registeration.id }, registeration);
        }

        // DELETE: api/registerations/5
        [ResponseType(typeof(registeration))]
        public IHttpActionResult Deleteregisteration(int id)
        {
            registeration registeration = db.registerations.Find(id);
            if (registeration == null)
            {
                return NotFound();
            }

            db.registerations.Remove(registeration);
            db.SaveChanges();

            return Ok(registeration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool registerationExists(int id)
        {
            return db.registerations.Count(e => e.id == id) > 0;
        }
    }
}