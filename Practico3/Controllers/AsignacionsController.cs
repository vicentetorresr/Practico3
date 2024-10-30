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
                // Buscar el usuario que recibe la nueva asignación
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == asignacion.UsuarioId);

                if (usuario != null)
                {
                    // Verifica si el usuario tiene menos de 3 herramientas asignadas
                    if (usuario.HerramientasAsignadas <= 3)
                    {
                        using (var transaction = await _context.Database.BeginTransactionAsync())
                        {
                            try
                            {
                                // Agregar la nueva asignación
                                await _context.AddAsync(asignacion);
                                await _context.SaveChangesAsync(); // Guarda la nueva asignación

                                // Incrementar el conteo de herramientas asignadas
                                usuario.IncrementarHerramientasAsignadas(); // Aumentar el conteo

                                // Actualizar el usuario en la base de datos
                                _context.Entry(usuario).State = EntityState.Modified; // Marca al usuario como modificado
                                await _context.SaveChangesAsync(); // Guarda los cambios del usuario

                                // Confirmar la transacción
                                await transaction.CommitAsync();

                                return RedirectToAction(nameof(Index));
                            }
                            catch (Exception ex)
                            {
                                // En caso de error, revertir la transacción
                                await transaction.RollbackAsync();
                                ModelState.AddModelError("", "Ocurrió un error al crear la asignación: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        // Maneja el caso en que el usuario ya tiene 3 herramientas asignadas
                        ModelState.AddModelError("", "El usuario ya tiene 3 herramientas asignadas.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Usuario no encontrado.");
                }
            }

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
