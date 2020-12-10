using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TCC.Data;
using TCC.Models;

namespace TCC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TCCContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, TCCContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Frutas()
        {
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            var f = _context.Frutas.ToList();
            return View(f);
        }

        public IActionResult Calorias()
        {
            return View();
        }

        public IActionResult GastoCalorico()
        {
            return View();
        }

        public IActionResult IMC()
        {
            return View();
        }

        public IActionResult Receitas()
        {
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            var r = _context.Receitas.ToList();
            return View(r);
        }

        public async Task<IActionResult> DetalheReceita(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receita
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receita == null)
            {
                return NotFound();
            }
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(receita);
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
