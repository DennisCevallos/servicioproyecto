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
    public class SeguroesController : Controller
    {
        private readonly SegurosContext _context;

        public SeguroesController(SegurosContext context)
        {
            _context = context;
        }

        // GET: Seguroes
        public async Task<IActionResult> Index()
        {
            var segurosContext = _context.Seguro.Include(s => s.IdPolizaNavigation).Include(s => s.IdVehiculoNavigation);
            return View(await segurosContext.ToListAsync());
        }

        // GET: Seguroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguro = await _context.Seguro
                .Include(s => s.IdPolizaNavigation)
                .Include(s => s.IdVehiculoNavigation)
                .SingleOrDefaultAsync(m => m.IdSeguro == id);
            if (seguro == null)
            {
                return NotFound();
            }

            return View(seguro);
        }

        // GET: Seguroes/Create
        public IActionResult Create()
        {
            ViewData["IdPoliza"] = new SelectList(_context.Poliza, "IdPoliza", "IdPoliza");
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculo, "IdVehiculo", "IdVehiculo");
            return View();
        }

        // POST: Seguroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSeguro,IdPoliza,IdVehiculo,ValAsegurado,Tasa,PrimaSeguro")] Seguro seguro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seguro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPoliza"] = new SelectList(_context.Poliza, "IdPoliza", "IdPoliza", seguro.IdPoliza);
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculo, "IdVehiculo", "IdVehiculo", seguro.IdVehiculo);
            return View(seguro);
        }

        // GET: Seguroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguro = await _context.Seguro.SingleOrDefaultAsync(m => m.IdSeguro == id);
            if (seguro == null)
            {
                return NotFound();
            }
            ViewData["IdPoliza"] = new SelectList(_context.Poliza, "IdPoliza", "IdPoliza", seguro.IdPoliza);
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculo, "IdVehiculo", "IdVehiculo", seguro.IdVehiculo);
            return View(seguro);
        }

        // POST: Seguroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSeguro,IdPoliza,IdVehiculo,ValAsegurado,Tasa,PrimaSeguro")] Seguro seguro)
        {
            if (id != seguro.IdSeguro)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seguro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeguroExists(seguro.IdSeguro))
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
            ViewData["IdPoliza"] = new SelectList(_context.Poliza, "IdPoliza", "IdPoliza", seguro.IdPoliza);
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculo, "IdVehiculo", "IdVehiculo", seguro.IdVehiculo);
            return View(seguro);
        }

        // GET: Seguroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seguro = await _context.Seguro
                .Include(s => s.IdPolizaNavigation)
                .Include(s => s.IdVehiculoNavigation)
                .SingleOrDefaultAsync(m => m.IdSeguro == id);
            if (seguro == null)
            {
                return NotFound();
            }

            return View(seguro);
        }

        // POST: Seguroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seguro = await _context.Seguro.SingleOrDefaultAsync(m => m.IdSeguro == id);
            _context.Seguro.Remove(seguro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeguroExists(int id)
        {
            return _context.Seguro.Any(e => e.IdSeguro == id);
        }
    }
}
