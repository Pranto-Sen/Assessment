using Assessment.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace Assessment.Controllers
{
	public class HomeController : Controller
	{
		private readonly HttpClient _httpClient;

		public HomeController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<IActionResult> Index()
		{
			var jsonFileUrl = "https://drive.google.com/uc?export=download&id=15-C8dUTSEwy5bCrVI9ZxYNwHN0ZtY-o-";
			var jsonData = await _httpClient.GetStringAsync(jsonFileUrl);
			var data = JsonSerializer.Deserialize<List<Data>>(jsonData);

			return View(data);
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
