using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PROYECTO_SWI.Data;
using PROYECTO_SWI.Models;

namespace PROYECTO_SWI.Controllers
{
    public class HistorialPacientesController : Controller
    {
        private readonly PROYECTO_SWIContext _context;

        public HistorialPacientesController(PROYECTO_SWIContext context)
        {
            _context = context;
        }

        // GET: HistorialPacientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.HistorialPacientes.ToListAsync());
        }

        // GET: HistorialPacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialPaciente = await _context.HistorialPacientes
                .FirstOrDefaultAsync(m => m.IdHistorial == id);
            if (historialPaciente == null)
            {
                return NotFound();
            }

            return View(historialPaciente);
        }

        // GET: HistorialPacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HistorialPacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHistorial,DniPaciente,NumeroAtenciones,UltimaAtencion")] HistorialPaciente historialPaciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historialPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historialPaciente);
        }

        // GET: HistorialPacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialPaciente = await _context.HistorialPacientes.FindAsync(id);
            if (historialPaciente == null)
            {
                return NotFound();
            }
            return View(historialPaciente);
        }

        // POST: HistorialPacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHistorial,DniPaciente,NumeroAtenciones,UltimaAtencion")] HistorialPaciente historialPaciente)
        {
            if (id != historialPaciente.IdHistorial)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historialPaciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistorialPacienteExists(historialPaciente.IdHistorial))
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
            return View(historialPaciente);
        }

        // GET: HistorialPacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historialPaciente = await _context.HistorialPacientes
                .FirstOrDefaultAsync(m => m.IdHistorial == id);
            if (historialPaciente == null)
            {
                return NotFound();
            }

            return View(historialPaciente);
        }

        // POST: HistorialPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historialPaciente = await _context.HistorialPacientes.FindAsync(id);
            if (historialPaciente != null)
            {
                _context.HistorialPacientes.Remove(historialPaciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistorialPacienteExists(int id)
        {
            return _context.HistorialPacientes.Any(e => e.IdHistorial == id);
        }
    }
}
