using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendPruebaTecnica.Context;
using BackendPruebaTecnica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BackendPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase //TODO: Mejorar mensajes de error
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }
        //Endpoint para mostrar a los usuarios
        // Method GET - Endpoint: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        //Endpoint para buscar un usuarios por id
        // Method GET - Endpoint: api/user/?
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, message = "Usuario encontrado", data = user });
        }
        //Endpoint para buscar un usuarios por nombre
        // Method GET - Endpoint: api/user/?
        [HttpGet("Search")]
        public async Task<ActionResult<User>> GetUserByName([FromQuery] string q)
        {
            var result = await _context.Users
                .Where(u => u.UserName.Contains(q) || u.Email.Contains(q))
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound(new { message = $"No se encontro ningun usuario llamado '{q}'" });
            }

            return Ok(result);
        }
        //Endpoint para actualizar los valores de un usuario
        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        //Endpoint para agregar un usuario
        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserById", new { id = user.Id }, user);
        }

        //Endopoint para borrar un usuario
        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
