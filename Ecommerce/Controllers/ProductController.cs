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
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class ProductController : ApiController
    {
        private EcommerceEntitiy db = new EcommerceEntitiy();

        // GET: api/Product
        public IQueryable<ProductTable> GetProductTables()
        {
            return db.ProductTables;
        }

        // GET: api/Product/5
        [ResponseType(typeof(ProductTable))]
        public IHttpActionResult GetProductTable(string id)
        {
            ProductTable productTable = db.ProductTables.Find(id);
            if (productTable == null)
            {
                return NotFound();
            }

            return Ok(productTable);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductTable(string id, ProductTable productTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productTable.ProductID)
            {
                return BadRequest();
            }

            db.Entry(productTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTableExists(id))
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

        // POST: api/Product
        [ResponseType(typeof(ProductTable))]
        public IHttpActionResult PostProductTable(ProductTable productTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductTables.Add(productTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductTableExists(productTable.ProductID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = productTable.ProductID }, productTable);
        }

        // DELETE: api/Product/5
        [ResponseType(typeof(ProductTable))]
        public IHttpActionResult DeleteProductTable(string id)
        {
            ProductTable productTable = db.ProductTables.Find(id);
            if (productTable == null)
            {
                return NotFound();
            }

            db.ProductTables.Remove(productTable);
            db.SaveChanges();

            return Ok(productTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductTableExists(string id)
        {
            return db.ProductTables.Count(e => e.ProductID == id) > 0;
        }
    }
}