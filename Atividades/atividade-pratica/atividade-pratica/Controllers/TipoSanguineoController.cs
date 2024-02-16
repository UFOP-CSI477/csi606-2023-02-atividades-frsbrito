using atividade_pratica.Entities;
using atividade_pratica.Services;
using Microsoft.AspNetCore.Mvc;

namespace atividade_pratica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TipoSanguineoController : ControllerBase
    {
        private readonly TipoSanguineoService _tiposanguineoService;

        public TipoSanguineoController(TipoSanguineoService TipoSanguineoService)
        {
            _tiposanguineoService = TipoSanguineoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<TipoSanguineo>>> Get(int? Id, string? Nome)
        {
            var listTipoSanguineos = await _tiposanguineoService.Get(Id);
            if (listTipoSanguineos == null)
            {
                return NotFound();
            }

            return Ok(listTipoSanguineos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<TipoSanguineo> AddTipoSanguineo(string Tipo, string Fator)
        {
            var tiposanguineoToAdd = new TipoSanguineo()
            {
                Tipo = Tipo,
                Fator = Fator
            };
            tiposanguineoToAdd.Created_at = DateTime.UtcNow;

            var tiposanguineoCreated = await _tiposanguineoService.AddTipoSanguineo(tiposanguineoToAdd);

            return tiposanguineoCreated;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTipoSanguineo(int Id, string Tipo, string Fator)
        {
            var result = await _tiposanguineoService.Get(Id);
            var tiposanguineoToUpdate = result.FirstOrDefault();
            if (tiposanguineoToUpdate == null) return NotFound();

            tiposanguineoToUpdate.Tipo = Tipo;
            tiposanguineoToUpdate.Fator = Fator;

            tiposanguineoToUpdate.Updated_at = DateTime.UtcNow;

            await _tiposanguineoService.UpdateTipoSanguineo(tiposanguineoToUpdate);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTipoSanguineo(int Id)
        {
            var result = await _tiposanguineoService.Get(Id);
            var tiposanguineoToDelete = result.FirstOrDefault();
            if (tiposanguineoToDelete == null) return NotFound();

            tiposanguineoToDelete.Updated_at = DateTime.UtcNow;
            tiposanguineoToDelete.Deleted_at = DateTime.UtcNow;

            await _tiposanguineoService.DeleteTipoSanguineo(tiposanguineoToDelete);

            return NoContent();
        }
    }
}
