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
    public class HerramientasController : Controller
    {
        private readonly Contextt _context;

        public HerramientasController(Contextt context)
        {
            _context = context;
        }

        // GET: Herramientas
        public async Task<IActionResult> Index()
        {
            var contextt = _context.Herramientas.Include(h => h.Marca);
            return View(await contextt.ToListAsync());
        }

        // GET: Herramientas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Herramientas == null)
            {
                return NotFound();
            }

            var herramientas = await _context.Herramientas
                .Include(h => h.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (herramientas == null)
            {
                return NotFound();
            }

            return View(herramientas);
        }

        // GET: Herramientas/Create
        public IActionResult Create()
        {
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] Herramientas herramientas)
        {
            if (herramientas == null)
            {
                return Json(new { success = false, message = "No se recibieron datos." });
            }

            // Validaciones
            if (string.IsNullOrWhiteSpace(herramientas.Nombre))
            {
                return Json(new { success = false, message = "El nombre es obligatorio." });
            }

            if (string.IsNullOrWhiteSpace(herramientas.Modelo))
            {
                return Json(new { success = false, message = "El modelo es obligatorio." });
            }

            if (herramientas.MarcaId <= 0)
            {
                return Json(new { success = false, message = "La marca es obligatoria." });
            }

            var marcaExists = await _context.Marcas.AnyAsync(m => m.Id == herramientas.MarcaId);
            if (!marcaExists)
            {
                return Json(new { success = false, message = "La marca seleccionada no es válida." });
            }

            if (herramientas.CantidadTotal < 0)
            {
                return Json(new { success = false, message = "La cantidad total debe ser mayor que cero." });
            }

            try
            {
                _context.Add(herramientas);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Herramienta creada correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al crear la herramienta: {ex.Message}" });
            }
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Modelo,MarcaId,CantidadTotal")] Herramientas herramientas)
        {
            // Verifica si los campos obligatorios están vacíos
            if (string.IsNullOrWhiteSpace(herramientas.Nombre))
            {
                ModelState.AddModelError("Nombre", "El nombre es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(herramientas.Modelo))
            {
                ModelState.AddModelError("Modelo", "El modelo es obligatorio.");
            }

            // Verifica si MarcaId es válido
            if (herramientas.MarcaId <= 0)
            {
                ModelState.AddModelError("MarcaId", "La marca es obligatoria.");
            }
            else
            {
                // Verifica si la marca existe en la base de datos
                var marcaExists = await _context.Marcas.AnyAsync(m => m.Id == herramientas.MarcaId);
                if (!marcaExists)
                {
                    ModelState.AddModelError("MarcaId", "La marca seleccionada no es válida.");
                }
            }

            if (herramientas.CantidadTotal < 0)
            {
                ModelState.AddModelError("CantidadTotal", "La cantidad total debe ser mayor que cero.");
            }
            /*
            // Si hay errores en el ModelState, lanza una excepción con todos los errores
            if (!ModelState.IsValid)
            {
                var errorMessage = "Errores de validación: ";
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errorMessage += $"Campo: {state.Key}, Error: {error.ErrorMessage}\n";
                    }
                }

                throw new Exception(errorMessage + $" : Herramienta.Marca: {herramientas.Marca} herramienta.id: {herramientas.MarcaId}");
            }
            */
            // Agregar la herramienta al contexto
            _context.Add(herramientas);
            await _context.SaveChangesAsync();

            // Redirige a la acción Index después de guardar exitosamente
            return RedirectToAction(nameof(Index));
        }















        // GET: Herramientas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Herramientas == null)
            {
                return NotFound();
            }

            var herramientas = await _context.Herramientas.FindAsync(id);
            if (herramientas == null)
            {
                return NotFound();
            }
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Id", herramientas.MarcaId);
            return View(herramientas);
        }

        // POST: Herramientas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Modelo,MarcaId,CantidadTotal,CantidadDisponible,CantidadUsada,CantidadEnMantenimiento,Estado")] Herramientas herramientas)
        {
            if (id != herramientas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(herramientas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HerramientasExists(herramientas.Id))
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
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Id", herramientas.MarcaId);
            return View(herramientas);
        }

        // GET: Herramientas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Herramientas == null)
            {
                return NotFound();
            }

            var herramientas = await _context.Herramientas
                .Include(h => h.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (herramientas == null)
            {
                return NotFound();
            }

            return View(herramientas);
        }

        // POST: Herramientas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Herramientas == null)
            {
                return Problem("Entity set 'Contextt.Herramientas'  is null.");
            }
            var herramientas = await _context.Herramientas.FindAsync(id);
            if (herramientas != null)
            {
                _context.Herramientas.Remove(herramientas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HerramientasExists(int id)
        {
          return (_context.Herramientas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
