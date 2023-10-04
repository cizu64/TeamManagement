using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.SeedWork;

namespace TaskManagement.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected readonly TaskManagementContext _context;
        internal DbSet<T> _set;
        public IUnitOfWork UnitOfWork => _context;
        public GenericRepository(TaskManagementContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _set.AddAsync(entity);
            return entity;
        }
        public async Task<bool> AnyAsync(Expression<Func<T,bool>> entity)
        {
            var any = await _set.AnyAsync(entity);
            return any;
        }
        public async Task<T> Get(Expression<Func<T, bool>> predicate, params string[] includes)
        {
            var data = await _set.FirstOrDefaultAsync(predicate);
            if (includes.Any())
            {
                data = Include(includes).FirstOrDefault();
            }
            return data;
        }

        public async Task<IReadOnlyList<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            var data = predicate == null ? _set : _set.Where(predicate);
            if (includes.Any())
            {
                data = Include(includes).AsQueryable();
            }
            return await data.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params string[] includes)
        {
            var data = await _set.FindAsync(id);
            if(includes.Any())
            {
                data = Include(includes).FirstOrDefault();
            }
            return data;
        }

        public Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        IEnumerable<T> Include(params string[] includes)
        {
            IEnumerable<T> query = null;
            foreach (var include in includes)
            {
                query = _set.Include(include);
            }
            return query ?? _set;
        }
    }
}
