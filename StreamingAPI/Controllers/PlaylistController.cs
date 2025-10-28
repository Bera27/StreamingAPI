using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamingAPI.Data;
using StreamingAPI.Models;
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
            try
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
            catch
            {
                return StatusCode(500, new ResultViewModel<Playlist>("PC10G - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/playlist/{id:int}")]
        public async Task<IActionResult> GetIdAsync(
            [FromServices] StreamingDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var playlist = await context.Playlists
                    .AsNoTracking()
                    .Include(x => x.Usuario)
                    .Include(x => x.Items)
                        .ThenInclude(x => x.Conteudo)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (playlist == null)
                    return NotFound(new ResultViewModel<Playlist>("Conteúdo não encontrado"));

                return Ok(new ResultViewModel<Playlist>(playlist));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Playlist>("PC20G - Falha interna no servidor"));
            }
        }

        [HttpPost("v1/playlist")]
        public async Task<IActionResult> PostAsync(
            [FromBody] PlaylistViewModel model,
            [FromServices] StreamingDataContext context)
        {
            try
            {
                var playlist = new Playlist
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    UsuarioId = model.UsuarioId,
                };

                await context.AddAsync(playlist);
                await context.SaveChangesAsync();

                return Created($"v1/playlist/{playlist.Id}", new ResultViewModel<Playlist>(playlist));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Playlist>("PC30P - Falha interna no servidor"));   
            }
        }

        [HttpPost("v1/playlist/{id:int}/items")]
        public async Task<IActionResult> PostItemsAsync(
            [FromServices] StreamingDataContext context,
            [FromBody] ItemPlaylistViewModel model,
            [FromRoute] int id
        )
        {
            try
            {
                var item = new ItemPlaylist
                {
                    PlaylistId = model.PlaylistId,
                    ConteudoId = model.ConteudoId
                };

                await context.ItemPlaylists.AddAsync(item);
                await context.SaveChangesAsync();

                return Created($"v1/playlist/{id}/{item.PlaylistId}", new ResultViewModel<ItemPlaylist>(item));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Playlist>("PC40PI - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/playlist/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] StreamingDataContext context,
            [FromRoute] int id
        )
        {
            try
            {
                var playlist = context.Playlists.FirstOrDefault(x => x.Id == id);

                if (playlist == null)
                    return NotFound(new ResultViewModel<Playlist>("PC50D - Playlist não encontrada"));

                context.Playlists.Remove(playlist);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Playlist>(playlist));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Playlist>("PC60D - Falha interna no servidor"));
            }
        }
    }
}