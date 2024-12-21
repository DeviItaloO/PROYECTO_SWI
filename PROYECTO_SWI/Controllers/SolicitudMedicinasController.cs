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
    public class SolicitudMedicinasController : Controller
    {
        private readonly PROYECTO_SWIContext _context;

        public SolicitudMedicinasController(PROYECTO_SWIContext context)
        {
            _context = context;
        }

        // GET: SolicitudMedicinas
        public async Task<IActionResult> Index()
        {
            var pROYECTO_SWIContext = _context.SolicitudMedicinas.Include(s => s.Medicina);
            return View(await pROYECTO_SWIContext.ToListAsync());
        }

        // GET: SolicitudMedicinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudMedicina = await _context.SolicitudMedicinas
                .Include(s => s.Medicina)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitudMedicina == null)
            {
                return NotFound();
            }

            return View(solicitudMedicina);
        }

        // GET: SolicitudMedicinas/Create
        public IActionResult Create()
        {
            ViewData["IdMedicina"] = new SelectList(_context.Medicinas, "IdMedicina", "Descripcion");
            return View();
        }

        // POST: SolicitudMedicinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSolicitud,IdMedicina,CantidadSolicitada,Fecha")] SolicitudMedicina solicitudMedicina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solicitudMedicina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMedicina"] = new SelectList(_context.Medicinas, "IdMedicina", "Descripcion", solicitudMedicina.IdMedicina);
            return View(solicitudMedicina);
        }

        // GET: SolicitudMedicinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudMedicina = await _context.SolicitudMedicinas.FindAsync(id);
            if (solicitudMedicina == null)
            {
                return NotFound();
            }
            ViewData["IdMedicina"] = new SelectList(_context.Medicinas, "IdMedicina", "Descripcion", solicitudMedicina.IdMedicina);
            return View(solicitudMedicina);
        }

        // POST: SolicitudMedicinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSolicitud,IdMedicina,CantidadSolicitada,Fecha")] SolicitudMedicina solicitudMedicina)
        {
            if (id != solicitudMedicina.IdSolicitud)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudMedicina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudMedicinaExists(solicitudMedicina.IdSolicitud))
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
            ViewData["IdMedicina"] = new SelectList(_context.Medicinas, "IdMedicina", "Descripcion", solicitudMedicina.IdMedicina);
            return View(solicitudMedicina);
        }

        // GET: SolicitudMedicinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solicitudMedicina = await _context.SolicitudMedicinas
                .Include(s => s.Medicina)
                .FirstOrDefaultAsync(m => m.IdSolicitud == id);
            if (solicitudMedicina == null)
            {
                return NotFound();
            }

            return View(solicitudMedicina);
        }

        // POST: SolicitudMedicinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solicitudMedicina = await _context.SolicitudMedicinas.FindAsync(id);
            if (solicitudMedicina != null)
            {
                _context.SolicitudMedicinas.Remove(solicitudMedicina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudMedicinaExists(int id)
        {
            return _context.SolicitudMedicinas.Any(e => e.IdSolicitud == id);
        }
    }
}
