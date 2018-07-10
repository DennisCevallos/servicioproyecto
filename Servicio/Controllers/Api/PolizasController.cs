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
    [Route("api/Polizas")]
    public class PolizasController : Controller
    {
        private readonly SegurosContext _context;

        public PolizasController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Polizas
        [HttpGet]
        [Route("ListarPoliza")]
        public IEnumerable<Poliza> GetPoliza()
        {
            return _context.Poliza;
        }

        // GET: api/Polizas/5
        [HttpGet("{id}")]
        public async Task<Response> GetPoliza([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var poliza = await _context.Poliza.SingleOrDefaultAsync(m => m.IdPoliza == id);

            if (poliza == null)
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
                Resultado = poliza
            };
        }

        // PUT: api/Polizas/5
        [HttpPut("{id}")]
        public async Task<Response> PutPoliza([FromRoute] int id, [FromBody] Poliza poliza)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != poliza.IdPoliza)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(poliza).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };


        }

        // POST: api/Polizas
        [HttpPost]
        [Route("InsertarPoliza")]
        public async Task<Response> InsertarPoliza([FromBody] Poliza poliza)
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

                _context.Poliza.Add(poliza);
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

        // DELETE: api/Polizas/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePoliza([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var poliza = await _context.Poliza.SingleOrDefaultAsync(m => m.IdPoliza == id);
            if (poliza == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Poliza.Remove(poliza);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool PolizaExists(int id)
        {
            return _context.Poliza.Any(e => e.IdPoliza == id);
        }
    }
}