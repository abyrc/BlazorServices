using BlazorCRUD.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPersonas()
        {
            var users = await _context.Persons.ToListAsync();

            return users.Select(user => new PersonDTO
            {
                ID = user.ID,
                nombreCompleto = user.Nombre + " " + user.ApellidoPaterno + " " + user.ApellidoMaterno,
                FechaNacimiento = user.FechaNacimiento,
                Sexo = user.Sexo
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersona(int id)
        {
            var persona = await _context.Persons.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            return persona;
        }


        [HttpPost]
        public async Task<ActionResult<PersonDTO>> PostPersona(RegisterDTO persona)
        {
            Person newPersona = new Person()
            {
                Nombre = persona.Nombre,
                ApellidoPaterno = persona.ApellidoPaterno,
                ApellidoMaterno = persona.ApellidoMaterno,
                FechaNacimiento = persona.FechaNacimiento,
                Sexo = persona.Sexo
            };
            _context.Persons.Add(newPersona);
            await _context.SaveChangesAsync();
            _context.Entry(newPersona).Reload();
            return new PersonDTO()
            {
                ID = newPersona.ID,
                nombreCompleto = newPersona.Nombre + " " + newPersona.ApellidoPaterno + " " + newPersona.ApellidoMaterno,
                FechaNacimiento = newPersona.FechaNacimiento,
                Sexo = newPersona.Sexo
            };
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersona(int id, RegisterDTO persona)
        {
            var updated = new Person
            {
                ID = id,
                Nombre = persona.Nombre,
                ApellidoPaterno = persona.ApellidoPaterno,
                ApellidoMaterno = persona.ApellidoMaterno,
                FechaNacimiento = persona.FechaNacimiento,
                Sexo = persona.Sexo
            };
            _context.Entry(updated).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Persons.Any(e => e.ID == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<bool> DeletePersona(int id)
        {
            var persona = await _context.Persons.FindAsync(id);
            if (persona == null) return false;

            _context.Persons.Remove(persona);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
