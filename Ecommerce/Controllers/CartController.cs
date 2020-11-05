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
    public class CartController : ApiController
    {
        private EcommerceEntitiy db = new EcommerceEntitiy();

        // GET: api/Cart
        public IQueryable<CartTable> GetCartTables()
        {
            return db.CartTables;
        }

        // GET: api/Cart/5
        [ResponseType(typeof(CartTable))]
        public IHttpActionResult GetCartTable(int id)
        {
            CartTable cartTable = db.CartTables.Find(id);
            if (cartTable == null)
            {
                return NotFound();
            }

            return Ok(cartTable);
        }

        // PUT: api/Cart/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCartTable(int id, CartTable cartTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cartTable.CartID)
            {
                return BadRequest();
            }

            db.Entry(cartTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartTableExists(id))
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

        // POST: api/Cart
        [ResponseType(typeof(CartTable))]
        public IHttpActionResult PostCartTable(CartTable cartTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            db.CartTables.Add(cartTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CartTableExists(cartTable.CartID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cartTable.CartID }, cartTable);


        }

        // DELETE: api/Cart/5
        [ResponseType(typeof(CartTable))]
        public IHttpActionResult DeleteCartTable(int id)
        {
            CartTable cartTable = db.CartTables.Find(id);
            if (cartTable == null)
            {
                return NotFound();
            }

            db.CartTables.Remove(cartTable);
            db.SaveChanges();

            return Ok(cartTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartTableExists(int id)
        {
            return db.CartTables.Count(e => e.CartID == id) > 0;
        }
    }
}