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
    public class MedicinasController : Controller
    {
        private readonly PROYECTO_SWIContext _context;

        public MedicinasController(PROYECTO_SWIContext context)
        {
            _context = context;
        }

        // GET: Medicinas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medicinas.ToListAsync());
        }

        // GET: Medicinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicina = await _context.Medicinas
                .FirstOrDefaultAsync(m => m.IdMedicina == id);
            if (medicina == null)
            {
                return NotFound();
            }

            return View(medicina);
        }

        // GET: Medicinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medicinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMedicina,NombreMedicina,Stock,Descripcion")] Medicina medicina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicina);
        }

        // GET: Medicinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicina = await _context.Medicinas.FindAsync(id);
            if (medicina == null)
            {
                return NotFound();
            }
            return View(medicina);
        }

        // POST: Medicinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMedicina,NombreMedicina,Stock,Descripcion")] Medicina medicina)
        {
            if (id != medicina.IdMedicina)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicinaExists(medicina.IdMedicina))
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
            return View(medicina);
        }

        // GET: Medicinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicina = await _context.Medicinas
                .FirstOrDefaultAsync(m => m.IdMedicina == id);
            if (medicina == null)
            {
                return NotFound();
            }

            return View(medicina);
        }

        // POST: Medicinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicina = await _context.Medicinas.FindAsync(id);
            if (medicina != null)
            {
                _context.Medicinas.Remove(medicina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicinaExists(int id)
        {
            return _context.Medicinas.Any(e => e.IdMedicina == id);
        }
    }
}
