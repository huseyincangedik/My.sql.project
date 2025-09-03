using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My.sql.project.Contexts;
using My.sql.project.Models;
using BCrypt.Net;

namespace My.sql.project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public AuthController(ProductDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            // Email kontrolü
            if (await _context.Users.AnyAsync(u => u.Email == newUser.Email))
            {
                return BadRequest(new { message = "Bu email zaten kayıtlı." });
            }

            // Şifreyi hash'le
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Kayıt başarılı." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            // Sadece email'e göre kullanıcıyı bul
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginUser.Email.ToLower());

            // Kullanıcı yoksa veya şifre yanlışsa
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            {
                return Unauthorized(new { message = "Hatalı email veya şifre." });
            }

            return Ok(new
            {
                message = "Giriş başarılı.",
                user.Id,
                user.Username,
                user.Email,
                user.Role
            });
        }
    }
}