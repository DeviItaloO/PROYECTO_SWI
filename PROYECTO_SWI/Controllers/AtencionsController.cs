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
    public class AtencionsController : Controller
    {
        private readonly PROYECTO_SWIContext _context;

        public AtencionsController(PROYECTO_SWIContext context)
        {
            _context = context;
        }

        // GET: Atencions
        public async Task<IActionResult> Index()
        {
            var pROYECTO_SWIContext = _context.Atenciones.Include(a => a.Paciente);
            return View(await pROYECTO_SWIContext.ToListAsync());
        }

        // GET: Atencions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atencion = await _context.Atenciones
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.IdAtencion == id);
            if (atencion == null)
            {
                return NotFound();
            }

            return View(atencion);
        }

        // GET: Atencions/Create
        public IActionResult Create()
        {
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido");
            return View();
        }

        // POST: Atencions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAtencion,IdPaciente,Fecha,Hora,Detalles")] Atencion atencion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atencion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido", atencion.IdPaciente);
            return View(atencion);
        }

        // GET: Atencions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atencion = await _context.Atenciones.FindAsync(id);
            if (atencion == null)
            {
                return NotFound();
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido", atencion.IdPaciente);
            return View(atencion);
        }

        // POST: Atencions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAtencion,IdPaciente,Fecha,Hora,Detalles")] Atencion atencion)
        {
            if (id != atencion.IdAtencion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atencion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtencionExists(atencion.IdAtencion))
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
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "IdPaciente", "Apellido", atencion.IdPaciente);
            return View(atencion);
        }

        // GET: Atencions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var atencion = await _context.Atenciones
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.IdAtencion == id);
            if (atencion == null)
            {
                return NotFound();
            }

            return View(atencion);
        }

        // POST: Atencions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var atencion = await _context.Atenciones.FindAsync(id);
            if (atencion != null)
            {
                _context.Atenciones.Remove(atencion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtencionExists(int id)
        {
            return _context.Atenciones.Any(e => e.IdAtencion == id);
        }
    }
}
