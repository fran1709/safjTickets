using devTicket.Models;
using devTicket.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace devTicket.Controllers
{
    public class DetalleEventoController : Controller
    {
        private readonly DevTicketContext _context;

        public DetalleEventoController(DevTicketContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Consulta
            var listaEventos = (from E in _context.Eventos
                                 join TE in _context.TipoEventos on E.IdTipoEvento equals TE.Id
                                 join ESC in _context.Escenarios on E.IdEscenario equals ESC.Id
                                 join TESC in _context.TipoEscenarios on ESC.Id equals TESC.IdEscenario
                                 where E.Active
                                 orderby E.Id ascending
                                 select new DetalleEvento
                                 {
                                     Id = E.Id,
                                     Descripcion = E.Descripcion,
                                     TipoEvento = TE.Descripcion,
                                     Fecha = E.Fecha,
                                     TipoEscenario = TESC.Descripcion,
                                     Escenario = ESC.Nombre,
                                     Localizacion = ESC.Localizacion
                                 }). ToListAsync();

            return View(await listaEventos);
        }

        public async Task<IActionResult> Create(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var evento = await _context.Eventos
                                             .Include(esc => esc.IdEscenarioNavigation)
                                            .ThenInclude(tesc => tesc.TipoEscenarios)
                                       .Include(te => te.IdTipoEventoNavigation)
                                       .Where(e => e.Active && e.Id == id)
                                       .Select(e => new DetalleEvento
                                       {
                                           Id = e.Id,
                                           Descripcion = e.Descripcion,
                                           TipoEvento = e.IdTipoEventoNavigation.Descripcion,
                                           Fecha = e.Fecha,
                                           TipoEscenario = e.IdEscenarioNavigation.TipoEscenarios.Select(te => te.Descripcion).FirstOrDefault(),
                                           Escenario = e.IdEscenarioNavigation.Nombre,
                                           Localizacion = e.IdEscenarioNavigation.Localizacion
                                       }).FirstOrDefaultAsync();

            if (evento == null) 
            { 
                return NotFound(); 
            }

            var asientos = await (from E in _context.Eventos
                                  join ESC in _context.Escenarios on E.IdEscenario equals ESC.Id
                                  join AS in _context.Asientos on ESC.Id equals AS.IdEscenario
                                  where E.Active && AS.Active && E.Id == id
                                  orderby E.Id ascending
                                  select new AsientoPrecio
                                  {
                                      Id = AS.Id,
                                      Descripcion = AS.Descripcion,
                                      Cantidad = AS.Cantidad,
                                      Precio = 0
                                  }).ToListAsync();

            if (asientos == null) { return NotFound(); }

            var eventoAsientos = new EventoAsientos
            {
                Id = evento.Id,
                Descripcion = evento.Descripcion,
                TipoEvento = evento.TipoEvento,
                Fecha = evento.Fecha,
                TipoEscenario = evento.TipoEscenario,
                Escenario = evento.Escenario,
                Localizacion = evento.Localizacion,
                Asientos = asientos
            };

            if (eventoAsientos == null) { return NotFound(); }

            return View(eventoAsientos);

        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var form = collection.ToList();
                    var idEvento = Int32.Parse(form[0].Value);
                    //verificar primero si ya existen las entradas porque solo se pueden crear una vez
                    var entradasEvento = await _context.Entradas.FirstOrDefaultAsync(m => m.IdEvento == idEvento);
                    if (entradasEvento == null)//si entradas no han sido creadas --> crearlas
                    {
                        var descripciones = form[1].Value.ToList();
                        var cantidades = form[2].Value.ToList();
                        var precios = form[3].Value.ToList();
                        for (var i = 0; i < descripciones.Count(); i++)
                        {
                            var entrada = new Entrada();
                            entrada.IdEvento = idEvento;
                            entrada.TipoAsiento = descripciones[i];
                            entrada.Disponibles = Int32.Parse(cantidades[i]);
                            entrada.Precio = Decimal.Parse(precios[i]);
                            _context.Add(entrada);
                            await _context.SaveChangesAsync();
                        }
                        TempData["Success"] = "Las Entradas fueron creadas exitosamente...";
                    }
                    else //mandar las entradas ya han sido creadas no se pueden volver a crear
                    {
                        TempData["Error"] = "Error, Las entradas ya fueron creadas...";
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: DetalleEventosController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetalleEventosController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DetalleEventosController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetalleEventosController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
