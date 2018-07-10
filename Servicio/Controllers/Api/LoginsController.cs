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
    [Route("api/Logins")]
    public class LoginsController : Controller
    {
        private readonly SegurosContext _context;

        public LoginsController(SegurosContext context)
        {
            _context = context;
        }

        // GET: api/Logins
        [HttpGet]
        [Route("ListarLogin")]
        public IEnumerable<Login> GetLogin()
        {
            return _context.Login;
        }

        // GET: api/Logins/5
        [HttpGet("{id}")]
        public async Task<Response> GetLogin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var login = await _context.Login.SingleOrDefaultAsync(m => m.IdLogin == id);

            if (login == null)
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
                Resultado = login
            };
        }

        // PUT: api/Logins/5
        [HttpPut("{id}")]
        public async Task<Response> PutLogin([FromRoute] int id, [FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            if (id != login.IdLogin)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Entry(login).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.RegistroEditado
            };


        }

        // POST: api/Login
        [HttpPost]
        [Route("InsertarLogin")]
        public async Task<Response> InsertarLogin([FromBody] Login login)
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

                _context.Login.Add(login);
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

        // DELETE: api/Login/5
        [HttpDelete("{id}")]
        public async Task<Response> DeleteLogin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.ModeloInvalido
                };
            }

            var login = await _context.Login.SingleOrDefaultAsync(m => m.IdLogin == id);
            if (login == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Mensaje.Error
                };
            }

            _context.Login.Remove(login);
            await _context.SaveChangesAsync();
            return new Response
            {
                IsSuccess = true,
                Message = Mensaje.Satisfactorio
            };
        }

        private bool LoginExists(int id)
        {
            return _context.Login.Any(e => e.IdLogin == id);
        }
    }
}