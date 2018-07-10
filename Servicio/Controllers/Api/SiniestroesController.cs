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
    [Route("api/Siniestroes")]
    public class SiniestroesController : Controller
    {
        private readonly SegurosContext _context;

        public SiniestroesController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Siniestroes
        [HttpGet]
        [Route("ListarSiniestro")]
        public IEnumerable<Siniestro> GetSiniestro()
        {
            return _context.Siniestro;
        }

        // GET: api/Siniestroes/5
        [HttpGet("{id}")]
        public async Task<Response> GetSiniestro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var siniestro = await _context.Siniestro.SingleOrDefaultAsync(m => m.IdSiniestro == id);

            if (siniestro == null)
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
                Resultado = siniestro
            };
        }

        // PUT: api/Generoes/5
        [HttpPut("{id}")]
        public async Task<Response> PutGenero([FromRoute] int id, [FromBody] Siniestro siniestro)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != siniestro.IdSiniestro)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(siniestro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };


        }

        // POST: api/Generoes
        [HttpPost]
        [Route("InsertarSiniestro")]
        public async Task<Response> InsertarSiniestro([FromBody] Siniestro siniestro)
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

                _context.Siniestro.Add(siniestro);
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
        public async Task<Response> DeleteSiniestro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var siniestro = await _context.Siniestro.SingleOrDefaultAsync(m => m.IdSiniestro == id);
            if (siniestro == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Siniestro.Remove(siniestro);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool SiniestroExists(int id)
        {
            return _context.Siniestro.Any(e => e.IdSiniestro == id);
        }
    }
}