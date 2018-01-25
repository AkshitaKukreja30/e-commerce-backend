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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cygshopnew.Controllers
{
    public class productsController : ApiController
    {
        private cygshopnewEntities db = new cygshopnewEntities();

        // GET: api/products
        public IQueryable<product> Getproducts()
        {
            return db.products;
        }

        // GET: api/products/5
        [ResponseType(typeof(product))]
        public IHttpActionResult Getproduct(int id)
        {
            product product = db.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public JArray GetOurProduct(int cid)
        {
            List<product> products = db.products.ToList();
            JArray array = new JArray();
            
            foreach (var product in products)
            {
                
                JObject obj = new JObject();
                obj["id"] = product.id;
                obj["name"] = product.name;
                obj["description"] = product.Description;
                obj["unitprice"] = product.unit_price;
                obj["categoryid"] = product.category_id;
                obj["quantity"] = product.quantity;

                if (cid == product.category_id)
                {// obj["countP"] = category.products.Count;
                    array.Add(obj);
                }
            }

            return array;

        }

        
        public JArray FetchQuantity(int[] pid)
        {
            List<product> products = db.products.ToList();
            JArray array = new JArray();

            foreach (var product in products)
            {
                          
                JObject obj = new JObject();
                for (var i = 0; i < pid.Length; i++)
                {
                    obj["id"] = product.id;
                    obj["quantity"] = product.quantity;


                    if (product.id == pid[i])
                    {
                        
                        array.Add(obj);
                    }

                     }
            }

            return array;

        }


























        // PUT: api/products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproduct(int id, product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(id))
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

        // POST: api/products/Postproduct
        [ResponseType(typeof(product))]
        public IHttpActionResult Postproduct(product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.products.Add(product);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                //if (productExists(product.id))
                //{
                //    return Conflict();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return CreatedAtRoute("DefaultApi", new { id = product.id }, product);
        }

        // DELETE: api/products/5
        [ResponseType(typeof(product))]
        public IHttpActionResult Deleteproduct(int id)
        {
            product product = db.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productExists(int id)
        {
            return db.products.Count(e => e.id == id) > 0;
        }
    }
}