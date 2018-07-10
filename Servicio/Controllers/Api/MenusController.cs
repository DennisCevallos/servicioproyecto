using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using Entidades.Negocio;
using Entidades.Utils;

namespace Servicio.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Menus")]
    public class MenusController : Controller
    {
        private readonly SegurosContext _context;

        public MenusController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Generoes
        [HttpGet]
        [Route("ListarMenus")]
        public IEnumerable<Menu> GetMenu()
        {
            return _context.Menu;
        }

        // GET: api/Generoes/5
        [HttpGet("{id}")]
        public async Task<Response> GetMenu([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var menu = await _context.Menu.SingleOrDefaultAsync(m => m.IdMenu == id);

            if (menu == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio,
                Resultado = menu
            };
        }

        // PUT: api/Menus/5
        [HttpPut("{id}")]
        public async Task<Response> PutMenu([FromRoute] int id, [FromBody] Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != menu.IdMenu)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(menu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };


        }

        // POST: api/Menus
        [HttpPost]
        [Route("InsertarMenus")]
        public async Task<Response> InsertarMenus([FromBody] Menu menu)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.ModeloInvalido
                    };
                }

                _context.Menu.Add(menu);
                await _context.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
                };

            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteMenu([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var menu = await _context.Menu.SingleOrDefaultAsync(m => m.IdMenu == id);
            if (menu == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.IdMenu == id);
        }
    }
}