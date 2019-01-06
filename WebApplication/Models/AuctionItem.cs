using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
	public class AuctionItem
	{
		[Display(Name = "ID")]
		public int ItemNumber { get; set; }

		[Display(Name = "Description")]
		public string ItemDescription { get; set; }

		[Display(Name = "Rating price")]
		public int RatingPrice { get; set; }

		[Display(Name = "Bid price")]
		[Required]
		public int BidPrice { get; set; }

		[Display(Name = "Bidder name")]
		[StringLength(100, MinimumLength = 4, ErrorMessage = "Name must be between 4-100 characters")]
		[Required]
		public string BidCustomName { get; set; }

		[Display(Name = "Bidder phonenumber")]
		[StringLength(8, MinimumLength = 8, ErrorMessage = "Number must be 8 cifres")]
		[Required]
		public string BidCustomPhone { get; set; }
		
		[Display(Name = "Bid timestamp")]
		public DateTime BidTimestamp { get; set; }
	}
}
