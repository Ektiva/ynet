using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Items1Controller : ControllerBase
    {
        private readonly StoreContext _context;

        public Items1Controller(StoreContext context)
        {
            _context = context;
        }

        // GET: api/Items1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items1/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchItem(int id, Item item)
        {
            var itemTochange = await _context.Items.FirstOrDefaultAsync(x=> x.Id == id);
            if (id != item.Id)
            {
                return BadRequest();
            }
            //itemTochange.CategoryIdNode = item.CategoryIdNode;
            //item.Name = itemTochange.Name;
            //item.Description = itemTochange.Description;
            //item.OldPrice = itemTochange.OldPrice;
            //item.NewPrice = itemTochange.NewPrice;
            //item.Discount = itemTochange.Discount;
            //item.RatingsCount = itemTochange.RatingsCount;
            //item.RatingsValue = itemTochange.RatingsValue;
            //item.availibilityCount = itemTochange.availibilityCount;
            //item.cartCount = itemTochange.cartCount;
            //item.Weight = itemTochange.Weight;
            //item.CategoryId = itemTochange.CategoryId;
            //item.ProductBrandId = itemTochange.ProductBrandId;

            // _context.Entry(item).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Items1/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
