using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.EntityFrameworkCore;
using TCC.Data;
using TCC.Models;

namespace TCC.Controllers
{
    public class ReceitasController : Controller
    {
        private readonly TCCContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ReceitasController(TCCContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Receitas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Receita.ToListAsync());
        }

        // GET: Receitas/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Receitas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Receitas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Título,Ingredientes,Preparo,FotoRe")] Receita receita, IFormFile FotoRe)
        {
            if (ModelState.IsValid)
            {
                if (FotoRe != null)
                {
                    string pasta = Path.Combine(webHostEnvironment.WebRootPath, "img\\receitas");
                    var nomeArquivo = Guid.NewGuid().ToString() + "_" + FotoRe.FileName;
                    string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await FotoRe.CopyToAsync(stream);
                    };
                    receita.FotoRe = "/img/receitas/" + nomeArquivo;
                }
                _context.Add(receita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(receita);
        }

        // GET: Receitas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas.FindAsync(id);
            if (receita == null)
            {
                return NotFound();
            }
            ViewData["CaminhoFoto"] = webHostEnvironment.WebRootPath;
            return View(receita);
        }

        // POST: Receitas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Título,Ingredientes,Preparo,FotoRe")] Receita receita, IFormFile NovaFoto)
        {
            if (id != receita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (NovaFoto != null)
                    {
                        string pasta = Path.Combine(webHostEnvironment.WebRootPath, "img\\receitas");
                        var nomeArquivo = Guid.NewGuid().ToString() + "_" + NovaFoto.FileName;
                        string caminhoArquivo = Path.Combine(pasta, nomeArquivo);
                        using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                        {
                            await NovaFoto.CopyToAsync(stream);
                        };
                        receita.FotoRe = "/img/receitas/" + nomeArquivo;
                    }
                    _context.Update(receita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceitaExists(receita.Id))
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
            return View(receita);
        }

        // GET: Receitas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receita == null)
            {
                return NotFound();
            }

            return View(receita);
        }

        // POST: Receitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receita = await _context.Receitas.FindAsync(id);
            _context.Receitas.Remove(receita);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceitaExists(int id)
        {
            return _context.Receita.Any(e => e.Id == id);
        }
    }
}
