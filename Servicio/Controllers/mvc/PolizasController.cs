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
    public class PolizasController : Controller
    {
        private readonly SegurosContext _context;

        public PolizasController(SegurosContext context)
        {
            _context = context;
        }

        // GET: Polizas
        public async Task<IActionResult> Index()
        {
            var segurosContext = _context.Poliza.Include(p => p.IdPersonaNavigation);
            return View(await segurosContext.ToListAsync());
        }

        // GET: Polizas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poliza = await _context.Poliza
                .Include(p => p.IdPersonaNavigation)
                .SingleOrDefaultAsync(m => m.IdPoliza == id);
            if (poliza == null)
            {
                return NotFound();
            }

            return View(poliza);
        }

        // GET: Polizas/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "IdPersona");
            return View();
        }

        // POST: Polizas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPoliza,FechaCoverturaI,FechaCoverturaF,NumPoliza,Factura,TotValAsegurado,TotValPrima,IdPersona")] Poliza poliza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poliza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "IdPersona", poliza.IdPersona);
            return View(poliza);
        }

        // GET: Polizas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poliza = await _context.Poliza.SingleOrDefaultAsync(m => m.IdPoliza == id);
            if (poliza == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "IdPersona", poliza.IdPersona);
            return View(poliza);
        }

        // POST: Polizas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPoliza,FechaCoverturaI,FechaCoverturaF,NumPoliza,Factura,TotValAsegurado,TotValPrima,IdPersona")] Poliza poliza)
        {
            if (id != poliza.IdPoliza)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poliza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolizaExists(poliza.IdPoliza))
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
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "IdPersona", poliza.IdPersona);
            return View(poliza);
        }

        // GET: Polizas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poliza = await _context.Poliza
                .Include(p => p.IdPersonaNavigation)
                .SingleOrDefaultAsync(m => m.IdPoliza == id);
            if (poliza == null)
            {
                return NotFound();
            }

            return View(poliza);
        }

        // POST: Polizas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poliza = await _context.Poliza.SingleOrDefaultAsync(m => m.IdPoliza == id);
            _context.Poliza.Remove(poliza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolizaExists(int id)
        {
            return _context.Poliza.Any(e => e.IdPoliza == id);
        }
    }
}
