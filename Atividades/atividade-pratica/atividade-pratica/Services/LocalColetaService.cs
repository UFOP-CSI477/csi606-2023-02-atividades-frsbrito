using atividade_pratica.Entities;
using atividade_pratica.Repository;
using atividade_pratica.Services;
using Microsoft.EntityFrameworkCore;

namespace atividade_pratica.Services
{
    public class LocalColetaService : BaseService<LocalColeta>
    {
        protected BaseRepository<LocalColeta> BaseRepository { get; }

        public LocalColetaService(BaseRepository<LocalColeta> baseRepository) : base(baseRepository)
        {
            BaseRepository = baseRepository;
        }

        public async Task<IEnumerable<LocalColeta>> Get(int? Id = null, string? Nome = null)
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

            return await result.ToListAsync<LocalColeta>();
        }

        public async Task<LocalColeta> AddLocalColeta(LocalColeta local)
        {
            return await BaseRepository.InsertAsync(local);
        }

        public async Task<LocalColeta> UpdateLocalColeta(LocalColeta local)
        {
            return await BaseRepository.UpdateAsync(local);
        }

        public async Task<LocalColeta> DeleteLocalColeta(LocalColeta local)
        {
            return await BaseRepository.UpdateAsync(local);
        }
    }
}
