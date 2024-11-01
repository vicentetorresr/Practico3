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
    public class MantenimientoesController : Controller
    {
        private readonly Contextt _context;

        public MantenimientoesController(Contextt context)
        {
            _context = context;
        }

        // GET: Mantenimientoes
        public async Task<IActionResult> Index()
        {
            var contextt = _context.Mantenimientos.Include(m => m.Herramienta);
            var mantenimientos = await contextt.ToListAsync();

            // Contar herramientas en mantenimiento
            ViewData["TotalHerramientasEnMantenimiento"] = mantenimientos.Count;

            return View(mantenimientos);
        }

        // GET: Mantenimientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mantenimientos == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimientos
                .Include(m => m.Herramienta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mantenimiento == null)
            {
                return NotFound();
            }

            return View(mantenimiento);
        }

        // GET: Mantenimientoes/Create
        public IActionResult Create()
        {
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Nombre");
            return View();
        }

        // POST: Mantenimientoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HerramientaId,FechaIngreso,FechaDevolucion")] Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mantenimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id", mantenimiento.HerramientaId);
            return View(mantenimiento);
        }

        // GET: Mantenimientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mantenimientos == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimientos.FindAsync(id);
            if (mantenimiento == null)
            {
                return NotFound();
            }
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id", mantenimiento.HerramientaId);
            return View(mantenimiento);
        }

        // POST: Mantenimientoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HerramientaId,FechaIngreso,FechaDevolucion")] Mantenimiento mantenimiento)
        {
            if (id != mantenimiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mantenimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MantenimientoExists(mantenimiento.Id))
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
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id", mantenimiento.HerramientaId);
            return View(mantenimiento);
        }

        // GET: Mantenimientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mantenimientos == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimientos
                .Include(m => m.Herramienta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mantenimiento == null)
            {
                return NotFound();
            }

            return View(mantenimiento);
        }

        // POST: Mantenimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mantenimientos == null)
            {
                return Problem("Entity set 'Contextt.Mantenimientos'  is null.");
            }
            var mantenimiento = await _context.Mantenimientos.FindAsync(id);
            if (mantenimiento != null)
            {
                _context.Mantenimientos.Remove(mantenimiento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MantenimientoExists(int id)
        {
          return (_context.Mantenimientos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
