using atividade_pratica.Entities;
using atividade_pratica.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace atividade_pratica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoacaoController : ControllerBase
    {
        private readonly DoacaoService _doacaoService;

        public DoacaoController(DoacaoService DoacaoService)
        {
            _doacaoService = DoacaoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Doacao>>> Get(int? Id, string? Nome)
        {
            var listDoacao = await _doacaoService.Get(Id);
            if (listDoacao == null)
            {
                return NotFound();
            }

            return Ok(listDoacao);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Doacao> AddDoacao(int PessoaId, int LocalId, DateTime Data)
        {
            var doacaoToAdd = new Doacao()
            {
                PessoaId = PessoaId,
                LocalId = LocalId,
                Data = Data
            };
            doacaoToAdd.Created_at = DateTime.UtcNow;

            var doacaoCreated = await _doacaoService.AddDoacao(doacaoToAdd);

            return doacaoCreated;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDoacao(int Id, int PessoaId, int LocalId, DateTime Data)
        {
            var result = await _doacaoService.Get(Id);
            var doacaoToUpdate = result.FirstOrDefault();
            if (doacaoToUpdate == null) return NotFound();

            doacaoToUpdate.PessoaId = PessoaId;
            doacaoToUpdate.LocalId = LocalId;
            doacaoToUpdate.Data = Data;

            doacaoToUpdate.Updated_at = DateTime.UtcNow;

            await _doacaoService.UpdateDoacao(doacaoToUpdate);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDoacao(int Id)
        {
            var result = await _doacaoService.Get(Id);
            var doacaoToDelete = result.FirstOrDefault();
            if (doacaoToDelete == null) return NotFound();

            doacaoToDelete.Updated_at = DateTime.UtcNow;
            doacaoToDelete.Deleted_at = DateTime.UtcNow;

            await _doacaoService.DeleteDoacao(doacaoToDelete);

            return NoContent();
        }
    }
}
