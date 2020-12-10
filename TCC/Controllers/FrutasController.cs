using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.EntityFrameworkCore;
using TCC.Data;
using TCC.Models;

namespace TCC.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FrutasController : Controller
    {
        private readonly TCCContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public FrutasController(TCCContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Frutas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Frutas.ToListAsync());
        }

        // GET: Frutas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruta = await _context.Frutas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fruta == null)
            {
                return NotFound();
            }
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(fruta);
        }

        // GET: Frutas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Frutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Benefício1,Benefício2,Benefício3,Benefício4,Benefício5,Benefício6,Foto")] Fruta fruta, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "img\\frutas");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + Foto.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await Foto.CopyToAsync(stream);
                    };
                    fruta.Foto = "/img/frutas/" + nomeArquivo;
                }
                _context.Add(fruta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fruta);
        }

        // GET: Frutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruta = await _context.Frutas.FindAsync(id);
            if (fruta == null)
            {
                return NotFound();
            }
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(fruta);
        }

        // POST: Frutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Benefício1,Benefício2,Benefício3,Benefício4,Benefício5,Benefício6,Foto")] Fruta fruta, IFormFile NovaFoto)
        {
            if (id != fruta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (NovaFoto != null)
                    {
                        string pasta = Path.Combine(webHostEnvironment.WebRootPath, "img\\frutas");
                        var nomeArquivo = Guid.NewGuid().ToString() + "_" + NovaFoto.FileName;
                        string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                        using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                        {
                            await NovaFoto.CopyToAsync(stream);
                        };
                        fruta.Foto = "/img/frutas/" + nomeArquivo;
                    }
                    _context.Update(fruta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FrutaExists(fruta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(fruta);
        }

        // GET: Frutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fruta = await _context.Frutas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fruta == null)
            {
                return NotFound();
            }

            return View(fruta);
        }

        // POST: Frutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fruta = await _context.Frutas.FindAsync(id);
            _context.Frutas.Remove(fruta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FrutaExists(int id)
        {
            return _context.Frutas.Any(e => e.Id == id);
        }
    }
}
