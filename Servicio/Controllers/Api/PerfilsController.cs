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
    [Route("api/Perfils")]
    public class PerfilsController : Controller
    {
        private readonly SegurosContext _context;

        public PerfilsController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Perfils
        [HttpGet]
        [Route("ListarPerfil")]
        public IEnumerable<Perfil> GetPerfil()
        {
            return _context.Perfil;
        }

        // GET: api/Perfils/5
        [HttpGet("{id}")]
        public async Task<Response> GetPerfil([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var perfil = await _context.Perfil.SingleOrDefaultAsync(m => m.IdPerfil == id);

            if (perfil == null)
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
                Resultado = perfil
            };
        }

        // PUT: api/Perfil/5
        [HttpPut("{id}")]
        public async Task<Response> PutPerfil([FromRoute] int id, [FromBody] Perfil perfil)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != perfil.IdPerfil)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(perfil).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };


        }

        // POST: api/Generoes
        [HttpPost]
        [Route("InsertarPerfil")]
        public async Task<Response> InsertarPerfil([FromBody] Perfil perfil)
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

                _context.Perfil.Add(perfil);
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

        // DELETE: api/Perfils/5
        [HttpDelete("{id}")]
        public async Task<Response> DeletePerfil([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var perfil = await _context.Perfil.SingleOrDefaultAsync(m => m.IdPerfil == id);
            if (perfil == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Perfil.Remove(perfil);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool PerfilExists(int id)
        {
            return _context.Perfil.Any(e => e.IdPerfil == id);
        }
    }
}