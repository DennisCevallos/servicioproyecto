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
    [Route("api/Vehiculoes")]
    public class VehiculoesController : Controller
    {
        private readonly SegurosContext _context;

        public VehiculoesController(SegurosContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListarVehiculos")]
        public IEnumerable<Vehiculo> GetVehiculos()
        {
            return _context.Vehiculo;
        }

        [HttpGet("{id}")]
        public async Task<Response> GetVehiculos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var vehiculo = await _context.Vehiculo.SingleOrDefaultAsync(m => m.IdVehiculo == id);

            if (vehiculo == null)
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
                Resultado = vehiculo
            };
        }

        [HttpPut("{id}")]
        public async Task<Response> PutVehiculo([FromRoute] int id, [FromBody] Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != vehiculo.IdVehiculo)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(vehiculo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };


        }

        [HttpPost]
        [Route("InsertarVehiculo")]
        public async Task<Response> InsertarVehiculo([FromBody] Vehiculo vehiculo)
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

                _context.Vehiculo.Add(vehiculo);
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
        public async Task<Response> DeleteVehiculo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var vehiculo = await _context.Vehiculo.SingleOrDefaultAsync(m => m.IdVehiculo == id);
            if (vehiculo == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Vehiculo.Remove(vehiculo);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculo.Any(e => e.IdVehiculo == id);
        }
    }
}