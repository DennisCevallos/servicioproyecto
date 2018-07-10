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
    public class LoginsController : Controller
    {
        private readonly SegurosContext _context;

        public LoginsController(SegurosContext context)
        {
            _context = context;
        }

        // GET: Logins
        public async Task<IActionResult> Index()
        {
            var segurosContext = _context.Login.Include(l => l.IdPerfilNavigation).Include(l => l.IdPersonaNavigation);
            return View(await segurosContext.ToListAsync());
        }

        // GET: Logins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .Include(l => l.IdPerfilNavigation)
                .Include(l => l.IdPersonaNavigation)
                .SingleOrDefaultAsync(m => m.IdLogin == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Logins/Create
        public IActionResult Create()
        {
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "Descripcion");
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Nombres");
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLogin,FechaCambio,Clave,Usuario,Estado,IdPersona,IdPerfil")] Login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "Descripcion", login.IdPerfil);
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Nombres", login.IdPersona);
            return View(login);
        }

        // GET: Logins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login.SingleOrDefaultAsync(m => m.IdLogin == id);
            if (login == null)
            {
                return NotFound();
            }
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "Descripcion", login.IdPerfil);
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Nombres", login.IdPersona);
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLogin,FechaCambio,Clave,Usuario,Estado,IdPersona,IdPerfil")] Login login)
        {
            if (id != login.IdLogin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginExists(login.IdLogin))
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
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "Descripcion", login.IdPerfil);
            ViewData["IdPersona"] = new SelectList(_context.Persona, "IdPersona", "Nombres", login.IdPersona);
            return View(login);
        }

        // GET: Logins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var login = await _context.Login
                .Include(l => l.IdPerfilNavigation)
                .Include(l => l.IdPersonaNavigation)
                .SingleOrDefaultAsync(m => m.IdLogin == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var login = await _context.Login.SingleOrDefaultAsync(m => m.IdLogin == id);
            _context.Login.Remove(login);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.IdLogin == id);
        }
    }
}
