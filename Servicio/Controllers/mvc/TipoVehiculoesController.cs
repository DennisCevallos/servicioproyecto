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
    public class TipoVehiculoesController : Controller
    {
        private readonly SegurosContext _context;

        public TipoVehiculoesController(SegurosContext context)
        {
            _context = context;
        }

        // GET: TipoVehiculoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoVehiculo.ToListAsync());
        }

        // GET: TipoVehiculoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVehiculo = await _context.TipoVehiculo
                .SingleOrDefaultAsync(m => m.IdTipoVehiculo == id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }

            return View(tipoVehiculo);
        }

        // GET: TipoVehiculoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoVehiculoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipoVehiculo,Descripcion,Estado")] TipoVehiculo tipoVehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoVehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoVehiculo);
        }

        // GET: TipoVehiculoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVehiculo = await _context.TipoVehiculo.SingleOrDefaultAsync(m => m.IdTipoVehiculo == id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }
            return View(tipoVehiculo);
        }

        // POST: TipoVehiculoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipoVehiculo,Descripcion,Estado")] TipoVehiculo tipoVehiculo)
        {
            if (id != tipoVehiculo.IdTipoVehiculo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoVehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoVehiculoExists(tipoVehiculo.IdTipoVehiculo))
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
            return View(tipoVehiculo);
        }

        // GET: TipoVehiculoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoVehiculo = await _context.TipoVehiculo
                .SingleOrDefaultAsync(m => m.IdTipoVehiculo == id);
            if (tipoVehiculo == null)
            {
                return NotFound();
            }

            return View(tipoVehiculo);
        }

        // POST: TipoVehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoVehiculo = await _context.TipoVehiculo.SingleOrDefaultAsync(m => m.IdTipoVehiculo == id);
            _context.TipoVehiculo.Remove(tipoVehiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoVehiculoExists(int id)
        {
            return _context.TipoVehiculo.Any(e => e.IdTipoVehiculo == id);
        }
    }
}
