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
    [Route("api/Seguroes")]
    public class SeguroesController : Controller
    {
        private readonly SegurosContext _context;

        public SeguroesController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Generoes
        [HttpGet]
        [Route("ListarSeguros")]
        public IEnumerable<Seguro> GetSeguro()
        {
            return _context.Seguro;
        }

        // GET: api/Generoes/5
        [HttpGet("{id}")]
        public async Task<Response> GetSeguro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var seguro = await _context.Seguro.SingleOrDefaultAsync(m => m.IdSeguro == id);

            if (seguro == null)
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
                Resultado = seguro
            };
        }

        // PUT: api/Generoes/5
        [HttpPut("{id}")]
        public async Task<Response> PutSeguro([FromRoute] int id, [FromBody] Seguro seguro)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != seguro.IdSeguro)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(seguro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };


        }

        // POST: api/Generoes
        [HttpPost]
        [Route("InsertarSeguros")]
        public async Task<Response> InsertarSeguro([FromBody] Seguro seguro)
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

                _context.Seguro.Add(seguro);
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

        // DELETE: api/Generoes/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteSeguro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var respuesta = await _context.Seguro.SingleOrDefaultAsync(m => m.IdSeguro == id);
            if (respuesta == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Seguro.Remove(respuesta);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool SeguroExists(int id)
        {
            return _context.Seguro.Any(e => e.IdSeguro == id);
        }
    }
}