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
    public class TiposPacientesController : Controller
    {
        private readonly PROYECTO_SWIContext _context;

        public TiposPacientesController(PROYECTO_SWIContext context)
        {
            _context = context;
        }

        // GET: TiposPacientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposPacientes.ToListAsync());
        }

        // GET: TiposPacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposPaciente = await _context.TiposPacientes
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tiposPaciente == null)
            {
                return NotFound();
            }

            return View(tiposPaciente);
        }

        // GET: TiposPacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposPacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipo,Tipo")] TiposPaciente tiposPaciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tiposPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tiposPaciente);
        }

        // GET: TiposPacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposPaciente = await _context.TiposPacientes.FindAsync(id);
            if (tiposPaciente == null)
            {
                return NotFound();
            }
            return View(tiposPaciente);
        }

        // POST: TiposPacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipo,Tipo")] TiposPaciente tiposPaciente)
        {
            if (id != tiposPaciente.IdTipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tiposPaciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TiposPacienteExists(tiposPaciente.IdTipo))
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
            return View(tiposPaciente);
        }

        // GET: TiposPacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tiposPaciente = await _context.TiposPacientes
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (tiposPaciente == null)
            {
                return NotFound();
            }

            return View(tiposPaciente);
        }

        // POST: TiposPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tiposPaciente = await _context.TiposPacientes.FindAsync(id);
            if (tiposPaciente != null)
            {
                _context.TiposPacientes.Remove(tiposPaciente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TiposPacienteExists(int id)
        {
            return _context.TiposPacientes.Any(e => e.IdTipo == id);
        }
    }
}
