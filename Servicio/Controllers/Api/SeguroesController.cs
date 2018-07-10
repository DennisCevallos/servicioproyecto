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

        // GET: api/Seguroes
        [Route("ListarSeguroes")]
        [HttpGet]
        public IEnumerable<Seguro> GetSeguro()
        {
            return _context.Seguro;
        }

        // GET: api/Seguroes/5
        [HttpGet("{id}")]
        public async Task<Response> GetSeguro([FromRoute] int id)
        {
            var seguro = await _context.Seguro.SingleOrDefaultAsync(m => m.IdSeguro == id);

            if (seguro != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = seguro
                };
            }
            return new Response
            {
                IsSuccess = false,
                Message = Mensaje.ModeloInvalido
            };
        }

        // PUT: api/Seguroes/5
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
            var modificar = await _context.Seguro.Where(x => x.IdSeguro == id).FirstOrDefaultAsync();
            if(modificar !=null)
            {
                modificar.ValAsegurado = seguro.ValAsegurado;
                modificar.Tasa = seguro.Tasa;
                modificar.PrimaSeguro = seguro.PrimaSeguro;
                _context.Seguro.Update(modificar);
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

        // POST: api/Seguroes
        [Route("InsertarSeguroes")]
        [HttpPost]
        public async Task<Response> PostSeguro([FromBody] Seguro seguro)
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
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }
        }

        // DELETE: api/Seguroes/5
        [HttpDelete("{id}")]
        
        public async Task<Response> DeleteSeguro([FromRoute] int id)
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
                var respuesta = await _context.Seguro.SingleOrDefaultAsync(m => m.IdSeguro == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                _context.Seguro.Remove(respuesta);
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
        private bool SeguroExists(int id)
        {
            return _context.Seguro.Any(e => e.IdSeguro == id);
        }
    }
}

