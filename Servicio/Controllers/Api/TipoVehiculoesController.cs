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
    [Route("api/TipoVehiculoes")]
    public class TipoVehiculoesController : Controller
    {
        private readonly SegurosContext _context;

        public TipoVehiculoesController(SegurosContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListarTipoVehiculo")]
        public IEnumerable<TipoVehiculo> GetTipoVehiculo()
        {
            return _context.TipoVehiculo;
        }

        [HttpGet("{id}")]
        public async Task<Response> GetTipoVehiculo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var tipoVehiculo = await _context.TipoVehiculo.SingleOrDefaultAsync(m => m.IdTipoVehiculo == id);

            if (tipoVehiculo == null)
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
                Resultado = tipoVehiculo
            };
        }

        [HttpPut("{id}")]
        public async Task<Response> PutTipoVehiculo([FromRoute] int id, [FromBody] TipoVehiculo tipoVehiculo)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != tipoVehiculo.IdTipoVehiculo)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(tipoVehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };


        }

        [HttpPost]
        [Route("InsertarTipoVehiculo")]
        public async Task<Response> InsertarTipoVehiculo([FromBody] TipoVehiculo tipoVehiculo)
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

                _context.TipoVehiculo.Add(tipoVehiculo);
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

        [HttpDelete("{id}")]
        public async Task<Response> DeleteTipoVehiculo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var tipoVehiculo = await _context.TipoVehiculo.SingleOrDefaultAsync(m => m.IdTipoVehiculo == id);
            if (tipoVehiculo == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.TipoVehiculo.Remove(tipoVehiculo);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool TipoVehiculoExists(int id)
        {
            return _context.TipoVehiculo.Any(e => e.IdTipoVehiculo == id);
        }
    }
}