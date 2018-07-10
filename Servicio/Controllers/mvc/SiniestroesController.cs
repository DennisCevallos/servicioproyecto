using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Negocio;

namespace Servicio.Controllers.mvc
{
    public class SiniestroesController : Controller
    {
        private readonly SegurosContext _context;

        public SiniestroesController(SegurosContext context)
        {
            _context = context;
        }

        // GET: Siniestroes
        public async Task<IActionResult> Index()
        {
            var segurosContext = _context.Siniestro.Include(s => s.IdVehiculoNavigation);
            return View(await segurosContext.ToListAsync());
        }

        // GET: Siniestroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siniestro = await _context.Siniestro
                .Include(s => s.IdVehiculoNavigation)
                .SingleOrDefaultAsync(m => m.IdSiniestro == id);
            if (siniestro == null)
            {
                return NotFound();
            }

            return View(siniestro);
        }

        // GET: Siniestroes/Create
        public IActionResult Create()
        {
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculo, "IdVehiculo", "IdVehiculo");
            return View();
        }

        // POST: Siniestroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSiniestro,Fecha,CallePrincipal,CalleSecundaria,Referencia,IdVehiculo")] Siniestro siniestro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siniestro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculo, "IdVehiculo", "IdVehiculo", siniestro.IdVehiculo);
            return View(siniestro);
        }

        // GET: Siniestroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siniestro = await _context.Siniestro.SingleOrDefaultAsync(m => m.IdSiniestro == id);
            if (siniestro == null)
            {
                return NotFound();
            }
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculo, "IdVehiculo", "IdVehiculo", siniestro.IdVehiculo);
            return View(siniestro);
        }

        // POST: Siniestroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSiniestro,Fecha,CallePrincipal,CalleSecundaria,Referencia,IdVehiculo")] Siniestro siniestro)
        {
            if (id != siniestro.IdSiniestro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siniestro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiniestroExists(siniestro.IdSiniestro))
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
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculo, "IdVehiculo", "IdVehiculo", siniestro.IdVehiculo);
            return View(siniestro);
        }

        // GET: Siniestroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siniestro = await _context.Siniestro
                .Include(s => s.IdVehiculoNavigation)
                .SingleOrDefaultAsync(m => m.IdSiniestro == id);
            if (siniestro == null)
            {
                return NotFound();
            }

            return View(siniestro);
        }

        // POST: Siniestroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siniestro = await _context.Siniestro.SingleOrDefaultAsync(m => m.IdSiniestro == id);
            _context.Siniestro.Remove(siniestro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiniestroExists(int id)
        {
            return _context.Siniestro.Any(e => e.IdSiniestro == id);
        }
    }
}
