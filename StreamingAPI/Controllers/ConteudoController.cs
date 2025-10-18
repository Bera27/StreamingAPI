using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamingAPI.Data;
using StreamingAPI.Extension;
using StreamingAPI.Models;
using StreamingAPI.ViewModels;

namespace StreamingAPI.Controllers
{
    [ApiController]
    public class ConteudoController : ControllerBase
    {
        [HttpGet("v1/conteudos")]
        public async Task<IActionResult> GetAsync(
            [FromServices] StreamingDataContext context)
        {
            try
            {
                var conteudos = await context.Conteudos.ToListAsync();
                return Ok(new ResultViewModel<List<Conteudo>>(conteudos));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Conteudo>>("CT10X - Erro ao buscar conteúdos"));
            }
        }

        [HttpGet("v1/conteudos/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] StreamingDataContext context)
        {
            try
            {
                var conteudo = await context.Conteudos.FirstOrDefaultAsync(i => i.Id == id);

                if (conteudo == null)
                    return NotFound(new ResultViewModel<Conteudo>("CT20X - Conteúdo não encontrado"));

                return Ok(new ResultViewModel<Conteudo>(conteudo));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Conteudo>("CT21X - Erro ao buscar o conteúdo"));
            }
        }

        [HttpPost("v1/conteudos")]
        public async Task<IActionResult> PostAsync
        (
            [FromBody] EditorConteudoViewModel model,
            [FromServices] StreamingDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Conteudo>(ModelState.GetErrors()));

            try
            {
                var conteudo = new Conteudo
                {
                    Id = 0,
                    Titulo = model.Titulo,
                    Tipo = model.Tipo,
                    FileUrl = model.FileUrl,
                    CriadorId = model.CriadorId

                };
                await context.Conteudos.AddAsync(conteudo);
                await context.SaveChangesAsync();

                return Created($"v1/conteudos/{conteudo.Id}", new ResultViewModel<Conteudo>(conteudo));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Conteudo>("CT30X - Erro ao salvar o conteúdo"));
            }
        }

        [HttpPut("v1/conteudos/{id:int}")]
        public async Task<IActionResult> PutAsync
        (
            [FromRoute] int id,
            [FromBody] EditorConteudoViewModel model,
            [FromServices] StreamingDataContext context)
        {
            try
            {
                var conteudo = await context.Conteudos.FirstOrDefaultAsync(i => i.Id == id);

                if (conteudo == null)
                    return NotFound(new ResultViewModel<Conteudo>("CT40X - Conteúdo não encontrado"));

                conteudo.Titulo = model.Titulo;
                conteudo.FileUrl = model.FileUrl;
                conteudo.Tipo = model.Tipo;

                context.Conteudos.Update(conteudo);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Conteudo>(conteudo));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Conteudo>("CT41X - Não foi possível atualizar o conteúdo"));
            }
        }

        [HttpDelete("v1/conteudos/{id:int}")]
        public async Task<IActionResult> DeleteAsync
        (
            [FromRoute] int id,
            [FromServices] StreamingDataContext context)
        {
            try
            {
                var conteudo = await context.Conteudos.FirstOrDefaultAsync(i => i.Id == id);

                if (conteudo == null)
                    NotFound(new ResultViewModel<Conteudo>("CT50X - Conteúdo não encontrado"));

                context.Conteudos.Remove(conteudo);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<Conteudo>(conteudo));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Conteudo>("CT51X - Não foi possível excluir o conteudo"));
            }
        }

    }
}