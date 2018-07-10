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
    [Route("api/Generoes")]
    public class GeneroesController : Controller
    {
        private readonly SegurosContext _context;

        public GeneroesController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Generoes
        [HttpGet]
        [Route("ListarGenero")]
        public IEnumerable<Genero> GetGenero()
        {
            return _context.Genero;
        }

        // GET: api/Generoes/5
        [HttpGet("{id}")]
        public async Task<Response> GetGenero([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var genero = await _context.Genero.SingleOrDefaultAsync(m => m.IdGenero == id);

            if (genero == null)
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
                Resultado = genero
            };
        }

        // PUT: api/Generoes/5
        [HttpPut("{id}")]
        public async Task<Response> PutGenero([FromRoute] int id, [FromBody] Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != genero.IdGenero)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(genero).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };

           
        }

        // POST: api/Generoes
        [HttpPost]
        [Route("InsertarGenero")]
        public async Task<Response> InsertarGenero([FromBody] Genero genero)
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

                _context.Genero.Add(genero);
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
        public async Task<Response> DeleteGenero([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                 return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var respuesta = await _context.Genero.SingleOrDefaultAsync(m => m.IdGenero == id);
            if (respuesta == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Genero.Remove(respuesta);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool GeneroExists(int id)
        {
            return _context.Genero.Any(e => e.IdGenero == id);
        }
    }
}