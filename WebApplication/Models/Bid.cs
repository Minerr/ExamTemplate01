using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
	public class Bid
	{
		[Required]
		public int ItemNumber { get; set; }

		[Required]
		public int Price { get; set; }

		[StringLength(100, MinimumLength = 4, ErrorMessage = "Name must be between 4-100 characters")]
		[Required]
		public string CustomName { get; set; }

		[StringLength(8, MinimumLength = 8, ErrorMessage = "Number must be 8 cifres")]
		[Required]
		public string CustomPhone { get; set; }
	}
}
