using AutoMapper;
using Blazor.Server.UseCases.Interfaces;
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
        private readonly IGeminiUseCase _geminiUseCase;


        public SeedController(AppDbContext context, IGeminiUseCase geminiUseCase)
        {
            _context = context;
            _geminiUseCase = geminiUseCase;
        }
        /// <summary>
        [HttpGet("reset")]
        public async Task<IActionResult> ResetData()
        {
            // Eliminar todos los registros existentes
            _context.Persons.RemoveRange(_context.Persons);
            await _context.SaveChangesAsync();
            var personas = await _geminiUseCase.geminiSeed() ?? new List<Person>();

            await _context.Persons.AddRangeAsync(personas);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Datos reseteados y registros creados." });
        }
    }
}
