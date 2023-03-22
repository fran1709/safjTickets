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
    public class TipoEscenariosController : Controller
    {
        private readonly samTicketContext _context;

        public TipoEscenariosController(samTicketContext context)
        {
            _context = context;
        }

        // GET: TipoEscenarios
        public async Task<IActionResult> Index()
        {
              return _context.TipoEscenario != null ? 
                          View(await _context.TipoEscenario.ToListAsync()) :
                          Problem("Entity set 'samTicketContext.TipoEscenario'  is null.");
        }

        // GET: TipoEscenarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoEscenario == null)
            {
                return NotFound();
            }

            var tipoEscenario = await _context.TipoEscenario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEscenario == null)
            {
                return NotFound();
            }

            return View(tipoEscenario);
        }

        // GET: TipoEscenarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEscenarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEscenario")] TipoEscenario tipoEscenario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoEscenario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEscenario);
        }

        // GET: TipoEscenarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoEscenario == null)
            {
                return NotFound();
            }

            var tipoEscenario = await _context.TipoEscenario.FindAsync(id);
            if (tipoEscenario == null)
            {
                return NotFound();
            }
            return View(tipoEscenario);
        }

        // POST: TipoEscenarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEscenario")] TipoEscenario tipoEscenario)
        {
            if (id != tipoEscenario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoEscenario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEscenarioExists(tipoEscenario.Id))
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
            return View(tipoEscenario);
        }

        // GET: TipoEscenarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoEscenario == null)
            {
                return NotFound();
            }

            var tipoEscenario = await _context.TipoEscenario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEscenario == null)
            {
                return NotFound();
            }

            return View(tipoEscenario);
        }

        // POST: TipoEscenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoEscenario == null)
            {
                return Problem("Entity set 'samTicketContext.TipoEscenario'  is null.");
            }
            var tipoEscenario = await _context.TipoEscenario.FindAsync(id);
            if (tipoEscenario != null)
            {
                _context.TipoEscenario.Remove(tipoEscenario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEscenarioExists(int id)
        {
          return (_context.TipoEscenario?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
