using Assessment.Data;
using Assessment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assessment.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppliationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(AppliationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

       
        public async Task<IActionResult> ImportJsonData()
        {
            string filePath = Path.Combine(_env.WebRootPath, "data", "stock_market_data.json");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            string jsonData = await System.IO.File.ReadAllTextAsync(filePath);
            List<sqlModel> data = JsonSerializer.Deserialize<List<sqlModel>>(jsonData);

            if (data != null)
            {
                await _context.sqlModels.AddRangeAsync(data);
                await _context.SaveChangesAsync();
                return Ok("Data imported successfully.");
            }

            return BadRequest("Failed to import data.");
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.sqlModels.ToListAsync();
            return View(data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




//using Assessment.Data;
//using Assessment.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Diagnostics;
//using System.Net.Http;
//using System.Text.Json;

//namespace Assessment.Controllers
//{
//	public class HomeController : Controller
//	{

//        private readonly AppliationDbContext _context;
//        private readonly IWebHostEnvironment _env;

//        public HomeController(AppliationDbContext context, IWebHostEnvironment env)
//        {
//            _context = context;
//            _env = env;
//        }

//        [HttpPost]
//        public async Task<IActionResult> ImportJsonData()
//        {
//            string filePath = Path.Combine(_env.WebRootPath, "data", "stock_market_data.json");

//            if (!System.IO.File.Exists(filePath))
//            {
//                return NotFound("File not found.");
//            }

//            string jsonData = await System.IO.File.ReadAllTextAsync(filePath);
//            List<sqlModel> data = JsonSerializer.Deserialize<List<sqlModel>>(jsonData);

//            if (data != null)
//            {
//                await _context.sqlModels.AddRangeAsync(data);
//                await _context.SaveChangesAsync();
//                return Ok("Data imported successfully.");
//            }

//            return BadRequest("Failed to import data.");
//        }

//        // private readonly HttpClient _httpClient;
//        //private readonly AppliationDbContext _context;

//        //public HomeController(AppliationDbContext context)
//        //{
//        //   // _httpClient = httpClient;
//        //    _context = context;
//        //}

//        //public async Task<IActionResult> LoadData()
//        //{
//        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","data", "trade_data.json");
//        //    string jsonData = string.Empty;

//        //    try
//        //    {
//        //        if (!System.IO.File.Exists(filePath))
//        //        {
//        //            throw new Exception("The JSON file was not found.");
//        //        }

//        //        jsonData = await System.IO.File.ReadAllTextAsync(filePath);

//        //        var dataList = JsonSerializer.Deserialize<List<sqlModel>>(jsonData);

//        //        foreach (var data in dataList)
//        //        {
//        //            if (!_context.sqlModels.Any(d => d.date == data.date && d.trade_code == data.trade_code && d.high == data.high && d.low == data.low && d.open == data.open && d.close == data.close && d.volume == data.volume))
//        //            {
//        //                _context.sqlModels.Add(data);
//        //            }
//        //        }

//        //        await _context.SaveChangesAsync();

//        //        return RedirectToAction(nameof(Index));
//        //    }
//        //    catch (FileNotFoundException e)
//        //    {
//        //        // Handle file not found exceptions
//        //        ViewBag.Error = "Error fetching data: " + e.Message;
//        //    }
//        //    catch (JsonException e)
//        //    {
//        //        // Handle JSON parsing exceptions
//        //        ViewBag.Error = "Error parsing JSON data: " + e.Message;
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        // Handle general exceptions
//        //        ViewBag.Error = "An error occurred: " + e.Message;
//        //    }

//        //    return View("Error");
//        //var jsonFileUrl = "https://drive.google.com/uc?export=download&id=15-C8dUTSEwy5bCrVI9ZxYNwHN0ZtY-o-";
//        //var jsonData = await _httpClient.GetStringAsync(jsonFileUrl);
//        //var dataList = JsonSerializer.Deserialize<List<sqlModel>>(jsonData);

//        //foreach (var data in dataList)
//        //{
//        //    if (!_context.sqlModels.Any(d => d.date == data.date && d.trade_code == data.trade_code && d.high == data.high && d.low == data.low && d.open == data.open && d.close == data.close && d.volume == data.volume))
//        //    {
//        //        _context.sqlModels.Add(data);
//        //    }
//        //}

//        //await _context.SaveChangesAsync();

//        //return RedirectToAction(nameof(Index));
//    }

//        public async Task<IActionResult> Index()
//        {
//            var data = await _context.sqlModels.ToListAsync();
//            return View(data);
//        }


//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//		public IActionResult Error()
//		{
//			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//		}
//	}
//}
