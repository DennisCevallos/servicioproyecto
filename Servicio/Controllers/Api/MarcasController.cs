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
    [Route("api/Marcas")]
    public class MarcasController : Controller
    {
        private readonly SegurosContext _context;

        public MarcasController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Marcas
        [Route("ListarMarcas")]
        [HttpGet]
        public IEnumerable<Marca> GetMarca()
        {
            return _context.Marca;
        }

        // GET: api/Marcas/5
        [HttpGet("{id}")]
        public async Task<Response> GetMarca([FromRoute] int id)
        {
            var marca = await _context.Marca.SingleOrDefaultAsync(m => m.IdMarca == id);

            if (marca != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = marca
                };
            }
            return new Response
            {
                IsSuccess = false,
                Message = Mensaje.ModeloInvalido
            };
        }

        // PUT: api/Marcas/5
        [HttpPut("{id}")]
        public async Task<Response> PutMarca([FromRoute] int id, [FromBody] Marca marca)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }
            var modificar = await _context.Marca.Where(x => x.IdMarca == id).FirstOrDefaultAsync();
            if (modificar != null)
            {
                modificar.Descripcion = marca.Descripcion;
                modificar.Estado = marca.Estado;
                _context.Marca.Update(modificar);
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
        // POST: api/Marcas
        [Route("InsertarMarca")]
        [HttpPost]
        public async Task<Response> PostMarca([FromBody] Marca marca)
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
                _context.Marca.Add(marca);
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
        // DELETE: api/Marcas/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteMarca([FromRoute] int id)
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
                var respuesta = await _context.Marca.SingleOrDefaultAsync(m => m.IdMarca == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                _context.Marca.Remove(respuesta);
                await _context.SaveChangesAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error,
                };
            }
        }
        private bool MarcaExists(int id)
        {
            return _context.Marca.Any(e => e.IdMarca == id);
        }
    }
}

