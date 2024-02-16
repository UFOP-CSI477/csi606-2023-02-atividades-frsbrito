using atividade_pratica.Entities;
using atividade_pratica.Services;
using Microsoft.AspNetCore.Mvc;

namespace atividade_pratica.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaService _pessoaService;

        public PessoaController(PessoaService PessoaService)
        {
            _pessoaService = PessoaService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Pessoa>>> Get(int? Id, string? Nome)
        {
            var listPessoas = await _pessoaService.Get(Id, Nome);
            if (listPessoas == null)
            {
                return NotFound();
            }

            return Ok(listPessoas);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Pessoa> AddPessoa(string Nome, string Rua, string Numero, string Complemento, string RG,
            int CidadeId, int TipoId)
        {
            var pessoaToAdd = new Pessoa()
            {
                Nome = Nome,
                Rua = Rua,
                Numero = Numero,
                Complemento = Complemento,
                RG = RG,
                CidadeId = CidadeId,
                TipoId = TipoId
            };
            pessoaToAdd.Created_at = DateTime.UtcNow;

            var pessoaCreated = await _pessoaService.AddPessoa(pessoaToAdd);

            return pessoaCreated;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePessoa(int Id, string Nome, string Rua, string Numero, string Complemento, string RG,
            int CidadeId, int TipoId)
        {
            var result = await _pessoaService.Get(Id);
            var pessoaToUpdate = result.FirstOrDefault();
            if (pessoaToUpdate == null) return NotFound();

            pessoaToUpdate.Nome = Nome;
            pessoaToUpdate.Rua = Rua;
            pessoaToUpdate.Numero = Numero;
            pessoaToUpdate.Complemento = Complemento;
            pessoaToUpdate.RG = RG;
            pessoaToUpdate.CidadeId = CidadeId;
            pessoaToUpdate.TipoId = TipoId;

            pessoaToUpdate.Updated_at = DateTime.UtcNow;

            await _pessoaService.UpdatePessoa(pessoaToUpdate);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePessoa(int Id)
        {
            var result = await _pessoaService.Get(Id);
            var pessoaToDelete = result.FirstOrDefault();
            if (pessoaToDelete == null) return NotFound();

            pessoaToDelete.Updated_at = DateTime.UtcNow;
            pessoaToDelete.Deleted_at = DateTime.UtcNow;

            await _pessoaService.DeletePessoa(pessoaToDelete);

            return NoContent();
        }
    }
}
