using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;
using API.Publisher;
using System.IO;
using API.Dtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products1Controller : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly MessagePublisher _messagePublisher;

        public Products1Controller(StoreContext context, MessagePublisher messagePublisher)
        {
            _context = context;
            _messagePublisher = messagePublisher;
        }

        // GET: api/Products1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products1/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost("publish/item")]
        public async Task<ActionResult> PublishItem([FromBody] ItemToReturnDto request)
        {
            var itemCreated = new ItemForServBus {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                OldPrice = request.OldPrice,
                NewPrice = request.NewPrice,
                Discount = request.Discount,
                RatingsCount = request.RatingsCount,
                RatingsValue = request.RatingsValue,
                availibilityCount = request.availibilityCount,
                cartCount = request.cartCount,
                Weight = request.Weight,
                CategoryId = request.CategoryId,
                BrandName = request.BrandName
            };

            await _messagePublisher.Publish(itemCreated);

            return Ok();
        }
        [HttpPost("publish/text")]
        public async Task<ActionResult> PublishText()
        {
            using var reader = new StreamReader(Request.Body);
            var BodyAsText = await reader.ReadToEndAsync();
            await _messagePublisher.Publish(BodyAsText);

            return Ok();
        }

        // POST: api/Products1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
