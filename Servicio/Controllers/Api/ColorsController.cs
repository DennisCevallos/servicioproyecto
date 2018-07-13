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
    [Route("api/Colors")]
    public class ColorsController : Controller
    {
        private readonly SegurosContext _context;

        public ColorsController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Colors
        [HttpGet]
        [Route("ListarColors")]
        public IEnumerable<Color> GetColor()
        {
            return _context.Color;
        }

        // GET: api/Colors/5
        [HttpGet("{id}")]
        public async Task<Response> GetColor([FromRoute] int id)
        {
            var color = await _context.Color.SingleOrDefaultAsync(m => m.IdColor == id);

            if (color != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = color
                };
            }
            return new Response
            {
                IsSuccess = false,
                Message = Mensaje.ModeloInvalido
            };
        }

        // PUT: api/Colors/5
        [HttpPut("{id}")]
        public async Task<Response> PutColor([FromRoute] int id, [FromBody] Color color)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }
            var modificar = await _context.Color.Where(x => x.IdColor == id).FirstOrDefaultAsync();
            if (modificar != null)
            {
                modificar.Descripcion = color.Descripcion;
                modificar.Estado = color.Estado;
                _context.Color.Update(modificar);
                await _context.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
                };
            }
            return new Response
            {
                IsSuccess = false,
                Message = Mensaje.Error
            };
        }

        // POST: api/Colors
        [HttpPost]
        [Route("InsertarColor")]
        public async Task<Response> PostColor([FromBody] Color color)
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
                _context.Color.Add(color);
                await _context.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }
        }
        // DELETE: api/Colors/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteColor([FromRoute] int id)
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
                var respuesta = await _context.Color.SingleOrDefaultAsync(m => m.IdColor == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                _context.Color.Remove(respuesta);
                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio
                };

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }
        }

        private bool ColorExists(int id)
        {
            return _context.Color.Any(e => e.IdColor == id);
        }
    }
}