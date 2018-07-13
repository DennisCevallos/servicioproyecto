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
    [Route("api/Personas")]
    public class PersonasController : Controller
    {
        private readonly SegurosContext _context;

        public PersonasController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Personas
        [HttpGet]
        [Route("ListarPersonas")]
        [HttpGet]
        public IEnumerable<Persona> GetPersona()
        {
            return _context.Persona;
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<Response> GetPersona([FromRoute] int id)
        {
            var persona = await _context.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);
            if (persona != null)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Mensaje.Satisfactorio,
                    Resultado = persona
                };
            }
            return new Response
            {
                IsSuccess = false,
                Message = Mensaje.ModeloInvalido
            };
        }

        // PUT: api/Personas/5
        [HttpPut("{id}")]
        public async Task<Response> PutPersona([FromRoute] int id, [FromBody] Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }
            var modificar = await _context.Persona.Where(x => x.IdPersona == id).FirstOrDefaultAsync();
            if (modificar != null)
            {
                modificar.Identificacion = persona.Identificacion;
                modificar.Nombres = persona.Nombres;
                modificar.Apellido = persona.Apellido;
                modificar.Direccion = persona.Direccion;
                modificar.Email = persona.Email;
                modificar.Telefono = persona.Telefono;
                modificar.Celular = persona.Celular;
                modificar.Estado = persona.Estado;
                modificar.IdGenero = persona.IdGenero;
                _context.Persona.Update(modificar);
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

        // POST: api/Personas
        [Route("InsertarPersona")]
        [HttpPost]
        public async Task<Response> PostPersona([FromBody] Persona persona)
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
                _context.Persona.Add(persona);
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

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePersona([FromRoute] int id)
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
                var respuesta = await _context.Persona.SingleOrDefaultAsync(m => m.IdPersona == id);
                if (respuesta == null)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = Mensaje.RegistroNoEncontrado,
                    };
                }
                _context.Persona.Remove(respuesta);
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
        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.IdPersona == id);
        }
    }
}
