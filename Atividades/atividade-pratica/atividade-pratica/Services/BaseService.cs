using atividade_pratica.Entities;
using atividade_pratica.Repository;

namespace atividade_pratica.Services
{
    public class BaseService<T> where T : BaseEntity
    {
        protected BaseRepository<T> _baseRepository { get; }

        public BaseService(BaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public IQueryable<T> Get()
        {
            return _baseRepository.GetAll();
        }
    }
}
