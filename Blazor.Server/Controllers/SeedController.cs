using BlazorCRUD.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeedController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        [HttpGet("reset")]
        public async Task<IActionResult> ResetData()
        {
            // Eliminar todos los registros existentes
            _context.Persons.RemoveRange(_context.Persons);
            await _context.SaveChangesAsync();

            // Crear 10,000 registros nuevos
            var personas = new List<Person>();
            for (int i = 1; i <= 2000; i++)
            {
                personas.Add(new Person
                {
                    Nombre = $"Nombre{i}",
                    ApellidoPaterno = $"ApellidoP{i}",
                    ApellidoMaterno = $"ApellidoM{i}",
                    FechaNacimiento = DateTime.Today.AddYears(-20).AddDays(i), // solo para variar fechas
                    Sexo = i % 2 == 0 ? 'M' : 'F'
                });
            }

            await _context.Persons.AddRangeAsync(personas);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Datos reseteados y 2,000 registros creados." });
        }
    }
}
