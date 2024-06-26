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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(sqlModel obj)
        {
            _context.sqlModels.Add(obj);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public IActionResult Edit(int ?id)
        {
            if (id == null)
            {
                return NotFound();
            }
            sqlModel data = _context.sqlModels.FirstOrDefault(x => x.Id == id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(sqlModel obj)
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var model = await _context.sqlModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

     
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _context.sqlModels.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            _context.sqlModels.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


