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
    public class ReportesController : Controller
    {
        private readonly PROYECTO_SWIContext _context;

        public ReportesController(PROYECTO_SWIContext context)
        {
            _context = context;
        }

        // GET: Reportes
        public async Task<IActionResult> Index()
        {
            var pROYECTO_SWIContext = _context.Reportes.Include(r => r.Atencion);
            return View(await pROYECTO_SWIContext.ToListAsync());
        }

        // GET: Reportes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await _context.Reportes
                .Include(r => r.Atencion)
                .FirstOrDefaultAsync(m => m.IdReporte == id);
            if (reporte == null)
            {
                return NotFound();
            }

            return View(reporte);
        }

        // GET: Reportes/Create
        public IActionResult Create()
        {
            ViewData["IdAtencion"] = new SelectList(_context.Atenciones, "IdAtencion", "Detalles");
            return View();
        }

        // POST: Reportes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReporte,IdAtencion,DetallesAdicionales")] Reporte reporte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reporte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAtencion"] = new SelectList(_context.Atenciones, "IdAtencion", "Detalles", reporte.IdAtencion);
            return View(reporte);
        }

        // GET: Reportes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null)
            {
                return NotFound();
            }
            ViewData["IdAtencion"] = new SelectList(_context.Atenciones, "IdAtencion", "Detalles", reporte.IdAtencion);
            return View(reporte);
        }

        // POST: Reportes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReporte,IdAtencion,DetallesAdicionales")] Reporte reporte)
        {
            if (id != reporte.IdReporte)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reporte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReporteExists(reporte.IdReporte))
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
            ViewData["IdAtencion"] = new SelectList(_context.Atenciones, "IdAtencion", "Detalles", reporte.IdAtencion);
            return View(reporte);
        }

        // GET: Reportes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reporte = await _context.Reportes
                .Include(r => r.Atencion)
                .FirstOrDefaultAsync(m => m.IdReporte == id);
            if (reporte == null)
            {
                return NotFound();
            }

            return View(reporte);
        }

        // POST: Reportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte != null)
            {
                _context.Reportes.Remove(reporte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReporteExists(int id)
        {
            return _context.Reportes.Any(e => e.IdReporte == id);
        }

        [HttpGet]
        public IActionResult GenerarReporte()
        {
            return View();
        }

        // POST: Reportes/GenerarReporte
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerarReporte(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                ModelState.AddModelError("", "La fecha de inicio no puede ser mayor que la fecha de fin.");
                return View();
            }

            // Filtra los datos usando LINQ
            var reportes = await _context.Reportes
                .Include(r => r.Atencion)
                .Where(r => r.Atencion.Fecha >= fechaInicio && r.Atencion.Fecha <= fechaFin)
                .ToListAsync();

            return View(reportes);
        }
    }
}
