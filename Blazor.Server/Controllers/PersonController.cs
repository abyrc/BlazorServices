using AutoMapper;
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
        private readonly IMapper _mapper;

        public PersonController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("pagination")]
        public async Task<ActionResult> GetPersonasPagination( int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            IQueryable<Person> query = _context.Persons;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    (p.Nombre + " " + p.ApellidoPaterno + " " + p.ApellidoMaterno)
                    .Contains(search));
            }

            var totalRecords = await query.CountAsync();

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var usersDto = _mapper.Map<List<PersonDTO>>(users);

            var response = new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = usersDto
            };

            return Ok(response);
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPersonas()
        {
            var users = await _context.Persons.ToListAsync();
            var usersDto = _mapper.Map<List<PersonDTO>>(users);
            return usersDto;
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
        public async Task<ActionResult<PersonDTO?>> PostPersona(RegisterDTO persona)
        {
            try
            {
                Person newPersona = _mapper.Map<Person>(persona);
                _context.Persons.Add(newPersona);
                await _context.SaveChangesAsync();
                _context.Entry(newPersona).Reload();
                var resultDto = _mapper.Map<PersonDTO>(newPersona);
                return resultDto;
            }
            catch (Exception ex) {
                return null;
            }
        }


        [HttpPut("{id}")]
        public async Task<bool> PutPersona(int id, RegisterDTO persona)
        {
            var existing = await _context.Persons.FindAsync(id);
            if (existing == null)
                return false;

            existing.Nombre = persona.Nombre;
            existing.ApellidoPaterno = persona.ApellidoPaterno;
            existing.ApellidoMaterno = persona.ApellidoMaterno;
            existing.FechaNacimiento = persona.FechaNacimiento;
            existing.Sexo = persona.Sexo;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Persons.Any(e => e.ID == id))
                    return false;
                else
                    throw;
            }

            return true;
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
