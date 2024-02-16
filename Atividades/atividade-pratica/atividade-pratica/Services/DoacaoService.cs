using atividade_pratica.Entities;
using atividade_pratica.Repository;
using atividade_pratica.Services;
using Microsoft.EntityFrameworkCore;

namespace atividade_pratica.Services
{
    public class DoacaoService : BaseService<Doacao>
    {
        protected BaseRepository<Doacao> BaseRepository { get; }

        public DoacaoService(BaseRepository<Doacao> baseRepository) : base(baseRepository)
        {
            BaseRepository = baseRepository;
        }

        public async Task<IEnumerable<Doacao>> Get(int? Id = null)
        {
            var result = BaseRepository.GetAll();

            if (Id is not null)
            {
                result = result.Where(p => p.Id == Id);
            }

            result = result.Where(p => p.Deleted_at == null);

            return await result.ToListAsync<Doacao>();
        }

        public async Task<Doacao> AddDoacao(Doacao doacao)
        {
            return await BaseRepository.InsertAsync(doacao);
        }

        public async Task<Doacao> UpdateDoacao(Doacao doacao)
        {
            return await BaseRepository.UpdateAsync(doacao);
        }

        public async Task<Doacao> DeleteDoacao(Doacao doacao)
        {
            return await BaseRepository.UpdateAsync(doacao);
        }
    }
}
