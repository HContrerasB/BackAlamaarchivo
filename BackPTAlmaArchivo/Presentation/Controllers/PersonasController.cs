using BackPTAlmaArchivo.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PersonasController : ControllerBase
{
    private readonly PTAlmaArchivoDbContext _context;

    public PersonasController(PTAlmaArchivoDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("ListarPersona")]
    public async Task<IActionResult> GetPersonas()
    {
        var personas = await _context.Personas.ToListAsync();
        return Ok(personas);
    }

    [Authorize]
    [HttpGet("ObtenerPersona{id}")]
    public async Task<IActionResult> GetPersona(int id)
    {
        var persona = await _context.Personas.FindAsync(id);
        if (persona == null) return NotFound();

        return Ok(persona);
    }

    [Authorize]
    [HttpPost("CrearPersona")]
    public async Task<IActionResult> CreatePersona([FromBody] Persona persona)
    {
        _context.Personas.Add(persona);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPersona), new { id = persona.Id }, persona);
    }

    [Authorize]
    [HttpPut("ActualizarPersona/{id}")]
    public async Task<IActionResult> UpdatePersona(int id, [FromBody] Persona updatedPersona)
    {
        if (id != updatedPersona.Id) return BadRequest();

        _context.Entry(updatedPersona).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPersona), new { id = updatedPersona.Id }, updatedPersona);

    }

    [Authorize]
    [HttpDelete("EliminarPersona/{id}")]
    public async Task<IActionResult> DeletePersona(int id)
    {
        var persona = await _context.Personas.FindAsync(id);
        if (persona == null) return NotFound();

        _context.Personas.Remove(persona);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
