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
    public class ItemMenusController : Controller
    {
        private readonly SegurosContext _context;

        public ItemMenusController(SegurosContext context)
        {
            _context = context;
        }

        // GET: ItemMenus
        public async Task<IActionResult> Index()
        {
            var segurosContext = _context.ItemMenu.Include(i => i.IdMenuNavigation).Include(i => i.IdPerfilNavigation);
            return View(await segurosContext.ToListAsync());
        }

        // GET: ItemMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemMenu = await _context.ItemMenu
                .Include(i => i.IdMenuNavigation)
                .Include(i => i.IdPerfilNavigation)
                .SingleOrDefaultAsync(m => m.IdSubMenu == id);
            if (itemMenu == null)
            {
                return NotFound();
            }

            return View(itemMenu);
        }

        // GET: ItemMenus/Create
        public IActionResult Create()
        {
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "IdMenu");
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "IdPerfil");
            return View();
        }

        // POST: ItemMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSubMenu,IdPerfil,IdMenu,Estado")] ItemMenu itemMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "IdMenu", itemMenu.IdMenu);
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "IdPerfil", itemMenu.IdPerfil);
            return View(itemMenu);
        }

        // GET: ItemMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemMenu = await _context.ItemMenu.SingleOrDefaultAsync(m => m.IdSubMenu == id);
            if (itemMenu == null)
            {
                return NotFound();
            }
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "IdMenu", itemMenu.IdMenu);
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "IdPerfil", itemMenu.IdPerfil);
            return View(itemMenu);
        }

        // POST: ItemMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSubMenu,IdPerfil,IdMenu,Estado")] ItemMenu itemMenu)
        {
            if (id != itemMenu.IdSubMenu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemMenuExists(itemMenu.IdSubMenu))
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
            ViewData["IdMenu"] = new SelectList(_context.Menu, "IdMenu", "IdMenu", itemMenu.IdMenu);
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "IdPerfil", itemMenu.IdPerfil);
            return View(itemMenu);
        }

        // GET: ItemMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemMenu = await _context.ItemMenu
                .Include(i => i.IdMenuNavigation)
                .Include(i => i.IdPerfilNavigation)
                .SingleOrDefaultAsync(m => m.IdSubMenu == id);
            if (itemMenu == null)
            {
                return NotFound();
            }

            return View(itemMenu);
        }

        // POST: ItemMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemMenu = await _context.ItemMenu.SingleOrDefaultAsync(m => m.IdSubMenu == id);
            _context.ItemMenu.Remove(itemMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemMenuExists(int id)
        {
            return _context.ItemMenu.Any(e => e.IdSubMenu == id);
        }
    }
}
