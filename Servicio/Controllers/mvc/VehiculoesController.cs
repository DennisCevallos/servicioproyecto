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
    public class VehiculoesController : Controller
    {
        private readonly SegurosContext _context;

        public VehiculoesController(SegurosContext context)
        {
            _context = context;
        }

        // GET: Vehiculoes
        public async Task<IActionResult> Index()
        {
            var segurosContext = _context.Vehiculo.Include(v => v.IdColorNavigation).Include(v => v.IdMarcaNavigation).Include(v => v.IdModeloNavigation).Include(v => v.IdTipoVehiculoNavigation);
            return View(await segurosContext.ToListAsync());
        }

        // GET: Vehiculoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculo
                .Include(v => v.IdColorNavigation)
                .Include(v => v.IdMarcaNavigation)
                .Include(v => v.IdModeloNavigation)
                .Include(v => v.IdTipoVehiculoNavigation)
                .SingleOrDefaultAsync(m => m.IdVehiculo == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // GET: Vehiculoes/Create
        public IActionResult Create()
        {
            ViewData["IdColor"] = new SelectList(_context.Color, "IdColor", "Descripcion");
            ViewData["IdMarca"] = new SelectList(_context.Marca, "IdMarca", "Descripcion");
            ViewData["IdModelo"] = new SelectList(_context.Modelo, "IdModelo", "Descripcion");
            ViewData["IdTipoVehiculo"] = new SelectList(_context.TipoVehiculo, "IdTipoVehiculo", "Descripcion");
            return View();
        }

        // POST: Vehiculoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVehiculo,IdMarca,IdModelo,Placa,Chasis,IdColor,Observaciones,IdTipoVehiculo,Estado,AnioDeFabricacion,Url")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdColor"] = new SelectList(_context.Color, "IdColor", "Descripcion", vehiculo.IdColor);
            ViewData["IdMarca"] = new SelectList(_context.Marca, "IdMarca", "Descripcion", vehiculo.IdMarca);
            ViewData["IdModelo"] = new SelectList(_context.Modelo, "IdModelo", "Descripcion", vehiculo.IdModelo);
            ViewData["IdTipoVehiculo"] = new SelectList(_context.TipoVehiculo, "IdTipoVehiculo", "Descripcion", vehiculo.IdTipoVehiculo);
            return View(vehiculo);
        }

        // GET: Vehiculoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculo.SingleOrDefaultAsync(m => m.IdVehiculo == id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            ViewData["IdColor"] = new SelectList(_context.Color, "IdColor", "Descripcion", vehiculo.IdColor);
            ViewData["IdMarca"] = new SelectList(_context.Marca, "IdMarca", "Descripcion", vehiculo.IdMarca);
            ViewData["IdModelo"] = new SelectList(_context.Modelo, "IdModelo", "Descripcion", vehiculo.IdModelo);
            ViewData["IdTipoVehiculo"] = new SelectList(_context.TipoVehiculo, "IdTipoVehiculo", "Descripcion", vehiculo.IdTipoVehiculo);
            return View(vehiculo);
        }

        // POST: Vehiculoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVehiculo,IdMarca,IdModelo,Placa,Chasis,IdColor,Observaciones,IdTipoVehiculo,Estado,AnioDeFabricacion,Url")] Vehiculo vehiculo)
        {
            if (id != vehiculo.IdVehiculo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.IdVehiculo))
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
            ViewData["IdColor"] = new SelectList(_context.Color, "IdColor", "Descripcion", vehiculo.IdColor);
            ViewData["IdMarca"] = new SelectList(_context.Marca, "IdMarca", "Descripcion", vehiculo.IdMarca);
            ViewData["IdModelo"] = new SelectList(_context.Modelo, "IdModelo", "Descripcion", vehiculo.IdModelo);
            ViewData["IdTipoVehiculo"] = new SelectList(_context.TipoVehiculo, "IdTipoVehiculo", "Descripcion", vehiculo.IdTipoVehiculo);
            return View(vehiculo);
        }

        // GET: Vehiculoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculo
                .Include(v => v.IdColorNavigation)
                .Include(v => v.IdMarcaNavigation)
                .Include(v => v.IdModeloNavigation)
                .Include(v => v.IdTipoVehiculoNavigation)
                .SingleOrDefaultAsync(m => m.IdVehiculo == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehiculo = await _context.Vehiculo.SingleOrDefaultAsync(m => m.IdVehiculo == id);
            _context.Vehiculo.Remove(vehiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculo.Any(e => e.IdVehiculo == id);
        }
    }
}
