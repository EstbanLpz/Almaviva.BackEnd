using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class EmpleadosController : ControllerBase
{
    private static List<Empleado> empleados = new(); 

    [HttpPost("login")]
    public IActionResult Login([FromBody] dynamic login)
    {
        try {
            if (login.GetProperty("usuario").GetString() == "admin" && 
                login.GetProperty("password").GetString() == "12345")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("EstaEsUnaClaveSuperSecretaDe32CaracteresMinimo!");
                var tokenDescriptor = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "admin") }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { token = tokenHandler.WriteToken(token) });
            }
        } catch { return BadRequest("Formato de login inválido"); }
        
        return Unauthorized();
    }

    [Authorize]
    [HttpGet]
    public IActionResult Get() => Ok(empleados);

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult GetById(int id) {
        var empleado = empleados.FirstOrDefault(x => x.Id == id);
        if (empleado == null) return NotFound();
        return Ok(empleado);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Create([FromBody] Empleado e) {
        if (string.IsNullOrWhiteSpace(e.Nombre) || string.IsNullOrWhiteSpace(e.Email))
            return BadRequest("Campos obligatorios vacíos.");

        e.Id = empleados.Any() ? empleados.Max(x => x.Id) + 1 : 1;
        e.FechaIngreso = DateTime.Now;
        empleados.Add(e);
        return Ok(e);
    }

    [Authorize]
[HttpPut("{id}")]
public IActionResult Update(int id, [FromBody] Empleado e) {
    var index = empleados.FindIndex(x => x.Id == id);
    if (index == -1) return NotFound();

    empleados[index] = e; 
    return Ok(empleados[index]); 
}

[Authorize]
[HttpDelete("{id}")]
public IActionResult Delete(int id) {
    var empleado = empleados.FirstOrDefault(x => x.Id == id);
    if (empleado == null) return NotFound();
    
    empleado.Estado = false; 
    return Ok(empleado); 
}
}