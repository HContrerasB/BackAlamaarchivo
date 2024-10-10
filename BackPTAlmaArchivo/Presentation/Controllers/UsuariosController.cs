using BackPTAlmaArchivo.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly PTAlmaArchivoDbContext _context;

    public UsuariosController(PTAlmaArchivoDbContext context)
    {
        _context = context;
    }

    [Authorize]
    [HttpGet("ListarUsuarios")]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return Ok(usuarios);
    }

    [Authorize]
    [HttpGet("ObtenerUsuario/{id}")]
    public async Task<IActionResult> GetUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        return Ok(usuario);
    }

    [Authorize]
    [HttpPost("CrearUsuario")]
    public async Task<IActionResult> CreateUsuario([FromBody] Usuarios usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
    }

    [Authorize]
    [HttpPut("ActualizarUsuario/{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuarios updatedUsuario)
    {
        if (id != updatedUsuario.Id) return BadRequest();

        _context.Entry(updatedUsuario).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [Authorize]
    [HttpDelete("EliminarUsuario{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
