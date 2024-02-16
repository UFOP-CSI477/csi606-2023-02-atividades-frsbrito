using atividade_pratica.Entities;
using atividade_pratica.Repository;
using atividade_pratica.Services;
using Microsoft.EntityFrameworkCore;

namespace atividade_pratica.Services
{
    public class PessoaService : BaseService<Pessoa>
    {
        protected BaseRepository<Pessoa> BaseRepository { get; }

        public PessoaService(BaseRepository<Pessoa> baseRepository) : base(baseRepository)
        {
            BaseRepository = baseRepository;
        }

        public async Task<IEnumerable<Pessoa>> Get(int? Id = null, string? Nome = null)
        {
            var result = BaseRepository.GetAll();

            if (Id is not null)
            {
                result = result.Where(p => p.Id == Id);
            }
            if (!string.IsNullOrEmpty(Nome))
            {
                result = result.Where(p => p.Nome == Nome);
            }

            result = result.Where(p => p.Deleted_at == null);
            return await result.ToListAsync<Pessoa>();
        }

        public async Task<Pessoa> AddPessoa(Pessoa pessoa)
        {
            return await BaseRepository.InsertAsync(pessoa);
        }

        public async Task<Pessoa> UpdatePessoa(Pessoa pessoa)
        {
            return await BaseRepository.UpdateAsync(pessoa);
        }

        public async Task<Pessoa> DeletePessoa(Pessoa pessoa)
        {
            return await BaseRepository.UpdateAsync(pessoa);
        }
    }
}
