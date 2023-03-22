using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using samTicket.Data;
using samTicket.Models;

namespace samTicket.Controllers
{
    public class EscenariosController : Controller
    {
        private readonly samTicketContext _context;

        public EscenariosController(samTicketContext context)
        {
            _context = context;
        }

        // GET: Escenarios
        public async Task<IActionResult> Index()
        {
              return _context.Escenario != null ? 
                          View(await _context.Escenario.ToListAsync()) :
                          Problem("Entity set 'samTicketContext.Escenario'  is null.");
        }

        // GET: Escenarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Escenario == null)
            {
                return NotFound();
            }

            var escenario = await _context.Escenario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escenario == null)
            {
                return NotFound();
            }

            return View(escenario);
        }

        // GET: Escenarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Escenarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Localizacion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active")] Escenario escenario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(escenario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(escenario);
        }

        // GET: Escenarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Escenario == null)
            {
                return NotFound();
            }

            var escenario = await _context.Escenario.FindAsync(id);
            if (escenario == null)
            {
                return NotFound();
            }
            return View(escenario);
        }

        // POST: Escenarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Localizacion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active")] Escenario escenario)
        {
            if (id != escenario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(escenario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EscenarioExists(escenario.Id))
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
            return View(escenario);
        }

        // GET: Escenarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Escenario == null)
            {
                return NotFound();
            }

            var escenario = await _context.Escenario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (escenario == null)
            {
                return NotFound();
            }

            return View(escenario);
        }

        // POST: Escenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Escenario == null)
            {
                return Problem("Entity set 'samTicketContext.Escenario'  is null.");
            }
            var escenario = await _context.Escenario.FindAsync(id);
            if (escenario != null)
            {
                _context.Escenario.Remove(escenario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EscenarioExists(int id)
        {
          return (_context.Escenario?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
