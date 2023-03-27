﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using devTicket.Models;

namespace devTicket.Controllers
{
    public class AsientoesController : Controller
    {
        private readonly DevTicketContext _context;

        public AsientoesController(DevTicketContext context)
        {
            _context = context;
        }

        // GET: Asientoes
        public async Task<IActionResult> Index()
        {
            var devTicketContext = _context.Asientos.Include(a => a.IdEscenarioNavigation);
            return View(await devTicketContext.ToListAsync());
        }

        // GET: Asientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asientos == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos
                .Include(a => a.IdEscenarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asiento == null)
            {
                return NotFound();
            }

            return View(asiento);
        }

        // GET: Asientoes/Create
        public IActionResult Create()
        {
            ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id");
            return View();
        }

        // POST: Asientoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Cantidad,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEscenario")] Asiento asiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id", asiento.IdEscenario);
            return View(asiento);
        }

        // GET: Asientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asientos == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos.FindAsync(id);
            if (asiento == null)
            {
                return NotFound();
            }
            ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id", asiento.IdEscenario);
            return View(asiento);
        }

        // POST: Asientoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Cantidad,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,Active,IdEscenario")] Asiento asiento)
        {
            if (id != asiento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsientoExists(asiento.Id))
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
            ViewData["IdEscenario"] = new SelectList(_context.Escenarios, "Id", "Id", asiento.IdEscenario);
            return View(asiento);
        }

        // GET: Asientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asientos == null)
            {
                return NotFound();
            }

            var asiento = await _context.Asientos
                .Include(a => a.IdEscenarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asiento == null)
            {
                return NotFound();
            }

            return View(asiento);
        }

        // POST: Asientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asientos == null)
            {
                return Problem("Entity set 'DevTicketContext.Asientos'  is null.");
            }
            var asiento = await _context.Asientos.FindAsync(id);
            if (asiento != null)
            {
                _context.Asientos.Remove(asiento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsientoExists(int id)
        {
          return (_context.Asientos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
