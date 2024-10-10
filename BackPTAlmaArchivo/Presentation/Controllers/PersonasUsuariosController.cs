using BackPTAlmaArchivo.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PersonasUsuariosController : ControllerBase
{
    private readonly PTAlmaArchivoDbContext _context;

    public PersonasUsuariosController(PTAlmaArchivoDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("listar")]
    public async Task<IActionResult> GetPersonasUsuarios()
    {
        var personasUsuarios = await _context.PersonasUsuarios.ToListAsync();
        return Ok(personasUsuarios);
    }

    [Authorize]
    [HttpGet("Obtener/{id}")]
    public async Task<IActionResult> GetPersonaUsuario(int id)
    {
        var personaUsuario = await _context.PersonasUsuarios.FindAsync(id);
        if (personaUsuario == null) return NotFound();

        return Ok(personaUsuario);
    }

    [Authorize]
    [HttpPost("crear")]
    public async Task<IActionResult> CreatePersonaUsuario([FromBody] PersonasUsuario personaUsuario)
    {
        _context.PersonasUsuarios.Add(personaUsuario);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPersonaUsuario), new { id = personaUsuario.Id }, personaUsuario);
    }

    [Authorize]
    [HttpPut("Actualizar/{id}")]
    public async Task<IActionResult> UpdatePersonaUsuario(int id, [FromBody] PersonasUsuario updatedPersonaUsuario)
    {
        if (id != updatedPersonaUsuario.Id) return BadRequest();

        _context.Entry(updatedPersonaUsuario).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("Eliminar/{id}")]
    public async Task<IActionResult> DeletePersonaUsuario(int id)
    {
        var personaUsuario = await _context.PersonasUsuarios.FindAsync(id);
        if (personaUsuario == null) return NotFound();

        _context.PersonasUsuarios.Remove(personaUsuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
