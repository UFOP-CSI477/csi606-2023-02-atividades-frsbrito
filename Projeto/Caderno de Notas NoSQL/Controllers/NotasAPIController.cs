using Caderno_de_Notas_NoSQL.Models;
using Caderno_de_Notas_NoSQL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Caderno_de_Notas_NoSQL.Controllers
{
    [ApiController]
    [Route("api/Notas")]
    public class NotasAPIController : Controller, INotasAPIController
    {
        private readonly INotasRepository _NotasRepository;

        public NotasAPIController(INotasRepository NotasRepository)
        {
            _NotasRepository = NotasRepository;
        }

        [HttpGet]
        public async Task<Nota> Get(string id)
        {
            var nota = await _NotasRepository.Get(id);
            return nota;
        }

        [HttpGet("list")]
        public async Task<IEnumerable<Nota>> GetAll()
        {
            var Notas = await _NotasRepository.GetAll();
            return Notas;
        }

        [HttpPost]
        public async Task<Nota> Create(Nota Nota)
        {
            await _NotasRepository.InserirNota(Nota);
            return Nota;
        }

        [HttpPut("{id}")]
        public async Task<Nota> Update(string id, Nota Nota)
        {
            var existingNotas = await _NotasRepository.Get(id);

            if (existingNotas == null)
                return null;

            Nota.Id = existingNotas.Id;

            var result = await _NotasRepository.Update(Nota);

            if (!result)
                throw new Exception("Error ao atualizar as notas");

            return Nota;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            var existingNotas = await _NotasRepository.Get(id);

            if (existingNotas == null)
                return false;

            var result = await _NotasRepository.Delete(existingNotas);

            if (!result)
                throw new Exception("Erro ao deletar uma nota");

            return true;
        }
    }
}

