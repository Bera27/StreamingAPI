using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamingAPI.Data;
using StreamingAPI.ViewModels;

namespace StreamingAPI.Controllers
{
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        [HttpGet("v1/playlists")]
        public async Task<ActionResult> GetAsync(
            [FromServices] StreamingDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            var playlists = await context.Playlists
                .AsNoTracking()
                .Include(x => x.Usuario)
                .Include(x => x.Items)
                    .ThenInclude(x => x.Conteudo)
                .Select(x => new PlaylistViewModel
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    Usuario = $"{x.Usuario.Nome} ({x.Usuario.Email})",
                    Items = x.Items.Select(i => i.Conteudo.Titulo).ToList()
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(playlists));
        }
    }
}