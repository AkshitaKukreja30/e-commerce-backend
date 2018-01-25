using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using cygshopnew.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cygshopnew.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class categoriesController : ApiController
    {
        private cygshopnewEntities db = new cygshopnewEntities();

        // GET: api/categories/Getcategories
        public JArray Getcategories()
        {   
            List<category> categories = db.categories.ToList();
            JArray array = new JArray();
            foreach (var category in categories)
            {
                JObject obj = new JObject();
                obj["id"] = category.id;
                obj["name"] = category.name;
                obj["countP"] = category.products.Count;
                array.Add(obj);
            }

            return array;

        }

        // GET: api/categories/5
        [ResponseType(typeof(category))]
        public IHttpActionResult Getcategory(int id)
        {
            category category = db.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcategory(int id, category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoryExists(id))
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

        // POST: api/categories/Postcategory
        [ResponseType(typeof(category))]
        public IHttpActionResult Postcategory(category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.categories.Add(category);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (categoryExists(category.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = category.id }, category);
        }

        // DELETE: api/categories/5
        [ResponseType(typeof(category))]
        public IHttpActionResult Deletecategory(int id)
        {
            category category = db.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool categoryExists(int id)
        {
            return db.categories.Count(e => e.id == id) > 0;
        }
    }
}