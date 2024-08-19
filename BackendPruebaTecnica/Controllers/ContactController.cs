using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendPruebaTecnica.Context;
using BackendPruebaTecnica.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace BackendPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)//TODO: Mejorar mensajes de error
        {
            _context = context;
        }
        //Endpoint para obtener los contactos
        // Method: get -  Endpoint: api/contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }
        //Endpoint para obtener un contacto en base al id
        // Method: get -  Endpoint: api/contact/?
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            //Realizamos la peticion a la bd para que busque el id
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        //Endpoint para actualizar un contacto en base al id
        // Method: put -  endpoint: api/contact/?
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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
        //Endpoint para buscar un contactos por nombre
        // Method GET - Endpoint: api/contact/search
        [HttpGet("search")]
        public async Task<ActionResult<Contact>> GetContactByName([FromQuery] string q)
        {
            var result = await _context.Contacts
                .Where(c => c.Name.Contains(q) || c.Email.Contains(q))
                .FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound(new { message = $"No se encontro ningun contacto llamado '{q}'" });
            }

            return Ok(result);
        }
        //Endpoint para agregar un contacto
        // Method: post -  endpoint: api/contact/
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }
        //Endpoint para eliminar un contacto
        // Method: delete -  endpoint: api/contact
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
