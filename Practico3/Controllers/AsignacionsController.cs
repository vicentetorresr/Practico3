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
    public class AsignacionsController : Controller
    {
        private readonly Contextt _context;

        public AsignacionsController(Contextt context)
        {
            _context = context;
        }

        // GET: Asignacions
        public async Task<IActionResult> Index()
        {
            var contextt = _context.Asignaciones.Include(a => a.Herramienta).Include(a => a.Usuario);
            return View(await contextt.ToListAsync());
        }

        // GET: Asignacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asignaciones == null)
            {
                return NotFound();
            }

            var asignacion = await _context.Asignaciones
                .Include(a => a.Herramienta)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignacion == null)
            {
                return NotFound();
            }

            return View(asignacion);
        }

        // GET: Asignacions/Create
        public IActionResult Create()
        {
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Asignacions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HerramientaId,UsuarioId,FechaAsignacion,FechaDevolucion,Estado")] Asignacion asignacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignacion);
                await _context.SaveChangesAsync();

                // Recuperar el usuario asignado y actualizar HerramientasAsignadas
                var usuario = await _context.Usuarios
                    .Include(u => u.Asignaciones) // Incluir las asignaciones para calcular el total
                    .FirstOrDefaultAsync(u => u.Id == asignacion.UsuarioId);

                if (usuario != null)
                {
                    usuario.ActualizarHerramientasAsignadas(); // Actualizar el campo
                    _context.Update(usuario); // Marcar el usuario como modificado
                    await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
                }

                return RedirectToAction(nameof(Index));
            }

            // Volver a cargar los datos de selección si el modelo no es válido
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id", asignacion.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", asignacion.UsuarioId);
            return View(asignacion);
        }


        // GET: Asignacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asignaciones == null)
            {
                return NotFound();
            }

            var asignacion = await _context.Asignaciones.FindAsync(id);
            if (asignacion == null)
            {
                return NotFound();
            }
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id", asignacion.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", asignacion.UsuarioId);
            return View(asignacion);
        }

        // POST: Asignacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HerramientaId,UsuarioId,FechaAsignacion,FechaDevolucion,Estado")] Asignacion asignacion)
        {
            if (id != asignacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignacionExists(asignacion.Id))
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
            ViewData["HerramientaId"] = new SelectList(_context.Herramientas, "Id", "Id", asignacion.HerramientaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", asignacion.UsuarioId);
            return View(asignacion);
        }

        // GET: Asignacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asignaciones == null)
            {
                return NotFound();
            }

            var asignacion = await _context.Asignaciones
                .Include(a => a.Herramienta)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignacion == null)
            {
                return NotFound();
            }

            return View(asignacion);
        }

        // POST: Asignacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asignaciones == null)
            {
                return Problem("Entity set 'Contextt.Asignaciones'  is null.");
            }
            var asignacion = await _context.Asignaciones.FindAsync(id);
            if (asignacion != null)
            {
                _context.Asignaciones.Remove(asignacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignacionExists(int id)
        {
          return (_context.Asignaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
