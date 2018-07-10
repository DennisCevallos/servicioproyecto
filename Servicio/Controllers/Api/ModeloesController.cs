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
    [Route("api/Modeloes")]
    public class ModeloesController : Controller
    {
        private readonly SegurosContext _context;

        public ModeloesController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Modeloes
        [HttpGet]
        [Route("ListarModelo")]
        public IEnumerable<Modelo> GetModelo()
        {
            return _context.Modelo;
        }

        // GET: api/Modeloes/5
        [HttpGet("{id}")]
        public async Task<Response> GetModelo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var modelo = await _context.Modelo.SingleOrDefaultAsync(m => m.IdModelo == id);

            if (modelo == null)
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
                Resultado = modelo
            };
        }

        // PUT: api/Modeloes/5
        [HttpPut("{id}")]
        public async Task<Response> PutModelo([FromRoute] int id, [FromBody] Modelo modelo)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != modelo.IdModelo)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(modelo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };


        }

        // POST: api/Modeloes
        [HttpPost]
        [Route("InsertarModelo")]
        public async Task<Response> InsertarModelo([FromBody] Modelo modelo)
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

                _context.Modelo.Add(modelo);
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

        // DELETE: api/Modeloes/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteModelo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var modelo = await _context.Modelo.SingleOrDefaultAsync(m => m.IdModelo == id);
            if (modelo == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Modelo.Remove(modelo);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool ModeloExists(int id)
        {
            return _context.Modelo.Any(e => e.IdModelo == id);
        }
    }
}