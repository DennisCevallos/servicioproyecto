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
    [Route("api/ItemMenus")]
    public class ItemMenusController : Controller
    {
        private readonly SegurosContext _context;

        public ItemMenusController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/ItemMenus
        [HttpGet]
        [Route("ListarItemMenu")]
        public IEnumerable<ItemMenu> GetItemMenu()
        {
            return _context.ItemMenu;
        }

        // GET: api/ItemMenus/5
        [HttpGet("{id}")]
        public async Task<Response> GetItemMenu([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var itemmenu = await _context.ItemMenu.SingleOrDefaultAsync(m => m.IdSubMenu == id);

            if (itemmenu == null)
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
                Resultado = itemmenu
            };
        }

        // PUT: api/ItemMenus/5
        [HttpPut("{id}")]
        public async Task<Response> PutItemMenu([FromRoute] int id, [FromBody] ItemMenu itemmenu)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != itemmenu.IdSubMenu)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(itemmenu).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };


        }

        // POST: api/ItemMenus
        [HttpPost]
        [Route("InsertarItemMenu")]
        public async Task<Response> InsertarItemMenu([FromBody] ItemMenu itemmenu)
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

                _context.ItemMenu.Add(itemmenu);
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

        // DELETE: api/ItemMenus/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteItemMenu([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var itemmenu = await _context.ItemMenu.SingleOrDefaultAsync(m => m.IdSubMenu == id);
            if (itemmenu == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.ItemMenu.Remove(itemmenu);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool ItemMenuExists(int id)
        {
            return _context.ItemMenu.Any(e => e.IdSubMenu == id);
        }
    }
}