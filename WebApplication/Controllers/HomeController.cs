using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly static string _apiUrl = "http://localhost:12177/api/AuctionHouse";

		public HomeController()
		{
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Overview()
		{
			// HttpClient: https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
			List<AuctionItem> auctionItems = new List<AuctionItem>();

			using(HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(_apiUrl);
				if(response.IsSuccessStatusCode)
				{
					auctionItems = await response.Content.ReadAsAsync<List<AuctionItem>>();
				}
			}

			return View(auctionItems);
		}

		[HttpGet]
		public async Task<IActionResult> BidDetails(int? id)
		{
			if(id == null)
			{
				return RedirectToAction("Overview", "Home");
			}

			AuctionItem item = new AuctionItem();

			using(HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = await client.GetAsync(_apiUrl + "/" + id);
				if(response.IsSuccessStatusCode)
				{
					item = await response.Content.ReadAsAsync<AuctionItem>();
				}
				else
				{
					return NotFound();
				}
			}

			return View(item); // Return the auctionItem to the view
		}

		[HttpPost]
		public async Task<IActionResult> BidDetails(AuctionItem item)
		{
			
			if(item == null)
			{
				return RedirectToAction("Overview", "Home");
			}

			if(ModelState.IsValid)
			{

				// Post call as Json with an object 'item'.
				using(HttpClient client = new HttpClient())
				{
					Bid bid = new Bid()
					{
						ItemNumber = item.ItemNumber,
						CustomName = item.BidCustomName,
						CustomPhone = item.BidCustomPhone,
						Price = item.BidPrice
					};

					HttpResponseMessage response = await client.PostAsJsonAsync(_apiUrl, bid);
					if(response.IsSuccessStatusCode)
					{
						// Handle your response if succesfull
						return RedirectToAction("Overview", "Home");
					}
					else
					{
						// Handle your response if not
						ModelState.AddModelError(String.Empty, "API StatusCode: " + response.StatusCode);
					}
				}

				/*
				 * // Put call with var id and an object item
				using(HttpClient client = new HttpClient())
				{
					HttpResponseMessage response = await client.PutAsJsonAsync(_apiUrl + "/" + id, item);
					if(response.IsSuccessStatusCode)
					{
						// Handle your response if succesfull
					}
					else
					{
						// Handle your response if not
					}
				}

				// Delete call with var id
				using(HttpClient client = new HttpClient())
				{
					HttpResponseMessage response = await client.DeleteAsync(_apiUrl + "/" + id);
					if(response.IsSuccessStatusCode)
					{
						// Handle your response if succesfull
					}
					else
					{
						// Handle your response if not
					}
				}

				
				// Get call where I read the content of the response to a List<Item>
				using(HttpClient client = new HttpClient())
				{
					HttpResponseMessage response = await client.GetAsync(_apiUrl);
					if(response.IsSuccessStatusCode)
					{
						// Handle your response if succesful
						// someVariable = await response.Content.ReadAsAsync<List<Item>>();
					}
					else
					{
						// Handle your response if not

						// Redirect to the Action in Controller.
						RedirectToAction("Index", "Home");
					}
				}
				*/


			}

			return View(item);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
