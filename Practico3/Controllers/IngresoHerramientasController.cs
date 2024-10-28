using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practico3.Data;
using Practico3.Models;

namespace Practico3.Controllers
{
    public class IngresoHerramientasController : Controller
    {
        private readonly Contextt _context;

        public IngresoHerramientasController(Contextt context)
        {
            _context = context;
        }

        // GET: IngresoHerramientas
        public async Task<IActionResult> Index()
        {
            var contextt = _context.IngresoHerramientas.Include(i => i.Herramienta).Include(i => i.Usuario);
            return View(await contextt.ToListAsync());
        }

        // GET: IngresoHerramientas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IngresoHerramientas == null)
            {
                return NotFound();
            }

            var ingresoHerramienta = await _context.IngresoHerramientas
                .Include(i => i.Herramienta)
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingresoHerramienta == null)
            {
                return NotFound();
            }

            return View(ingresoHerramienta);
        }

        // GET: IngresoHerramientas/Create
        public IActionResult Create()
        {
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: IngresoHerramientas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HerramientaId,FechaIngreso,FechaDevolucion,EstaEnUso,UsuarioId")] IngresoHerramienta ingresoHerramienta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingresoHerramienta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id", ingresoHerramienta.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", ingresoHerramienta.UsuarioId);
            return View(ingresoHerramienta);
        }

        // GET: IngresoHerramientas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IngresoHerramientas == null)
            {
                return NotFound();
            }

            var ingresoHerramienta = await _context.IngresoHerramientas.FindAsync(id);
            if (ingresoHerramienta == null)
            {
                return NotFound();
            }
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id", ingresoHerramienta.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", ingresoHerramienta.UsuarioId);
            return View(ingresoHerramienta);
        }

        // POST: IngresoHerramientas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HerramientaId,FechaIngreso,FechaDevolucion,EstaEnUso,UsuarioId")] IngresoHerramienta ingresoHerramienta)
        {
            if (id != ingresoHerramienta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingresoHerramienta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngresoHerramientaExists(ingresoHerramienta.Id))
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
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id", ingresoHerramienta.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", ingresoHerramienta.UsuarioId);
            return View(ingresoHerramienta);
        }

        // GET: IngresoHerramientas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IngresoHerramientas == null)
            {
                return NotFound();
            }

            var ingresoHerramienta = await _context.IngresoHerramientas
                .Include(i => i.Herramienta)
                .Include(i => i.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingresoHerramienta == null)
            {
                return NotFound();
            }

            return View(ingresoHerramienta);
        }

        // POST: IngresoHerramientas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IngresoHerramientas == null)
            {
                return Problem("Entity set 'Contextt.IngresoHerramientas'  is null.");
            }
            var ingresoHerramienta = await _context.IngresoHerramientas.FindAsync(id);
            if (ingresoHerramienta != null)
            {
                _context.IngresoHerramientas.Remove(ingresoHerramienta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngresoHerramientaExists(int id)
        {
          return (_context.IngresoHerramientas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
