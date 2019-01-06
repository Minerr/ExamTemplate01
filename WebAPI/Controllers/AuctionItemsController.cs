using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebApplication.Models;

namespace WebAPI.Controllers
{
	[Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionItemsController : ControllerBase
    {
        private readonly MainDbContext _context;

        public AuctionItemsController(MainDbContext context)
        {
            _context = context;
        }

		/// <summary>
		/// Returns all items
		/// </summary>
		/// <returns>All auction items from the database as JSON list</returns>
        // GET: api/AuctionItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionItem>>> GetAuctionItems()
        {
            return await _context.AuctionItems.ToListAsync();
        }

		/// <summary>
		/// Get a specific item
		/// </summary>
		/// <param name="id">The item number</param>
		/// <returns>Returns status NotFound, if the item is null</returns>
        // GET: api/AuctionItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionItem>> GetAuctionItem(int id)
        {
            var auctionItem = await _context.AuctionItems.FindAsync(id);

            if (auctionItem == null)
            {
                return NotFound();
            }

            return auctionItem;
        }

		/// <summary>
		/// Updates an auction item in the database
		/// </summary>
		/// <param name="id">The item number of the item in the database</param>
		/// <param name="auctionItem">The item with updated values to update the database with</param>
		/// <returns>
		/// Returns BadRequest if the id and object id does not match.
		/// Returns NotFound if the id does not exists. 
		/// Returns NoContent if succesful
		/// </returns>
        // PUT: api/AuctionItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuctionItem(int id, AuctionItem auctionItem)
        {
            if (id != auctionItem.ItemNumber)
            {
                return BadRequest();
            }

            _context.Entry(auctionItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionItemExists(id))
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

		/// <summary>
		/// Save an auction item to the database
		/// </summary>
		/// <param name="auctionItem">Item to store in the database</param>
		/// <returns>Returns 201 Created if succelful, and returns the item with the generated id</returns>
        // POST: api/AuctionItems
        [HttpPost]
        public async Task<ActionResult<AuctionItem>> PostAuctionItem(AuctionItem auctionItem)
        {
            _context.AuctionItems.Add(auctionItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuctionItem", new { id = auctionItem.ItemNumber }, auctionItem);
        }

		/// <summary>
		/// Delete an item in the database
		/// </summary>
		/// <param name="id">Item number of the item to delete.</param>
		/// <returns>Returns NotFound status if the id does not exists. Returns the deleted item.</returns>
        // DELETE: api/AuctionItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuctionItem>> DeleteAuctionItem(int id)
        {
            var auctionItem = await _context.AuctionItems.FindAsync(id);
            if (auctionItem == null)
            {
                return NotFound();
            }

            _context.AuctionItems.Remove(auctionItem);
            await _context.SaveChangesAsync();

            return auctionItem;
        }

        private bool AuctionItemExists(int id)
        {
            return _context.AuctionItems.Any(e => e.ItemNumber == id);
        }
    }
}
