using atividade_pratica.Entities;
using atividade_pratica.Repository;
using Microsoft.EntityFrameworkCore;

namespace atividade_pratica.Services
{
    public class TipoSanguineoService : BaseService<TipoSanguineo>
    {
        protected BaseRepository<TipoSanguineo> BaseRepository { get; }

        public TipoSanguineoService(BaseRepository<TipoSanguineo> baseRepository) : base(baseRepository)
        {
            BaseRepository = baseRepository;
        }

        public async Task<IEnumerable<TipoSanguineo>> Get(int? Id = null)
        {
            var result = BaseRepository.GetAll();
            if (Id is not null)
            {
                result = result.Where(p => p.Id == Id);
            }

            result = result.Where(p => p.Deleted_at == null);

            return await result.ToListAsync<TipoSanguineo>();
        }

        public async Task<TipoSanguineo> AddTipoSanguineo(TipoSanguineo tiposanguineo)
        {
            return await BaseRepository.InsertAsync(tiposanguineo);
        }

        public async Task<TipoSanguineo> UpdateTipoSanguineo(TipoSanguineo tiposanguineo)
        {
            return await BaseRepository.UpdateAsync(tiposanguineo);
        }

        public async Task<TipoSanguineo> DeleteTipoSanguineo(TipoSanguineo tiposanguineo)
        {
            return await BaseRepository.UpdateAsync(tiposanguineo);
        }
    }
}
