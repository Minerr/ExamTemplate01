using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Context;
using WebApplication.Models;

namespace WebAPI.Controllers
{
	[Produces("application/json")]
	[Route("api/[controller]")]
	[ApiController]
	public class AuctionHouseController : ControllerBase
	{
		private readonly MainDbContext _context;

		public AuctionHouseController(MainDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// A method that returns all information for all auction items.
		/// </summary>
		/// <returns>Returns a list of auction items</returns>
		// GET: api/AuctionHouse
		[HttpGet]
		public List<AuctionItem> GetAllAuctionItems()
		{
			return _context.AuctionItems.ToList();
		}

		/// <summary>
		/// Returns all information for one auction item, if the item does not exist, then returns null.
		/// </summary>
		/// <param name="itemNumber">The id of the auction item</param>
		/// <returns>Returns one auction item, or null if the id does not exist</returns>
		// GET: api/AuctionHouse/5
		[HttpGet("{itemNumber}")]
		public AuctionItem GetAuctionItem(int itemNumber)
		{
			return _context.AuctionItems.Find(itemNumber);
		}

		/// <summary>
		/// Provide a bid on an auction item
		/// </summary>
		/// <param name="bid">Bid object with info</param>
		/// <returns>
		/// Returns "Varen findes ikke" if the item does not exist.
		/// Returns "Bud for lavt" if the bid is too low.
		/// Returns "OK" if succesful
		/// </returns>
		[HttpPost]
		public string ProvideBid(Bid bid)
		{
			AuctionItem item = _context.AuctionItems.Find(bid.ItemNumber);
			if(item == null)
			{
				return "Varen findes ikke";
			}

			if(item.BidPrice >= bid.Price)
			{
				return "Bud for lavt";
			}

			item.BidPrice = bid.Price;
			item.BidCustomName = bid.CustomName;
			item.BidCustomPhone = bid.CustomPhone;
			item.BidTimestamp = DateTime.Now;

			_context.SaveChanges();

			return "OK";
		}
	}
}