using atividade_pratica.Context;
using atividade_pratica.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace atividade_pratica.Repository
{
    public class BaseRepository<T> where T : BaseEntity
    {
        private readonly AtividadeDbContext _context;

        public BaseRepository(AtividadeDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>()
                .AsQueryable<T>();
        }

        public async Task<IEnumerable<T>> List(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<T> InsertAsync(T entity)
        {
            _context.Set<T>()
                .Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>()
                .Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _context.Set<T>()
                .Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
