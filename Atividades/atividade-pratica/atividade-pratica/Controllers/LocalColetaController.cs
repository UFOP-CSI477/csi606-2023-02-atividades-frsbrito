using atividade_pratica.Entities;
using atividade_pratica.Services;
using Microsoft.AspNetCore.Mvc;

namespace atividade_pratica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocalColetaController : ControllerBase
    {
        private readonly LocalColetaService _localcoletaService;

        public LocalColetaController(LocalColetaService LocalColetaService)
        {
            _localcoletaService = LocalColetaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<LocalColeta>>> Get(int? Id, string? Nome)
        {
            var listLocalColetas = await _localcoletaService.Get(Id, Nome);
            if (listLocalColetas == null)
            {
                return NotFound();
            }

            return Ok(listLocalColetas);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<LocalColeta> AddLocalColeta(string Nome, string Rua, string Numero, string Complemento,
            int CidadeId)
        {
            var localcoletaToAdd = new LocalColeta()
            {
                Nome = Nome,
                Rua = Rua,
                Numero = Numero,
                Complemento = Complemento,
                CidadeId = CidadeId
            };
            localcoletaToAdd.Created_at = DateTime.UtcNow;

            var localcoletaCreated = await _localcoletaService.AddLocalColeta(localcoletaToAdd);

            return localcoletaCreated;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLocalColeta(int Id, string Nome, string Rua, string Numero, string Complemento,
            int CidadeId)
        {
            var result = await _localcoletaService.Get(Id);
            var localcoletaToUpdate = result.FirstOrDefault();
            if (localcoletaToUpdate == null) return NotFound();

            localcoletaToUpdate.Nome = Nome;
            localcoletaToUpdate.Rua = Rua;
            localcoletaToUpdate.Numero = Numero;
            localcoletaToUpdate.Complemento = Complemento;
            localcoletaToUpdate.CidadeId = CidadeId;

            localcoletaToUpdate.Updated_at = DateTime.UtcNow;

            await _localcoletaService.UpdateLocalColeta(localcoletaToUpdate);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLocalColeta(int Id)
        {
            var result = await _localcoletaService.Get(Id);
            var localcoletaToDelete = result.FirstOrDefault();
            if (localcoletaToDelete == null) return NotFound();

            localcoletaToDelete.Updated_at = DateTime.UtcNow;
            localcoletaToDelete.Deleted_at = DateTime.UtcNow;

            await _localcoletaService.DeleteLocalColeta(localcoletaToDelete);

            return NoContent();
        }
    }
}
