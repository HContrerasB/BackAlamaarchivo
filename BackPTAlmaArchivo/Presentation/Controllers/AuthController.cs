using BackPTAlmaArchivo.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly PasswordEncryptionService _passwordEncryptionService;

    public AuthController(JwtService jwtService, PasswordEncryptionService passwordEncryptionService)
    {
        _jwtService = jwtService;
        _passwordEncryptionService = passwordEncryptionService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        using (var context = new PTAlmaArchivoDbContext())
        {
            var user = await context.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario == loginRequest.Username);

            if (user != null && _passwordEncryptionService.VerifyPassword(loginRequest.Password, user.Pass))
            {
                var token = _jwtService.GenerateToken(user.Usuario);
                return Ok(new { Token = token });
            }
        }

        return Unauthorized("Credenciales inválidas");
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        using (var context = new PTAlmaArchivoDbContext())
        {
            var encryptedPassword = _passwordEncryptionService.EncryptPassword(registerRequest.Password);
            var newUser = new Usuarios
            {
                Usuario = registerRequest.Username,
                Pass = encryptedPassword,
                FechaCreacion = DateTime.Now
            };

            context.Usuarios.Add(newUser);
            await context.SaveChangesAsync();

            return Ok(new { message = "Usuario registrado exitosamente" });
        }
    }
    [Authorize]
    [HttpGet("protected-endpoint")]
    public IActionResult ProtectedEndpoint()
    {
        return Ok("Acceso autorizado a un endpoint protegido");
    }




}
