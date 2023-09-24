using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.SeedWork
{
    public interface IGenericRepository<T> where T: Entity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> AddAsync(T entity);
        Task GetByIdAsync(int id);
        Task Get(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAll();
        Task UpdateAsync(T entity);
    }
}
