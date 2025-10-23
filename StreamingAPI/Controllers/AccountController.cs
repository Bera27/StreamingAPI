using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using StreamingAPI.Data;
using StreamingAPI.Extension;
using StreamingAPI.Models;
using StreamingAPI.Services;
using StreamingAPI.ViewModels;

namespace StreamingAPI.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("v1/accounts/")]
        public async Task<IActionResult> Post(
            [FromBody] RegisterViewModel model,
            [FromServices] StreamingDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var usuario = new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                SenhaHash = model.SenhaHash
            };

            usuario.SenhaHash = PasswordHasher.Hash(model.SenhaHash);

            try
            {
                await context.Usuarios.AddAsync(usuario);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    usuario = usuario.Email,
                    model.SenhaHash
                }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultViewModel<string>("AC10A - Este E-mail já está cadastrado"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("AC11A - Falha no servidor"));
            }
        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(
            [FromServices] TokenService tokenService,
            [FromServices] StreamingDataContext context,
            [FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var usuario = await context
                .Usuarios
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (usuario == null)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            if (!PasswordHasher.Verify(usuario.SenhaHash, model.Senha))
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));
                
            try
            {
                var token = tokenService.GenerateToken(usuario);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("AC20L - Falha interna no servidor"));
            }
        }
    }
}