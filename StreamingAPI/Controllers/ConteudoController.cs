using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamingAPI.Data;
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
            var conteudos = await context.Conteudos.ToListAsync();
            return Ok(conteudos);
        }

        [HttpGet("v1/conteudos/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] StreamingDataContext context)
        {
            var conteudo = await context.Conteudos.FirstOrDefaultAsync(i => i.Id == id);

            if (conteudo == null)
                return NotFound();

            return Ok(conteudo);
        }

        [HttpPost("v1/conteudos")]
        public async Task<IActionResult> PostAsync
        (
            [FromBody] EditorConteudoViewModel model,
            [FromServices] StreamingDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest();

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

                return Created($"v1/conteudos/{conteudo.Id}", conteudo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "CT11X - Falha interna no servior" + ex.Message);
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
                    return NotFound();

                conteudo.Titulo = model.Titulo;
                conteudo.FileUrl = model.FileUrl;
                conteudo.Tipo = model.Tipo;

                context.Conteudos.Update(conteudo);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception)
            {
                return StatusCode(500, "CT20X - Não foi possível atualizar");
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
                    NotFound();

                context.Conteudos.Remove(conteudo);
                await context.SaveChangesAsync();

                return Ok(conteudo);
            }
            catch (Exception)
            {
                return StatusCode(500, "CT30X - Não foi possível excluir o conteudo");
            }
        }

    }
}